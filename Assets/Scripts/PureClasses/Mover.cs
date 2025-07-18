using UnityEngine;

public static class Mover
{
    public static bool Move(TileGrid world, TileObjectMap tileObjectMap, Thing thing)
    {
        Tile currentTile = world.GetTile(thing.position);
        if (currentTile == null)
        {
            Debug.LogError($"Mover: Could not find tile at {thing.position} for {thing.name}");
            return false;
        }

        Vector2Int direction = DirectionFromTileType(currentTile.type);
        if (direction == Vector2Int.zero)
            return false; // No movement for this tile type

        Vector2Int newPosition = thing.position + direction;
        Tile targetTile = world.GetTile(newPosition);

        if (targetTile == null)
            return false; // Invalid move, off the map

        // The core logic change: use the TileObjectMap to check for occupancy and perform the move.
        if (tileObjectMap.TryMoveGameObject(currentTile, targetTile))
        {
            // If the map move was successful, then update the Thing's internal position.
            thing.MoveTo(newPosition);
            return true;
        }

        return false; // Target tile was occupied or something went wrong.
    }

    private static Vector2Int DirectionFromTileType(TileType type)
    {
        switch (type)
        {
            case TileType.Right: return Vector2Int.right;
            case TileType.Left:  return Vector2Int.left;
            case TileType.Up:    return Vector2Int.up;
            case TileType.Down:  return Vector2Int.down;
            default:             return Vector2Int.zero;
        }
    }
}
