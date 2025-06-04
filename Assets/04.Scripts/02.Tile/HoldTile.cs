public class HoldTile : TrapTileScript
{
    public int clickCount;
    private int clickCounter = 0;
    private PlayerController pc;

    protected override void Awake()
    {
        base.Awake();
        pc = player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        clickCount *= pc.obstructionFactor;
    }


    public void Struggle()
    {
        clickCounter++;

        if(clickCounter >= clickCount)
        {
            pc.ReleaseHold();
        }        
    }
}
