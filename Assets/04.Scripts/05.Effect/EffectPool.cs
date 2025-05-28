using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class EffectPool : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] dashList;
    private const int dashCount = 20;
    private static int dashCounter = 0;

    [SerializeField] private ParticleSystem ramEffect;
    private const int ramCount = 20;
    private static int ramCounter = 0;

    [SerializeField] private ParticleSystem batteryEffect;
    private const int batteryCount = 20;
    private static int batteryCounter = 0;

    private static List<GameObject> dashObjects = new();
    private static List<GameObject> ramObjects = new();
    private static List<GameObject> batteryObjects = new();

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
        for (int i = 0; i < ramCount; i++)
        {
            var effect = Instantiate(ramEffect, gameObject.transform);
            ramObjects.Add(effect.gameObject);
            effect.gameObject.SetActive(false);
        }
        for (int i = 0; i < batteryCount; i++)
        {
            var effect = Instantiate(batteryEffect, gameObject.transform);
            batteryObjects.Add(effect.gameObject);
            effect.gameObject.SetActive(false);
        }
    }

    public static void DashEffect(MoveTo moveTo, Vector3 pos)
    {
        dashCounter++;
        if(dashCount == dashCounter)
        {
            dashCounter = 0;
        }
        var rot = moveTo switch
        {
            MoveTo.Forward => new Vector3(0f, 0f, 0f),
            MoveTo.Right => new Vector3(0f, 60f, 0f),
            MoveTo.Left => new Vector3(0f, -60f, 0f),
            MoveTo.Back => new Vector3(0f, 180f, 0f),
            _ => new Vector3(0f, 180f, 0f)
        };

        var effect = dashObjects[dashCounter];

        effect.transform.position = pos;
        effect.transform.rotation = Quaternion.Euler(rot);

        effect.SetActive(true);
    }

    public static void RamEffect(Vector3 pos)
    {
        ramCounter++;
        if (ramCount == ramCounter)
        {
            ramCounter = 0;
        }

        var effect = ramObjects[ramCounter];
        
        effect.transform.position = pos; 
        pos.y -= 0.5f;
        effect.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

        effect.SetActive(true);
    }

    public static void BatteryEffect(Vector3 pos)
    {
        batteryCounter++;
        if (batteryCount == batteryCounter)
        {
            batteryCounter = 0;
        }

        var effect = batteryObjects[batteryCounter];
        pos.y -= 0.5f;
        effect.transform.position = pos;
        effect.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

        effect.SetActive(true);
    }
    private void OnDestroy()
    {
        dashObjects.Clear();  
        ramObjects.Clear();
        batteryObjects.Clear();
    }
}
