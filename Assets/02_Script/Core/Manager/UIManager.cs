using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TitleRootUI _titleRootUI;

    public TitleRootUI TitleRootUI
    {
        get
        {
            if( _titleRootUI == null )
                _titleRootUI = FindAnyObjectByType<TitleRootUI>();

            if (_titleRootUI == null)
                Debug.LogError($"[사람이냐] : 현재 TitleScene이 아닙니다.");

            return _titleRootUI;
        }
    }
}
