using UnityEngine;

public class Spawner : MonoBehaviour, IOnTick
{
    public ushort id { get; private set; }
    public ThingPool thingPool { get; private set; }
    public SpawnerData data { get; private set; }
    public Destination targetDestination { get; private set; }
    public Color color { get; private set; }
    public string spawnerName { get; private set; }
    public Sprite spawnerSprite { get; private set; }
    public Sprite destinationSprite { get; private set; }
    public byte ticksPerSpawn { get; private set; }
    public Vector2Int position { get; private set; }
    public Vector2Int direction { get; private set; }

    private GameController gameController;

    // todo: Have this populate fields from data instead of accessing them directly
    public void Initialize(SpawnerData data, Vector2Int position, Vector2Int direction, ThingPool thingPool, Destination destination, Color color)
    {
        gameController = FindFirstObjectByType<GameController>();
        this.id = gameController.GetNextSpawnerId();
        this.data = data;
        this.position = position;
        this.direction = direction;
        this.thingPool = thingPool;
        this.targetDestination = destination;
        this.color = color;

        // Populate fields from data
        this.spawnerName = data.spawnerName;
        this.spawnerSprite = data.spawnerSprite;
        this.destinationSprite = data.destinationSprite;
        this.ticksPerSpawn = data.ticksPerSpawn;
        
        // One-time lookup instead of dragging references around
        gameController = FindFirstObjectByType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController not found! It exists, but you can't find it");
            return;
        }
    }

    public void OnTick(int tick)
    {
        if (data == null || thingPool == null) return;

        if (tick % data.ticksPerSpawn == 0)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector2Int spawnPos = position + direction;

        // Get TileGrid and TileObjectMap from GameController
        TileGrid tileGrid = gameController.tileGrid;
        TileObjectMap tileObjectMap = gameController.tileObjectMap;

        Tile spawnTile = tileGrid.GetTile(spawnPos);

        if (spawnTile == null)
        {
            // This can happen if the spawner is near the edge of the map.
            // It's not necessarily an error, but something to be aware of.
            return;
        }

        // Use the map to see if anything is on the target tile.
        if (tileObjectMap.GetTileObject(spawnTile) != null)
        {
            // Can't spawn, tile is occupied.
            return;
        }

        // Get next ID from game controller
        ushort thingId = gameController.GetNextThingId();

        // Get thing from pool and stamp it with info
        Thing thing = thingPool.GetThing(thingId, data.thingToSpawn, spawnPos, targetDestination.position, targetDestination);

        // Add the new object to our map!
        if (!tileObjectMap.TryAddTileObject(spawnTile, thing.gameObject))
        {
            // This should be impossible since we just checked, but it's good practice.
            Debug.LogError($"Could not add {thing.name} to map at {spawnPos}. Returning to pool.");
            thingPool.ReturnThing(thing);
            return;
        }

        Debug.Log($"Spawner {id} spawned {thing.name} at {spawnPos}");
    }
}