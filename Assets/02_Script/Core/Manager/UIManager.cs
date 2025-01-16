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
                Debug.LogError($"[����̳�] : ���� TitleScene�� �ƴմϴ�.");

            return _titleRootUI;
        }
    }
}
