using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MusicPowerTreasure : Treasure
{
    protected override void Reward()
    {
        StartCoroutine(SpreadMusicPowerCoroutine());
    }

    private IEnumerator SpreadMusicPowerCoroutine()
    {
        for(int i = 0; i < 25; i++)
        {
            Vector3 jumpPos = new Vector3(Random.Range(-2f, 2f), Random.Range(-0.5f, 0f));
            jumpPos += transform.position;

            Transform trm = Managers.Instance.Pool.PopObject(PoolType.MusicPowerOrb, transform.position).transform;
            trm.DOJump(jumpPos, 1.5f, 2, 0.25f);

            yield return new WaitForFixedUpdate();
        }

        _poolable.PushThisObject();
    }
}
