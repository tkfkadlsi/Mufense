using UnityEngine;
using System.Collections.Generic;

public class PlayerInterection : BaseInit
{
    private List<Collider2D> _colliders = new List<Collider2D>();
    private Collider2D _collider;
    private Canvas _canvas;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        _collider = GetComponent<Collider2D>();
        _canvas = GetComponentInChildren<Canvas>();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _canvas.enabled = false;
        Managers.Instance.Game.InputReader.InterectionEvent += Interection;
    }

    protected override void Release()
    {
        Managers.Instance.Game.InputReader.InterectionEvent -= Interection;

        base.Release();
    }

    private void Update()
    {
        Physics2D.OverlapCollider(_collider, _colliders);

        if(_colliders.Count > 0)
        {
            _canvas.enabled = true;
        }
        else
        {
            _canvas.enabled = false;
        }
    }

    private void Interection()
    {


        foreach(Collider2D collider in _colliders)
        {
            if(collider.CompareTag("Tower"))
            {
                Tower tower = collider.GetComponent<Tower>();
                tower.Interection();
            }
            else if(collider.CompareTag("Treasure"))
            {
                Treasure treasure = collider.GetComponent<Treasure>();
                treasure.Interection();
            }
        }
    }
}
