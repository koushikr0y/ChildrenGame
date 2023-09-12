using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFood : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action OnDragStarted;
    public Action OnDragEnded;

    private Vector3 originalPos;
    private Vector3 originalScale;
    private Transform plate;
    private Transform canvasTransform;
    private RectTransform plateRectTransform;
    private Transform originalParent;

    private void Awake()
    {
        plate = GameObject.FindGameObjectWithTag("Plate").transform;
        canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
        plateRectTransform = plate.GetComponent<RectTransform>();
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPos = transform.position;
        originalScale = transform.localScale;
        LeanTween.scale(gameObject, originalScale * .8f, .5f).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position += (Vector3)data.delta;
        transform.SetParent(canvasTransform, true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(plateRectTransform, eventData.position))
        {
            Vector3 dropPosition = eventData.pointerCurrentRaycast.screenPosition;

            transform.position = dropPosition;
            transform.SetParent(plate, true);

            FoodContainer.Instane.spawnNewFood?.Invoke();
        }
        else
        {
            transform.position = originalPos;
            transform.SetParent(originalParent, true);
            LeanTween.scale(gameObject, originalScale, .8f).setEase(LeanTweenType.easeOutQuad);

        }
    }

    public void DestroyItemOnAnimEnd()
    {
        Destroy(this.gameObject);
    }
}
