using UnityEngine;

[ExecuteInEditMode]
public class GridTransformGroup : MonoBehaviour
{
    [SerializeField] private int columns;
    [SerializeField] private Vector2 cellSpacing;
    [SerializeField] private Vector2 cellSize;

    private void Start()
    {
        ArrangeChildren();
    }

    public void ArrangeChildren()
    {
        int count = transform.childCount;
        for(int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                int row = i / columns;
                int col = i % columns;

                Vector3 newPosition = new(col * (cellSize.x + cellSpacing.x), -row * (cellSize.y + cellSpacing.y), 0);
                child.localPosition = newPosition;
            }
        }
    }
}
