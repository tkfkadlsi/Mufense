using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Core : Unit, IHealth, IMusicPlayHandle
{
    private Slider _hpSlider;
    private IEnumerator HitedCoroutine;
    private float hp = 0f;
    public float HP 
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;

            if(hp <= 0f)
            {
                Die();
            }
            else
            {
                if (HitedCoroutine is not null)
                {
                    StopCoroutine(HitedCoroutine);
                }
                HitedCoroutine = Hited();
                StartCoroutine(HitedCoroutine);
            }
        }
    }

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _objectType = ObjectType.Core;

        _hpSlider = transform.parent.gameObject.FindChild<Slider>("HPSlider", true);
        _hpSlider.maxValue = 100f;
        _hpSlider.value = 100f;
        HP = 100;

        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;

        return true;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.Release();
    }

    private void Update()
    {
        transform.Rotate(0, 0, (120f / Managers.Instance.Game.UnitTime) * Time.deltaTime);
    }

    protected override void Setting()
    {
        base.Setting();
    }

    public void CircleArcAttack()
    {
        Managers.Instance.Pool.PopObject(PoolType.CircleArc, transform.position);
    }

    public void SettingColor(Music music)
    {
        _spriteRenderer.DOColor(music.PlayerColor, 1f);
    }

    public void Die(bool drop = false)
    {
        //게임 오버
    }

    public IEnumerator Hited()
    {
        _hpSlider.value = HP;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;


        float t = 0f;
        float lerpTime = 0.5f;

        while(t < lerpTime)
        {
            yield return null;
            t += Time.deltaTime;

            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Managers.Instance.Game.PlayingMusic.PlayerColor, t / lerpTime);
        }
    }
}