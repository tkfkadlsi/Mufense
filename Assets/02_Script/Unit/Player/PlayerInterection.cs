using TMPro;
using UnityEngine;

public class PlayerInterection : BaseInit, IMusicPlayHandle
{
    private int _collisionCount;
    private Collider2D[] _colliders = new Collider2D[20];
    private ContactFilter2D _filter = new ContactFilter2D();

    private Canvas _canvas;
    private TextMeshProUGUI _text;
    private Player _player;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _canvas = GetComponentInChildren<Canvas>();
        _text = gameObject.FindChild<TextMeshProUGUI>("", true);
        _player = FindAnyObjectByType<Player>();
        _collisionCount = 0;
        _filter.NoFilter();

        return true;
    }

    protected override void Setting()
    {
        base.Setting();
        _canvas.enabled = false;
        _canvas.sortingOrder = 999;
        Managers.Instance.Game.InputReader.InterectionEvent += Interection;
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic += SettingColor;
    }

    protected override void Release()
    {
        if (Managers.Instance != null)
        {
            Managers.Instance.Game.InputReader.InterectionEvent -= Interection;
            Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().PlayMusic -= SettingColor;
        }

        base.Release();
    }

    private void Update()
    {
        transform.position = _player.transform.position;
    }

    private void Interection()
    {
        int count = Physics2D.OverlapCircle(transform.position, 1.1f, _filter, _colliders);
        _colliders = Physics2D.OverlapCircleAll(transform.position, 1.1f);

        for (int i = 0; i < count; i++)
        {
            if (_colliders[i].CompareTag("Tower"))
            {
                Tower tower = _colliders[i].GetComponent<Tower>();
                tower.Interection();
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower") || collision.CompareTag("Treasure"))
        {
            _collisionCount++;
        }

        if (_collisionCount == 0)
        {
            _canvas.enabled = false;
        }
        else
        {
            _canvas.enabled = true;
            _text.color = Managers.Instance.Game.PlayingMusic.TextColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower") || collision.CompareTag("Treasure"))
        {
            _collisionCount--;
        }

        if (_collisionCount == 0)
        {
            _canvas.enabled = false;
        }
        else
        {
            _canvas.enabled = true;
        }
    }

    public void SettingColor(Music music)
    {
        if (_text == null) return;
        _text.color = music.TextColor;
    }
}
