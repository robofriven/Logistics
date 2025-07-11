using UnityEngine;

public class Spawner : MonoBehaviour, IOnTick
{
    public ushort id;
    public ThingPool thingPool;  // Direct reference to pool
    public SpawnerData data;
    public Destination targetDestination; // Direct reference to destination
    public Color color;

    public string spawnerName;
    public ushort spawnerID;
    public Sprite spawnerSprite;
    public Sprite destinationSprite;
    public byte ticksPerSpawn = 5;
    public Vector2Int position;
    public Vector2Int direction;
    public Vector2Int thingDestinationCoords;

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
        
        // Get next ID from game controller
        int thingId = gameController.GetNextThingId();
        
        // Get thing from pool and stamp it with info
        Thing thing = thingPool.GetThing(thingId, data.thingToSpawn, spawnPos, targetDestination.position, targetDestination);
        
        Debug.Log($"Spawner {name} spawned {thing.name} at {spawnPos}");
    }
}