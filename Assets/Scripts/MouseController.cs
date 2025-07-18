using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Camera mainCamera;
    public TileGrid tileGrid;

    private TileType selectedTileType = TileType.Up; // Default to placing Up` tiles
    private bool isDragging = false;
    private Vector2Int startDragPosition;

    // --- Public Methods for UI Buttons ---
    public void SelectEmptyType() => selectedTileType = TileType.Empty;
    public void SelectUpType() => selectedTileType = TileType.Up;
    public void SelectDownType() => selectedTileType = TileType.Down;
    public void SelectLeftType() => selectedTileType = TileType.Left;
    public void SelectRightType() => selectedTileType = TileType.Right;
    public void SelectWallType() => selectedTileType = TileType.Wall;

    void Update()
    {
        // Prevents clicking "through" the UI
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startDragPosition = GetMouseGridPosition();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                Vector2Int endDragPosition = GetMouseGridPosition();
                ApplyTileChanges(startDragPosition, endDragPosition);
                isDragging = false;
            }
        }
    }

    private Vector2Int GetMouseGridPosition()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2Int(Mathf.FloorToInt(mouseWorldPos.x), Mathf.FloorToInt(mouseWorldPos.y));
    }

    private void ApplyTileChanges(Vector2Int start, Vector2Int end)
    {
        Vector2Int delta = end - start;

        // Determine the primary axis of the drag
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            // Horizontal drag
            int y = start.y;
            int startX = Mathf.Min(start.x, end.x);
            int endX = Mathf.Max(start.x, end.x);
            for (int x = startX; x <= endX; x++)
            {
                SetTile(new Vector2Int(x, y));
            }
        }
        else
        {
            // Vertical drag (or a single click)
            int x = start.x;
            int startY = Mathf.Min(start.y, end.y);
            int endY = Mathf.Max(start.y, end.y);
            for (int y = startY; y <= endY; y++)
            {
                SetTile(new Vector2Int(x, y));
            }
        }
    }

    private void SetTile(Vector2Int position)
    {
        Tile tile = tileGrid.GetTile(position);
        // Only change the tile if it exists and is not occupied by a spawner/destination
        if (tile != null && !tile.isOccupied)
        {
            tile.SetType(selectedTileType);
        }
    }
}