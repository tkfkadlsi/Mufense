using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TowerGuide : BaseObject
{
    [SerializedDictionary("Type", "Sprite")] public SerializedDictionary<TowerType, Sprite> _spriteDictionary;

    private TowerSpawner _towerSpawner;
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;

    private List<Collider2D> _colliders = new List<Collider2D>();

    private IEnumerator BuildCor;
    private bool _canBuild;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _towerSpawner = Managers.Instance.Game.FindBaseInitScript<TowerSpawner>();

        _objectType = ObjectType.TowerGuide;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _canBuild = true;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _rigidbody.gravityScale = 0f;
        _collider.isTrigger = true;
        _collider.radius = 1.5f;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && BuildCor is not null)
        {
            StopCoroutine(BuildCor);
            _towerSpawner.SetSpawnState(TowerSpawnState.None, TowerType.None, 0);
            Managers.Instance.UI.GameRootUI.MainCanvas.SetBuildButtonActive(true);
        }
    }

    public void BuildTower(TowerType type, int cost)
    {
        BuildCor = BuildCoroutine(type, cost);
        StartCoroutine(BuildCor);
    }

    private IEnumerator BuildCoroutine(TowerType type, int cost)
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && _canBuild);
        Vector3 buildPos = transform.position;

        Tower newTower = Managers.Instance.Pool.PopObject(PoolType.Tower, buildPos).GetComponent<Tower>();
        newTower.TowerSetting(type, _spriteDictionary[type]);
        Managers.Instance.Game.FindBaseInitScript<MusicPowerChest>().RemoveMusicPower(cost);
        _towerSpawner.SetSpawnState(TowerSpawnState.None, TowerType.None, 0);
        Managers.Instance.UI.GameRootUI.MainCanvas.SetBuildButtonActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Tower") || collision.CompareTag("Core"))
        {
            _colliders.Add(collision);

            if (_colliders.Count == 0)
            {
                _spriteRenderer.color = Color.green;
                _canBuild = true;
            }
            else
            {
                _spriteRenderer.color = Color.red;
                _canBuild = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_colliders.Contains(collision))
        {
            _colliders.Remove(collision);

            if (_colliders.Count == 0)
            {
                _spriteRenderer.color = Color.green;
                _canBuild = true;
            }
            else
            {
                _spriteRenderer.color = Color.red;
                _canBuild = false;
            }
        }
    }
}
