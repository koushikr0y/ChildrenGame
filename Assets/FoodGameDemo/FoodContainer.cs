using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class FoodContainer : MonoBehaviour, IDropHandler
{
    public static FoodContainer Instane;

    public Action spawnNewFood;

    [SerializeField] private List<DraggableFood> draggedItems = new List<DraggableFood>();


    private void Awake()
    {
        Instane = this;
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableFood draggableFood = eventData.pointerDrag.GetComponent<DraggableFood>();

        if (draggableFood != null)
        {
            draggedItems.Add(draggableFood);

            draggableFood.transform.SetParent(transform);

            draggableFood.transform.localPosition = Vector3.zero;
            draggableFood.transform.SetAsFirstSibling();
        }
    }
    private void OnDisable()
    {
        draggedItems.Clear();
    }
}
