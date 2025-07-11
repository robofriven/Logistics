using UnityEngine;

public static class SpriteHelper
{
    public static void AttachSprite(GameObject parent, Vector2Int position, Sprite sprite, string sortingLayer, Color color = default)
    {
        GameObject spriteObj = new GameObject("Sprite");
        spriteObj.transform.parent = parent.transform;
        spriteObj.transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, 0f);
        
        SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingLayerName = sortingLayer;
        if (color != default)
        {
            sr.color = color; // Set the color if provided
        }
    }
}

public static class SpawnerFactory
{
    public static void CreateSpawner(SpawnerData data, Vector2Int position, Vector2Int direction, ThingPool thingPool, Destination destination, GameController gameController, Color color = default)
    {
        // Create the main spawner GameObject
        GameObject spawnerGO = new GameObject(data.spawnerName);
        spawnerGO.transform.position = new Vector3(position.x, position.y, 0f);
        
        // Add and configure the Spawner component
        Spawner spawner = spawnerGO.AddComponent<Spawner>();
        spawner.Initialize(data, position, direction, thingPool, destination, color);
        
        // One line instead of 6!
        SpriteHelper.AttachSprite(spawnerGO, position, data.spawnerSprite, "Spawners", color);
        
        // Register with game controller
        gameController.RegisterSpawner(spawner);
    }
}

public static class DestinationFactory
{
    public static Destination CreateDestination(DestinationData data, Vector2Int position, ThingPool thingPool, Color color = default)
    {
        GameObject destinationObj = new GameObject($"Destination_{position.x}_{position.y}");
        destinationObj.transform.position = new Vector3(position.x, position.y, 0);
        
        // Add and configure the Destination component
        Destination destination = destinationObj.AddComponent<Destination>();
        destination.Initialize(data, position, thingPool, color);

        SpriteHelper.AttachSprite(destinationObj, position, data.destinationSprite, "Spawners", color);

        return destination;
    }
}

public static class TileFactory
{
    public static Tile CreateTile(TileController controller, Vector2Int position, TileType type, Transform parent = null)
    {
        GameObject tileObj = new GameObject($"Tile_{position.x}_{position.y}");
        if (parent != null)
        {
            tileObj.transform.parent = parent;
        }
        tileObj.transform.position = new Vector3(position.x, position.y, 0);
        
        Tile tile = tileObj.AddComponent<Tile>();
        
        SpriteHelper.AttachSprite(tileObj, position, controller.GetTileSprite(type), "Tiles");
        
        tile.Initialize(controller, position, type);
        
        return tile;
    }
}