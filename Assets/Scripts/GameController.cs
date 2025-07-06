using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    public TileController tileController;
    public ThingPool thingPool;
    [Tooltip("Spawner data for the spawner")]
    public SpawnerData spawnerData;

    private List<Spawner> spawners = new List<Spawner>();

    private int tick = 0;
    [SerializeField]
    [Tooltip("How many ticks per second")]
    [Range(1, 60)]
    private int ticksPerSecond;

    private bool isRunning = false;

    // Delegated Factory Functions
    Action<SpawnerData, ThingPool, Destination, GameController> CreateSpawner = SpawnerFactory.CreateSpawner;
    Func<SpawnerData, ThingPool, Destination> CreateDestination = DestinationFactory.CreateDestination;

    void Start()
    {
        if (tileController == null || spawnerData == null || thingPool == null)
        {
            Debug.LogError("Something not assigned in Editor");
            return;
        }

        tileController.InitializeWorld();

        // Create destination first, then spawner
        Destination destination = CreateDestination(spawnerData, thingPool);
        CreateSpawner(spawnerData, thingPool, destination, this);
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
}
