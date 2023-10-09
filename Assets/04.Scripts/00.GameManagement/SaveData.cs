using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveData
{
    public int Version { get; set; }

    public abstract SaveData VersionUp();
} 

public class SaveDataV1 : SaveData
{
    public SaveDataV1()
    {
        Version = 1;
    }

    public int bestScore = 0;

    public override SaveData VersionUp()
    {
        var rtn = new SaveDataV2();
        rtn.bestScore = bestScore;
        return rtn;
    }
}

public class SaveDataV2 : SaveDataV1
{
    public SaveDataV2()
    {
        Version = 2;
    }

    public int ramCount = 0;

    public override SaveData VersionUp()
    {      
        var rtn = new SaveDataV3();
        rtn.ramCount = ramCount;
        return rtn;
    }
}

public class SaveDataV3 : SaveDataV2
{
    public SaveDataV3()
    {
        Version = 3;
    }

    public GameManager.Settings options = 0;

    public override SaveData VersionUp()
    {
        return null;
    }
}
