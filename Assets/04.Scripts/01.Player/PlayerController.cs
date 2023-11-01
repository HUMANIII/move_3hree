using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum MoveTo
    {
        Forward,
        Right,
        Left,
        Back
    }
    
    protected Rigidbody rb;
    protected TileManager tileManager;
    protected TimerScripts timerScripts;
    protected PlayerStatManager playerStatManager;
    protected Collider cr;
    protected int knockbackCounter = 0;
    protected MoveTo prevMoveTo;
    [SerializeField] protected ParticleSystem chainedEffect;
    [SerializeField] protected ParticleSystem fallDownEffect;

    protected Animator animator;
    
    public int timerDecreaseFactor = 10; 
    public int scoreFactor = 10;

    protected float moveUpperInterval;
    protected float moveSideInterval;

    public int overclockActiveCount = 40;
    protected int overclockActiveCounter;

    [SerializeField] protected int moveCount = 1;
    protected int moveCounter = 0;

    public int warnEarly = 0;

    public int obstructionFactor = 1;

    public float ramGatheringFactor = 1;

    public float maxTimeLimitCtrl = 0f;

    protected void Awake()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        timerScripts = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();
        playerStatManager = GameObject.FindGameObjectWithTag("PlayerStatManager").GetComponent<PlayerStatManager>();
        rb = GetComponent<Rigidbody>();
        cr = GetComponentInChildren<Collider>();
        animator = GetComponentInChildren<Animator>();
        overclockActiveCount -= playerStatManager.upgrade.overclockOptimization * playerStatManager.overclockOptimizationRate;
        SpecificEffect();
    }
    protected void Start()
    {
        moveUpperInterval = tileManager.UpperInterval;
        moveSideInterval = tileManager.SideInterval;
        knockbackCounter -= playerStatManager.upgrade.knockbackResist;
    }

    protected void FixedUpdate()
    {
        var gm = GameManager.Instance;
        if (transform.position.y < -0.5f && (gm.State & (GameManager.States.IsGameOver | GameManager.States.IsLoading)) == 0)
        {
            fallDownEffect.gameObject.SetActive(true);
        }
        if((gm.State & GameManager.States.IsGameOver) != 0)
        {
            Destroy(gameObject);
        }
    }

    protected void Update()
    {
        //cheatCode
        if(Input.GetKeyDown(KeyCode.F8))
        {
            GameManager.Instance.CurScore += 500;
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            tileManager.ActiveOverclock();
        }
        var gm = GameManager.Instance;
        if ((gm.State & (GameManager.States.IsGameOver | GameManager.States.IsPause)) != 0)
            return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveWithButton(MoveTo.Forward);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveWithButton(MoveTo.Left);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveWithButton(MoveTo.Right);
        }
    }

    protected virtual void MovePosition(Vector3 pos)
    {
        var gm = GameManager.Instance;
        if ((gm.State & (GameManager.States.IsTrapped | GameManager.States.IsHolded)) != 0)
            return;

        if (pos.x == transform.position.x)
        {
            GameManager.Instance.CurScore += scoreFactor;
        }
        else
        {
            GameManager.Instance.CurScore += scoreFactor / 2;
        }

        pos.y = 2f;
        //var ts = TileManager.CheckUnderTile(pos);

        if((timerScripts.curMaxTime / 2f) < timerScripts.Timer)
        {
            overclockActiveCounter++;
        }
        else
        {
            overclockActiveCounter = 0;
        }
        EffectPool.DashEffect(prevMoveTo, transform.position);
        MoveObjectAndTriggerEvent(pos);
        SoundManager.Instance.MoveSound();
        if (overclockActiveCounter >= overclockActiveCount) 
        {
            tileManager.ActiveOverclock();
            overclockActiveCounter = 0;
        }
        tileManager.SpawnTile();
        tileManager.CheckAllTiles();

        if (tileManager.PlayerLineCounter % timerDecreaseFactor == 0)
        {
            timerScripts.DecreaseMaxTime();
        }
        moveCounter++;
        if(moveCount <= moveCounter)
        {
            timerScripts.ResetTimer();
            moveCounter = 0;
        }
    }

    public void MoveWithButton(MoveTo where) 
    {
        var gm = GameManager.Instance;
        prevMoveTo = where;

        Vector3 pos = transform.position;
        if ((gm.State & GameManager.States.IsHolded) != 0)
        {
            var ht = TileManager.CheckUnderTile(pos) as HoldTile;
            if(ht != null)
            {
                ht.Struggle();
            }
            return;
        }

        pos += where switch
        {
            MoveTo.Forward => new Vector3(0f, 0f, moveUpperInterval * 2),
            MoveTo.Left => new Vector3(-moveSideInterval * 2, 0f, moveUpperInterval),
            MoveTo.Right => new Vector3(moveSideInterval * 2, 0f, moveUpperInterval),           
            _ => new Vector3()
        };
        MovePosition(pos);
        animator.SetTrigger("Move");
        switch (where)
        {
            case MoveTo.Forward:
                rb.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case MoveTo.Right:
                rb.rotation = Quaternion.Euler(0f, 30f, 0f);
                break;
            case MoveTo.Left:
                rb.rotation = Quaternion.Euler(0f, -30f, 0f);
                break;
        }
        var ts = TileManager.CheckUnderTile(transform.position);
        Debug.Assert(ts != null);
        if (ts != null)         
        {
            Debug.Log("In");
            ts.GetPos();
            if(ts.GetComponent<HoldTile>() != null)
            {
                GameManager.Instance.IsHolded();
                chainedEffect.gameObject.SetActive(true);
                chainedEffect.Play();
            }
            else if(ts.GetComponent<TrapTileScript>() != null)
            {
                GameManager.Instance.IsTrapped();                
            }
        }
    }

    public void Knockback()
    {
        ReleaseHold();
        var kc = Mathf.Clamp(knockbackCounter, 0, 1);
        var knockbackRange = moveUpperInterval * (kc + 2) * 2 * obstructionFactor;
        var pos = transform.position;
        pos.z -= knockbackRange;
        MoveObjectAndTriggerEvent(pos);
        knockbackCounter++;
    }

    protected void MoveObjectAndTriggerEvent(Vector3 newPosition)
    {
        transform.position = newPosition;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider collider in colliders)
        {
            var kb = collider.GetComponentInParent<KnockbackTile>();
            if(kb != null)
            {
                Knockback();
                Physics.Raycast(transform.position,(collider.transform.position - transform.position).normalized, out var hitInfo, 15f);
                kb.ActiveEffect(hitInfo.point);
            }
        }
    }

    public void ReleaseHold()
    {
        chainedEffect.Stop();
        chainedEffect.gameObject.SetActive(false);
        GameManager.Instance.ReleaseHold();
    }

    protected virtual void SpecificEffect()
    {

    }
}
