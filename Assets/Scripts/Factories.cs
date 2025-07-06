using UnityEngine;

public static class SpawnerFactory
{
    public static void CreateSpawner(SpawnerData data, ThingPool thingPool, Destination destination, GameController gameController)
    {
        // Create the main spawner GameObject
        GameObject spawnerGO = new GameObject(data.spawnerName);
        spawnerGO.transform.position = new Vector3(data.position.x, data.position.y, 0f);
        
        // Add and configure the Spawner component
        Spawner spawner = spawnerGO.AddComponent<Spawner>();
        spawner.data = data;
        spawner.thingPool = thingPool;
        spawner.targetDestination = destination;
        
        // Create the sprite child object
        GameObject spriteObj = new GameObject("Sprite");
        spriteObj.transform.parent = spawnerGO.transform;
        spriteObj.transform.position = new Vector3(data.position.x + 0.5f, data.position.y + 0.5f, 0f);
        SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
        sr.sprite = data.spawnerSprite;
        sr.sortingLayerName = "Spawners";
        
        // Register with game controller
        gameController.RegisterSpawner(spawner);
    }
}

public static class DestinationFactory
{
    public static Destination CreateDestination(SpawnerData data, ThingPool thingPool)
    {
        GameObject destinationObj = new GameObject($"{data.spawnerName} Destination");
        destinationObj.transform.position = new Vector3(data.thingDestinationCoords.x, data.thingDestinationCoords.y, 0);
        
        // Add and configure the Destination component
        Destination destination = destinationObj.AddComponent<Destination>();
        destination.position = data.thingDestinationCoords;
        destination.thingPool = thingPool;
        
        // Create the sprite child object
        GameObject spriteObj = new GameObject("Sprite");
        spriteObj.transform.parent = destinationObj.transform;
        spriteObj.transform.position = new Vector3(data.thingDestinationCoords.x + 0.5f, data.thingDestinationCoords.y + 0.5f, 0f);
        SpriteRenderer destinationSr = spriteObj.AddComponent<SpriteRenderer>();
        destinationSr.sprite = data.destinationSprite;
        destinationSr.sortingLayerName = "Destinations";
        
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
        
        // Create the sprite child object, centered in the tile
        GameObject spriteObj = new GameObject("Sprite");
        spriteObj.transform.parent = tileObj.transform;
        spriteObj.transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, 0f);
        SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
        sr.sprite = controller.GetTileSprite(type);
        sr.sortingLayerName = "Tiles";
        
        tile.Initialize(controller, position, type);
        
        return tile;
    }
}