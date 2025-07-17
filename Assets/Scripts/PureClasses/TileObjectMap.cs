using UnityEngine;
using System.Collections.Generic;

public class TileObjectMap 
{
    private Dictionary<Tile, GameObject> tileObjectMap = new Dictionary<Tile, GameObject>();

    public bool TryAddTileObject(Tile tile, GameObject obj)
    {
        if (tileObjectMap.ContainsKey(tile))
        {
            return false; // Tile is already occupied
        }
        tileObjectMap[tile] = obj;
        return true;
    }

    public GameObject GetTileObject(Tile tile)
    {
        tileObjectMap.TryGetValue(tile, out GameObject obj);
        return obj;
    }

    public void RemoveTileObject(Tile tile)
    {
        tileObjectMap.Remove(tile);
    }

    public bool TryMoveGameObject(Tile fromTile, Tile toTile)
    {
        // Check if there's an object to move and the destination is free
        if (tileObjectMap.ContainsKey(fromTile) && !tileObjectMap.ContainsKey(toTile))
        {
            GameObject obj = tileObjectMap[fromTile];
            tileObjectMap.Remove(fromTile);
            tileObjectMap[toTile] = obj;
            return true;
        }
        return false;
    }
}
