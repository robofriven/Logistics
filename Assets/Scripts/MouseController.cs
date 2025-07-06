using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Camera mainCamera;
    public TileController tileController; // was: public GameWorld gameWorld;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = new Vector2Int(Mathf.FloorToInt(mouseWorldPos.x), Mathf.FloorToInt(mouseWorldPos.y));
            Tile tile = tileController.GetTile(gridPos);

            if (tile != null)
            {
                CycleTileType(tile);
            }
        }
    }

    private void CycleTileType(Tile tile)
    {
        TileType[] types = (TileType[])System.Enum.GetValues(typeof(TileType));
        int currentIndex = System.Array.IndexOf(types, tile.type);
        int nextIndex = (currentIndex + 1) % types.Length;
        tile.SetType(types[nextIndex]);
    }
}