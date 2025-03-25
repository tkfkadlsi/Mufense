using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColorChangeWithPointer : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _lerpTime = 0.25f;

    [SerializeField] private Color _changeColor = Color.white;
    private Color _originColor;

    private Image _image;

    private IEnumerator Coroutine;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _image = GetComponent<Image>();
        _originColor = _image.color;

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

            _image.color = Color.Lerp(_image.color, color, t / _lerpTime);
        }
    }
}
