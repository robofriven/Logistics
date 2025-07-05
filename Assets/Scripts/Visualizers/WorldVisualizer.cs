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
            GameObject tileObj = new GameObject($"{tile.position.x},{tile.position.y}");
            tileObj.transform.parent = tilesParent.transform;
            tileObj.transform.position = new Vector3(tile.position.x, tile.position.y, 0);
            TileVisual visual = tileObj.AddComponent<TileVisual>();
            visual.Initialize(
                tile,
                emptyTilePrefab,
                occupiedTilePrefab,
                rightTilePrefab,
                leftTilePrefab,
                upTilePrefab,
                downTilePrefab,
                wallTilePrefab
            );
            tileVisuals[tile] = visual;
        }
    }

    public void UpdateThings()
    {
        foreach (var kvp in tileVisuals)
        {
            var tile = kvp.Key;
            var visual = kvp.Value;
            visual.UpdateVisual(tile.isOccupied, tile.type);
        }
    }
}
