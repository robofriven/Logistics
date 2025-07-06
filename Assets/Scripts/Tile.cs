using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int position;
    public TileType type;
    public bool isOccupied;

    private TileController tileController;

    public void Initialize(TileController tileController, Vector2Int position, TileType type)
    {
        this.tileController = tileController;
        this.position = position;
        this.type = type;
        this.isOccupied = false;
        transform.position = new Vector3(position.x, position.y, 0);
    }

    public void SetType(TileType newType)
    {
        type = newType;
        
        // Find the SpriteRenderer in the child object
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = tileController.GetTileSprite(type);
        }
        else
        {
            Debug.LogWarning($"No SpriteRenderer found in children of tile at {position}");
        }
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }
}