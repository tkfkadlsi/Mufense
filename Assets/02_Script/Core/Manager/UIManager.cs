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
                    Debug.LogError($"[사람이냐] : 현재 TitleScene이 아닙니다.");
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
                    Debug.LogError($"[사람이냐] : 현재 GameScene이 아닙니다.");
            }

            return _gameRootUI;
        }
    }
}
