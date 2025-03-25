using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultCanvas : BaseUI
{
    enum EButtons
    {
        GoTitleButton
    }

    private Button _goTitleButton;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Bind<Button>(typeof(EButtons));

        _goTitleButton = Get<Button>((int)EButtons.GoTitleButton);

        _goTitleButton.onClick.AddListener(HandleGoTitleButton);

        return true;
    }

    private void HandleGoTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
