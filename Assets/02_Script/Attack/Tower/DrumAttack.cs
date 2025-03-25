using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DrumAttack : TowerAttack
{
    private float _range;
    private Color _endColor => new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);

    protected override void Setting()
    {
        base.Setting();
        transform.localScale = Vector3.one;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.PlayerColor;
    }

    public override void SettingTarget(Vector3 target, float musicPower, Tower attacker)
    {
        base.SettingTarget(target, musicPower, attacker);
        _damage = musicPower;
        _range = target.x;
        StartCoroutine(DrumCoroutine());
    }

    private IEnumerator DrumCoroutine()
    {
        _spriteRenderer.DOColor(_endColor, Managers.Instance.Game.UnitTime).SetEase(Ease.Linear);
        transform.DOScale(_range, Managers.Instance.Game.UnitTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(Managers.Instance.Game.UnitTime);
        _poolable.PushThisObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Hit(_damage, attacker: _attacker);
        }
    }
}
