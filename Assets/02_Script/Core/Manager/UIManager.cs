using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TitleRootUI _titleRootUI;

    public TitleRootUI TitleRootUI
    {
        get
        {
            if( _titleRootUI == null )
            {
                _titleRootUI = FindAnyObjectByType<TitleRootUI>();

                if (_titleRootUI == null)
                    Debug.LogError($"[����̳�] : ���� TitleScene�� �ƴմϴ�.");
            }

            return _titleRootUI;
        }
    }


    private GameRootUI _gameRootUI;

    public GameRootUI GameRootUI
    {
        get
        {
            if (_gameRootUI == null)
            {
                _gameRootUI = FindAnyObjectByType<GameRootUI>();

                if (_gameRootUI == null)
                    Debug.LogError($"[����̳�] : ���� GameScene�� �ƴմϴ�.");
            }

            return _gameRootUI;
        }
    }
}
