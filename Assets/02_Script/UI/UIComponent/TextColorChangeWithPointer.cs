using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextColorChangeWithPointer : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _lerpTime = 0.25f;

    [SerializeField] private Color _changeColor = Color.white;
    private Color _originColor;

    private TextMeshProUGUI _text;

    private IEnumerator Coroutine;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _text = GetComponentInChildren<TextMeshProUGUI>();
        _originColor = _text.color;

        return true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Coroutine is not null)
        {
            StopCoroutine(Coroutine);
        }

        Coroutine = ChangeColor(_changeColor);
        StartCoroutine(Coroutine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Coroutine is not null)
        {
            StopCoroutine(Coroutine);
        }

        Coroutine = ChangeColor(_originColor);
        StartCoroutine(Coroutine);
    }

    private IEnumerator ChangeColor(Color color)
    {
        float t = 0f;

        while (t < _lerpTime)
        {
            yield return null;
            t += Time.deltaTime;

            _text.color = Color.Lerp(_text.color, color, t / _lerpTime);
        }
    }
}
