using System.Collections;
using UnityEngine;

public enum OrbState
{
    Create,
    Enable,
    Disable
}

public class MusicPowerOrb : BaseObject
{
    private TrailRenderer _trailRenderer;
    private Core _core;
    private CircleCollider2D _collider;
    private PoolableObject _poolable;

    private OrbState _state;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _trailRenderer = GetComponent<TrailRenderer>();
        _core = Managers.Instance.Game.FindBaseInitScript<Core>();
        _collider = GetComponent<CircleCollider2D>();
        _poolable = GetComponent<PoolableObject>();
        _objectType = ObjectType.MusicPowerOrb;

        _trailRenderer.minVertexDistance = 0.005f;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _state = OrbState.Create;
        _collider.isTrigger = true;
        _collider.radius = 1.5f;
        _spriteRenderer.color = Managers.Instance.Game.PlayingMusic.EnemyColor;
        _trailRenderer.startColor = Managers.Instance.Game.PlayingMusic.EnemyColor;
        _trailRenderer.endColor = Color.clear;
        _trailRenderer.Clear();
        StartCoroutine(EnableCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && _state == OrbState.Create || collision.CompareTag("CircleArc") && _state == OrbState.Create)
        {
            _state = OrbState.Enable;
        }
        if(collision.CompareTag("Core") && _state == OrbState.Enable)
        {
            _state = OrbState.Disable;
            Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().AddMusicPower(1);
            _poolable.PushThisObject();
        }
    }

    private IEnumerator EnableCoroutine()
    {
        float time = Random.Range(0.15f, 0.5f);
        yield return new WaitForSeconds(time);
        _state = OrbState.Enable;
    }

    private void Update()
    {
        if(_state == OrbState.Enable)
        {
            Vector3 direction = _core.transform.position - transform.position;
            transform.position += direction.normalized * 10f * Time.deltaTime;
        }
    }
}
