using UnityEngine;
using UnityEngine.EventSystems;

public class SizeChangeWithPointer : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    private float SizeMultiplyWithPointer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * SizeMultiplyWithPointer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
