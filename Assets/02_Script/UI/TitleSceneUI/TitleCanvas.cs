using UnityEngine;
using UnityEngine.UI;

public class TitleCanvas : BaseUI
{
    private enum EButtons
    {
        GameStartButton,
        OptionButton
    }

    private Button _gameStartButton;
    private Button _optionButton;

    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));
        
        _gameStartButton = Get<Button>((int)EButtons.GameStartButton);
        _optionButton = Get<Button>((int)EButtons.OptionButton);

        _gameStartButton.onClick.AddListener(HandleGameStart);
        _optionButton.onClick.AddListener(HandleOption);

        return true;
    }

    private void HandleGameStart()
    {
        
    }

    private void HandleOption()
    {
        Managers.Instance.UI.TitleRootUI.SetActiveCanvas("OptionCanvas", true);
    }
}
