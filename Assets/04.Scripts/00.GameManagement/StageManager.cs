using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Serializable]
    public class StageTileInfo
    {
        public string name;
        public GameObject normalTile;
        public GameObject holeTile;
        public float holeTileSpawnRate;
        public GameObject fallingObjectTile;
        public float fallingObjectTileSpawnRate;
        public GameObject knockbackTile;
        public float knockbackTileSpawnRate;
        public GameObject holdTile;
        public float holdTileSpawnRate;

        public Material skyBoxMaterial;

        public int maximumTrapTile;

        public int nextStateScore;
    }

    public StageTileInfo[] stageTileInfos = new StageTileInfo[3];

    public StageTileInfo GetStageTileInfo(int stageCount)
    {
        return stageTileInfos[stageCount - 1];
    }
}
