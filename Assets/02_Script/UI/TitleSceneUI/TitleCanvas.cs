using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleCanvas : BaseUI
{
    private enum EButtons
    {
        GameStartButton,
        OptionButton,
        ExitButton
    }

    private Button _gameStartButton;
    private Button _optionButton;
    private Button _exitButton;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));

        _gameStartButton = Get<Button>((int)EButtons.GameStartButton);
        _optionButton = Get<Button>((int)EButtons.OptionButton);
        _exitButton = Get<Button>((int)EButtons.ExitButton);

        _gameStartButton.onClick.AddListener(HandleGameStart);
        _optionButton.onClick.AddListener(HandleOption);
        _exitButton.onClick.AddListener(HandleExit);

        return true;
    }

    private void HandleGameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void HandleOption()
    {
        Managers.Instance.UI.TitleRootUI.SetActiveCanvas("OptionCanvas", true);
    }

    private void HandleExit()
    {
        Application.Quit();
    }
}
