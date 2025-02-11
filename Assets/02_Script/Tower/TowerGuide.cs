using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TowerGuide : BaseObject
{
    [SerializedDictionary("Type", "Sprite")] public SerializedDictionary<TowerType, Sprite> _spriteDictionary;
    [SerializedDictionary("Type", "Range")] public SerializedDictionary<TowerType, float> _rangeDictionary;
    [SerializedDictionary("Type", "Cooltime")] public SerializedDictionary<TowerType, int> _cooltimeDictionary;

    private TowerSpawner _towerSpawner;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private List<Collider2D> _colliders = new List<Collider2D>();

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
        _collider = GetComponent<Collider2D>();
        _canBuild = false;

        return true;
    }

    protected override void Setting()
    {
        base.Setting();

        _rigidbody.gravityScale = 0f;
        _collider.isTrigger = true;
    }

    public void BuildTower(TowerType type)
    {
        StartCoroutine(BuildCoroutine(type));
    }

    private IEnumerator BuildCoroutine(TowerType type)
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && _canBuild);
        Vector3 buildPos = transform.position;

        Tower newTower = Managers.Instance.Pool.PopObject(PoolType.Tower, buildPos).GetComponent<Tower>();
        newTower.TowerSetting(type, _spriteDictionary[type], _cooltimeDictionary[type], _rangeDictionary[type]);
        _towerSpawner.SetSpawnState(TowerSpawnState.None, TowerType.None);
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
