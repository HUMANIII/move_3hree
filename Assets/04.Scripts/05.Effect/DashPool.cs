using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPool : MonoBehaviour
{
    public ParticleSystem[] dashList;
    [SerializeField] private static int dashCount = 20;
    private static int dashCounter = 0;

    private static List<GameObject> dashObjects = new();

    private void Awake()
    {
        Debug.Assert(dashList.Length > 0);

        int index = (int)PlayerStatManager.Instance.playerType;

        for (int i = 0; i < dashCount; i++)
        {
            var effect = Instantiate(dashList[index], gameObject.transform);
            dashObjects.Add(effect.gameObject);
            effect.gameObject.SetActive(false);
        }
    }

    public static void DashEffect(PlayerController.MoveTo moveTo, Vector3 pos)
    {
        dashCounter++;
        if(dashCount == dashCounter)
        {
            dashCounter = 0;
        }
        var rot = moveTo switch
        {
            PlayerController.MoveTo.Forward => new Vector3(0f, 0f, 0f),
            PlayerController.MoveTo.Right => new Vector3(0f, 60f, 0f),
            PlayerController.MoveTo.Left => new Vector3(0f, -60f, 0f),
            PlayerController.MoveTo.Back => new Vector3(0f, 180f, 0f),
        };

        var effect = dashObjects[dashCounter];

        effect.transform.position = pos;
        effect.transform.rotation = Quaternion.Euler(rot);

        effect.SetActive(true);
    }

    private void OnDestroy()
    {
        dashObjects.Clear();        
    }
}
