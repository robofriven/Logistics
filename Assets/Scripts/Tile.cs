using UnityEngine;

public class Tile : MonoBehaviour
{
    public ushort id { get; private set; }
    public Vector2Int position { get; private set; }
    public TileType type { get; private set; }
    public bool isOccupied { get; private set; }

    private TileGrid tileGrid;

    public void Initialize(ushort id, Vector2Int position, TileType type, TileGrid controller)
    {
        this.id = id;
        this.position = position;
        this.type = type;
        this.isOccupied = false;
        this.tileGrid = controller;
        transform.position = new Vector3(position.x, position.y, 0);
    }

    public void SetType(TileType newType)
    {
        this.type = newType;
        
        // Find the SpriteRenderer in the child object
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = tileGrid.GetTileSprite(type);
        }
        else
        {
            Debug.LogWarning($"No SpriteRenderer found in children of tile at {position}");
        }
    }

    public void SetIsOccupied(bool isOccupied)
    {
        this.isOccupied = isOccupied;
    }
}