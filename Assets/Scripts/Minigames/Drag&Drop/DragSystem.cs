using UnityEngine;

public class DragSystem : MonoBehaviour
{
    public GameObject[] draggableObjects;

    public GameObject targetObject;

    private BoxCollider2D dragBounds;
    private GameObject currentlyDragging;
    private Vector3 dragOffset;

    private void Start()
    {
        dragBounds = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleDragging();
    }

    private void HandleDragging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Start dragging if clicking on a draggable object
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentlyDragging = GetDraggableUnderMouse(mousePos);

            if (currentlyDragging != null)
            {
                dragOffset = currentlyDragging.transform.position - (Vector3)mousePos;

                // Check if the clicked object is the target
                if (currentlyDragging == targetObject)
                {
                    SetAllDraggableObjectsTransparent();
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && currentlyDragging != null)
        {
            // Stop dragging when mouse button is released
            currentlyDragging = null;
        }
        else if (currentlyDragging != null)
        {
            // Continue dragging within bounds
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + dragOffset;
            Vector2 clampedPosition = ClampToBounds(newPosition);
            currentlyDragging.transform.position = clampedPosition;
        }
    }

    private GameObject GetDraggableUnderMouse(Vector2 position)
    {
        foreach (GameObject obj in draggableObjects)
        {
            if (obj != null && obj.GetComponent<Collider2D>().OverlapPoint(position))
            {
                return obj;
            }
        }
        return null;
    }

    private Vector2 ClampToBounds(Vector2 position)
    {
        Bounds bounds = dragBounds.bounds;
        float clampedX = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        return new Vector2(clampedX, clampedY);
    }

    private void SetAllDraggableObjectsTransparent()
    {
        foreach (GameObject obj in draggableObjects)
        {
            if (obj != targetObject)
            {
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    Color transparentColor = renderer.color;
                    transparentColor.a = 0.5f;
                    renderer.color = transparentColor;
                }
            }
        }
    }
}
