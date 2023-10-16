using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStatManager;

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
        rtn.bestScore = bestScore;
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
        var rtn = new SaveDataV4();
        rtn.bestScore = bestScore;
        rtn.ramCount = ramCount;
        rtn.options = options;
        return rtn;
    }
}

public class SaveDataV4 : SaveDataV3
{
    public SaveDataV4()
    {
        Version = 4;
        upgrade = new();
    }

    public PlayerStatManager.Upgrade upgrade;

    public override SaveData VersionUp()
    {
        var rtn = new SaveDataV5();
        rtn.bestScore = bestScore;
        rtn.ramCount = ramCount;
        rtn.options = options;
        rtn.upgrade = upgrade;
        return rtn;
    }
}

public class SaveDataV5 : SaveDataV4
{
    public SaveDataV5()
    {
        Version = 5;
    }

    public PlayerType playerType = PlayerType.DefaultPhone;

    public override SaveData VersionUp()
    {
        return null;
    }
}
