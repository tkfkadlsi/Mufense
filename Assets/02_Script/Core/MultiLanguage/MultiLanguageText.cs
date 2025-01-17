using TMPro;
using UnityEngine;

public enum Language
{
    Korean,
    English
}

[RequireComponent(typeof(TextMeshProUGUI))]
public class MultiLanguageText : BaseInit
{
    [SerializeField] private string _koreanText;
    [SerializeField] private string _englishText;

    private TextMeshProUGUI _text;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _text = GetComponent<TextMeshProUGUI>();

        Managers.Instance.Game.ChangeLanguageEvent += HandleChangeLanguage;

        return true;
    }

    private void HandleChangeLanguage(Language language)
    {
        switch (language)
        {
            case Language.Korean:
                _text.text = _koreanText;
                break;
            case Language.English:
                _text.text = _englishText;
                break;
        }
    }
}
