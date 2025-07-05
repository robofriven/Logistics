using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public Tile[,] tiles {get; private set;}

    public void InitializeWorld()
    {
        if (tiles != null)
        {
            Debug.LogWarning("World is already initialized.");
            return;
        }
        if (width <= 0 || height <= 0)
        {
            Debug.LogError("Invalid world dimensions.");
            return;
        }


        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                Tile tile = new Tile(position, TileType.Empty); // Assuming all tiles are usable initially
                tiles[x, y] = tile;
            }
        }
    }

    public Tile GetTile(Vector2Int position)
    {
        if (position.x < 0 || position.x >= width || position.y < 0 || position.y >= height)
        {
            Debug.LogWarning($"Position {position} is out of bounds for the world of size {width}x{height}.");
            return null; // Out of bounds
        }
        return tiles[position.x, position.y];
    }
}

public class Tile
{
    public Vector2Int position { get; private set; }
    public TileType type { get; private set; }
    public bool isOccupied{ get; private set; }

    public Tile(Vector2Int position, TileType type)
    {
        this.position = position;
        this.type = type;
        this.isOccupied = false; // Initially not occupied
    }

    public void SetType(TileType newType)
    {
        type = newType;
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }
}

public enum TileType
{
    Empty,
    Wall,
    Right,
    Left,
    Up,
    Down
}