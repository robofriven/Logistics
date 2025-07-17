using UnityEngine;

public class TileController : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public Sprite emptyTileSprite;
    public Sprite upTileSprite;
    public Sprite downTileSprite;
    public Sprite rightTileSprite;
    public Sprite leftTileSprite;    
    public Sprite wallTileSprite;
    public Sprite spawnerTileSprite;
    public Sprite destinationTileSprite;

    public Tile[,] tiles { get; private set; }
    private ushort nextTileId = 0;

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
        GameObject tilesObj = new GameObject("Tiles");

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                Tile tile = TileFactory.CreateTile(this, nextTileId++, position, TileType.Empty, tilesObj.transform);
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

    public Sprite GetTileSprite(TileType type)
    {
        switch (type)
        {
            case TileType.Empty: return emptyTileSprite;
            case TileType.Up: return upTileSprite;
            case TileType.Down: return downTileSprite;
            case TileType.Right: return rightTileSprite;
            case TileType.Left: return leftTileSprite;
            default: return null; // or a default sprite
        }
    }
}