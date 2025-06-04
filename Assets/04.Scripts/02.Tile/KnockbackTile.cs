using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTile : TrapTileScript
{
    [SerializeField] protected List<ParticleSystem> effects;

    public void ActiveEffect(Vector3 pos)
    {
        var effect = PlayerStatManager.Instance.playerType switch
        {
            PlayerStatManager.PlayerType.DefaultPhone => effects[0],
            PlayerStatManager.PlayerType.JailbreakedPhone => effects[1],
            PlayerStatManager.PlayerType.GreenApplePhone => effects[2],
            PlayerStatManager.PlayerType.BananaPhone => effects[3],
            PlayerStatManager.PlayerType.MithrillPhone => effects[4],
            _ => null
        };
        Instantiate(effect, pos ,Quaternion.identity);
    }
}
