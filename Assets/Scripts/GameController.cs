using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    public TileController tileController;
    public ThingPool thingPool;
    [SerializeField]
    [Tooltip("Spawner data for the spawner")]
    private SpawnerData spawnerData;
    [SerializeField]
    [Tooltip("The data for the destination where things will be delivered")]
    private DestinationData destinationData;

    private List<Spawner> spawners = new List<Spawner>();

    private int tick = 0;
    [SerializeField]
    [Tooltip("How many ticks per second")]
    [Range(1, 60)]
    private int ticksPerSecond;
    [SerializeField]
    [Tooltip("The score for the game, incremented by Delivering Things to the Destination")]
    private int Score = 0;

    private bool isRunning = false;

    private ushort nextThingId = 0;
    private ushort nextSpawnerId = 0;
    private ushort nextDestinationId = 0;

    public ushort GetNextThingId() => nextThingId++;
    public ushort GetNextSpawnerId() => nextSpawnerId++;
    public ushort GetNextDestinationId() => nextDestinationId++;
    public Vector2Int GetRandomSpawnDirection() => new Vector2Int(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2));
    
    public RouteColor GetRandomRouteColor()
    {
        var values = Enum.GetValues(typeof(RouteColor));
        return (RouteColor)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    public Color ConvertRouteToColor(RouteColor route)
    {
        switch (route)
        {
            case RouteColor.Red: return Color.red;
            case RouteColor.Blue: return Color.blue;
            case RouteColor.Green: return Color.green;
            case RouteColor.Yellow: return Color.yellow;
            case RouteColor.Purple: return new Color(0.5f, 0, 0.5f); // Magenta is too bright
            case RouteColor.Orange: return new Color(1f, 0.5f, 0f);
            default: return Color.white;
        }
    }

    // The idea of this is good in theoryk, but after only a couple of days I forgot that I was using it and searched all over fro CreateDestination in this file.
    // // Delegated Factory Functions
    // Action<SpawnerData, ThingPool, Destination, GameController> CreateSpawner = SpawnerFactory.CreateSpawner;
    // Func<Vector2Int, Sprite, ThingPool, Destination> CreateDestination = DestinationFactory.CreateDestination;

    void Start()
    {
        if (tileController == null || spawnerData == null || thingPool == null)
        {
            Debug.LogError("Something not assigned in Editor");
            return;
        }

        if (destinationData == null)
        {
            Debug.LogError("DestinationData not assigned in Editor");
            return;
        }

        tileController.InitializeWorld();

        // Create the first pair
        SpawnDestinationSpawnerPair();
    }

    void Update()
    {
        // Start/stop ticking based on space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = !isRunning;
            
            if (isRunning)
            {
                InvokeRepeating(nameof(Tick), 1f / ticksPerSecond, 1f / ticksPerSecond);
            }
            else
            {
                CancelInvoke(nameof(Tick));
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!isRunning)
            {
                Tick(); // Manually tick once if not running
            }
            else
            {
                CancelInvoke(nameof(Tick));
                isRunning = false; // Stop the automatic ticking
            }

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnDestinationSpawnerPair();
        }
    }

    public void RegisterSpawner(Spawner spawner)
    {
        spawners.Add(spawner);
    }

    private void Tick()
    {
        tick++;
        // Update all active things from the pool
        List<Thing> activeThings = thingPool.GetActiveThings();
        foreach (Thing thing in activeThings)
        {
            Mover.Move(tileController, thing);
            thing.OnTick(tick);
        }

        // Update all spawners
        foreach (Spawner spawner in spawners)
        {
            spawner.OnTick(tick);
        }
    }

    public void IncrementScore(int amount)
    {
        Score += amount;
        Debug.Log($"Score: {Score}");
    }


    public Vector2Int GetRandomPositionInBounds()
    {
        int randomX = UnityEngine.Random.Range(0, tileController.width);
        int randomY = UnityEngine.Random.Range(0, tileController.height);
        return new Vector2Int(randomX, randomY);
    }

    public Vector2Int GetRandomInteriorPosition()
    {
        int randomX = UnityEngine.Random.Range(1, tileController.width - 1);
        int randomY = UnityEngine.Random.Range(1, tileController.height - 1);
        return new Vector2Int(randomX, randomY);
    }

    // Alternative version that ensures the position is on a valid tile type
    public Vector2Int GetRandomValidPosition()
    {
        Vector2Int randomPos;
        Tile tile;
        int attempts = 0;
        int maxAttempts = 100; // Prevent infinite loops

        do
        {
            randomPos = GetRandomPositionInBounds();
            tile = tileController.GetTile(randomPos);
            attempts++;
        } 
        while (tile != null && !tile.isOccupiable && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Could not find valid random position after 100 attempts");
        }

        return randomPos;
    }
    public void SpawnDestinationSpawnerPair()
    {
        Vector2Int spawnerPosition = GetRandomInteriorPosition();
        Vector2Int destinationPosition = GetRandomPositionInBounds();
        Vector2Int spawnDirection = GetRandomSpawnDirection();
        RouteColor route = GetRandomRouteColor();
        Color color = ConvertRouteToColor(route);

        Destination destination = DestinationFactory.CreateDestination(destinationData, destinationPosition, thingPool, color);
        SpawnerFactory.CreateSpawner(spawnerData, spawnerPosition, spawnDirection, thingPool, destination, this, color);
    }
}
