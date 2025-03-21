using UnityEngine;
using System.Collections.Generic;

public class PlayerInterection : BaseInit
{
    private Collider2D[] _colliders = new Collider2D[5];
    private ContactFilter2D _filter = new ContactFilter2D();

    private Canvas _canvas;
    private Player _player;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _canvas = GetComponentInChildren<Canvas>();
        _player = FindAnyObjectByType<Player>();
        _filter.NoFilter();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _canvas.enabled = false;
        _canvas.sortingOrder = 999;
        Managers.Instance.Game.InputReader.InterectionEvent += Interection;
    }

    protected override void Release()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.InputReader.InterectionEvent -= Interection;
        }

        base.Release();
    }

    private void Update()
    {
        transform.position = _player.transform.position;
        int count = Physics2D.OverlapCircle(transform.position, 1.1f, _filter, _colliders);

        for(int i = 0; i < count; i++)
        {
            if (_colliders[i].CompareTag("Tower") || _colliders[i].CompareTag("Treasure"))
            {
                _canvas.enabled = true;
                return;
            }
        }
        _canvas.enabled = false;
    }

    private void Interection()
    {
        int count = Physics2D.OverlapCircle(transform.position, 1.1f, _filter, _colliders);
        _colliders = Physics2D.OverlapCircleAll(transform.position, 1.1f);

        for(int i = 0; i < count; i++)
        {
            if (_colliders[i].CompareTag("Tower"))
            {
                Tower tower = _colliders[i].GetComponent<Tower>();
                tower.Interection();
            }
            else if (_colliders[i].CompareTag("Treasure"))
            {
                Treasure treasure = _colliders[i].GetComponent<Treasure>();
                treasure.Interection();
            }
        }
    }
}
