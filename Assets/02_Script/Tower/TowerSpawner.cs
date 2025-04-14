using UnityEngine;

public enum TowerSpawnState
{
    None,
    Create
}

public class TowerSpawner : BaseInit
{
    private TowerSpawnState _state;

    [SerializeField] private TowerGuide _guide;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _guide.gameObject.SetActive(false);

        return true;
    }

    public void SetSpawnState(TowerSpawnState state, TowerType towerType, int cost)
    {
        _state = state;

        if (_state == TowerSpawnState.Create)
        {
            _guide.gameObject.SetActive(true);
            _guide.BuildTower(towerType, cost);
        }
        else
        {
            _guide.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_state == TowerSpawnState.Create)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _guide.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
        }
    }
}
