using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SongButton : BaseUI, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IMusicPlayHandle
{
    private Music _music;
    private PoolableObject _poolable;
    private Image _image;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _poolable = GetComponent<PoolableObject>();
        _image = GetComponent<Image>();

        return true;
    }

    public void SettingButton(Music music)
    {
        _music = music;
        _image.color = music.BackGroundColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Managers.Instance.UI.GameRootUI.SongCanvas.SettingSongPanel(_music);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Managers.Instance.UI.GameRootUI.SongCanvas.ReleaseSongPanel();
    }

    public void PushThisObject()
    {
        _poolable.PushThisObject();
    }

    private void Update()
    {
        if(_music.Clip.length < Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().MusicTime)
        {
            _poolable.PushThisObject();
        }
    }

    public void SettingColor(Music music)
    {
        _image.DOColor(music.PlayerColor, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Instance.Game.FindBaseInitScript<MusicPlayer>().ChangeMusic(_music);
        Managers.Instance.UI.GameRootUI.SetActiveCanvas("SongCanvas", false);
    }
}
