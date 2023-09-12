using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    public GameObject objectPrefab;

    private bool isDragging = false;
    private GameObject currentObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverImage(Input.mousePosition))
            {
                currentObject = Instantiate(objectPrefab, Input.mousePosition, Quaternion.identity);
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }

    private bool IsPointerOverImage(Vector2 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

        return hit.collider != null && hit.collider.gameObject == gameObject;
    }
}