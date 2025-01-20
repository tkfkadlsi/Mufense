using UnityEngine;
using UnityEngine.EventSystems;

public class SizeChangeWithPointer : BaseUI, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float SizeMultiplyWithPointer = 1.1f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * SizeMultiplyWithPointer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
