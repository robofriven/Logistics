using UnityEngine;
using System.Collections.Generic;

public class WorldVisualizer : MonoBehaviour
{
    public GameObject emptyTilePrefab;
    public GameObject occupiedTilePrefab;
    public GameObject rightTilePrefab;
    public GameObject leftTilePrefab;
    public GameObject upTilePrefab;
    public GameObject downTilePrefab;
    public GameObject wallTilePrefab;

    private GameWorld gameWorld;
    private Dictionary<Tile, TileVisual> tileVisuals = new();

    public void Initialize(GameWorld gameWorld)
    {
        this.gameWorld = gameWorld;

        // Create a parent GameObject for all tiles
        GameObject tilesParent = new GameObject("Tiles");

        foreach (Tile tile in gameWorld.tiles)
        {
            if (tile == null) continue;
            GameObject prefab = GetPrefabForTileType(tile.type);
            GameObject tileObj = Instantiate(
                prefab,
                new Vector3(tile.position.x, tile.position.y, 0),
                Quaternion.identity,
                tilesParent.transform // Set parent
            );
            tileObj.name = $"{tile.position.x},{tile.position.y}";
            TileVisual visual = tileObj.AddComponent<TileVisual>();
            visual.Initialize(tile, emptyTilePrefab, occupiedTilePrefab);
            tileVisuals[tile] = visual;
        }
    }

    private GameObject GetPrefabForTileType(TileType type)
    {
        switch (type)
        {
            case TileType.Right: return rightTilePrefab;
            case TileType.Left:  return leftTilePrefab;
            case TileType.Up:    return upTilePrefab;
            case TileType.Down:  return downTilePrefab;
            case TileType.Wall:  return wallTilePrefab;
            case TileType.Empty:
            default:             return emptyTilePrefab;
        }
    }

    public void UpdateThings()
    {
        foreach (var kvp in tileVisuals)
        {
            var tile = kvp.Key;
            var visual = kvp.Value;
            visual.UpdateVisual(tile.isOccupied);
        }
    }
}
