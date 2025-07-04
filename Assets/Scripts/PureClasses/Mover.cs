using UnityEngine;

public static class Mover
{
    public static bool Move(GameWorld world, Thing thing, Tile tile)
    {
        Vector2Int direction = DirectionFromTileType(tile.type);
        if (direction == Vector2Int.zero)
            return false; // No movement for this tile type

        Vector2Int currentPosition = thing.position;
        Vector2Int newPosition = currentPosition + direction;
        Tile targetTile = world.GetTile(newPosition);

        if (targetTile == null || targetTile.isOccupied)
            return false; // Invalid move

        // Clear old tile
        world.GetTile(currentPosition)?.SetOccupied(false);

        // Move thing
        thing.SetPosition(newPosition);

        // Set new tile
        targetTile.SetOccupied(true);

        return true;
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
