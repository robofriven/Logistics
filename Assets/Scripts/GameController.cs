using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameWorld gameWorld;
    public WorldVisualizer worldVisualizer;
    public ThingVisualizer thingVisualizer;
    public SpawnerVisualizer spawnerVisualizer;

    private int tick = 0;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float tickRate = 0.5f; // Number of ticks per second
    private float tickTimer = 0f;

    [SerializeField]
    private bool isRunning = false;

    private List<Thing> things = new List<Thing>();
    private List<Spawner> spawners = new List<Spawner>();

    void Start()
    {
        if (gameWorld == null || worldVisualizer == null || thingVisualizer == null || spawnerVisualizer == null)
        {
            Debug.LogError("Something not assigned in Editor");
            return;
        }

        gameWorld.InitializeWorld();
        worldVisualizer.Initialize(gameWorld);

        // Example tile setup
        gameWorld.GetTile(new Vector2Int(0, 0)).SetType(TileType.Right);
        gameWorld.GetTile(new Vector2Int(1, 0)).SetType(TileType.Left);
        gameWorld.GetTile(new Vector2Int(2, 0)).SetType(TileType.Up);
        gameWorld.GetTile(new Vector2Int(3, 0)).SetType(TileType.Down);

        Tile spawnTile = gameWorld.GetTile(new Vector2Int(4, 0));
        spawnTile.SetType(TileType.Empty);

        Spawner spawner = new Spawner(
            this,
            new Thing("TestThing"),
            3,
            gameWorld.GetTile(new Vector2Int(1, 8)),
            spawnTile.position,
            Vector2Int.up,
            tick
        );
        spawners.Add(spawner);

        // Visualize initial spawners and things
        spawnerVisualizer.UpdateSpawners(spawners);
        thingVisualizer.UpdateThings(things);
    }

    void Update()
    {
        if (isRunning)
        {
            tickTimer += Time.deltaTime;
            float tickInterval = 1f / tickRate;

            while (tickTimer >= tickInterval)
            {
                tick++;
                Tick();
                tickTimer -= tickInterval;
            }
        }
    }

    private void Tick()
    {
        // Example: tick all spawners
        foreach (Spawner spawner in spawners)
        {
            spawner.OnTick(tick);
        }

        // Move things, update visuals, etc.
        CheckTiles();
        IterateOverThings();

        // Update visuals after logic
        spawnerVisualizer.UpdateSpawners(spawners);
        thingVisualizer.UpdateThings(things);
    }

    private void CheckTiles()
    {
        // Implement your movement and tile logic here
    }

    private void IterateOverThings()
    {
        // Example: iterate over things in creation order
        foreach (Thing thing in things)
        {
            Mover.Move(gameWorld, thing, gameWorld.GetTile(thing.position));
            thingVisualizer.UpdateThings(things);
            Debug.Log($"Thing {thing.name} moved to {thing.position}");            
        }
    }

    public void AddThing(Thing thing)
    {
        Debug.Log("Adding thing: " + thing.name);
        things.Add(thing);
        //thingVisualizer.UpdateThings(things);
    }

    public void RemoveThing(Thing thing)
    {
        things.Remove(thing);
        thingVisualizer.UpdateThings(things);
    }

    public void AddSpawner(Spawner spawner)
    {
        spawners.Add(spawner);
        spawnerVisualizer.UpdateSpawners(spawners);
    }

    public void RemoveSpawner(Spawner spawner)
    {
        spawners.Remove(spawner);
        spawnerVisualizer.UpdateSpawners(spawners);
    }
}
