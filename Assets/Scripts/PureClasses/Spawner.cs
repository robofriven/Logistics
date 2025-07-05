using UnityEngine;

public class Spawner : IOnTick
{
    public int rate { get; private set; }
    public Thing thing { get; private set; } // The thing that this spawner will spawn
    public Tile destination {get; private set; }
    public Vector2Int position { get; private set; } // Position of the spawner in the world
    public Vector2Int direction { get; private set; } // Direction of the spawner, if applicable
    public int id {get; private set; } // Unique identifier for the spawner
    public int startingTick { get; private set; } // The tick at which the spawner was created

    private static int nextId = 0;
    private GameController gameController; // Reference to the GameController for spawning logic

    // Add in more perperties later to complicate things

    public Spawner(GameController gameController, Thing thing, int rate, Tile destination, Vector2Int position, Vector2Int direction, int currentTick)
    {
        this.id = GenerateUniqueId();
        this.gameController = gameController;
        this.startingTick = currentTick; // Store the tick when the spawner was created
        this.thing = thing;
        this.rate = rate;
        this.destination = destination;
        this.position = position; // The actual spawner's position in the world
        this.direction = direction;
        thing.SetDestination(destination.position); // Set the destination for the thing
        Debug.Log($"Spawner created with ID {id} at position {position} pointing {direction}");
        gameController.AddSpawner(this); // Register this spawner with the GameController        
    }

    private static int GenerateUniqueId()
    {
        return nextId++;
    }

    public void SetRate(int newRate)
    {
        rate = newRate;
    }
    public void SetThing(Thing newThing)
    {
        thing = newThing;
    }
    public void SetDestination(Tile newDestination)
    {
        destination = newDestination;
    }
    public void SetPosition(Vector2Int newPosition)
    {
        position = newPosition;
    }
    public void SetDirection(Vector2Int newDirection)
    {
        direction = newDirection;
    }

    public void Spawn()
    {
        if (thing == null || destination == null)
        {
            Debug.LogWarning("Spawner cannot spawn: thing or destination is not set.");
            return;
        }

        Vector2Int spawnPosition = position + direction;

        // Create a new Thing (pure class, not a Unity object)
        Thing newThing = new Thing(thing.name);
        newThing.SetPosition(spawnPosition);
        newThing.SetDestination(destination.position);
        gameController.AddThing(newThing); // Add the new thing to the game world
        Debug.Log($"Spawned {newThing.name} at {spawnPosition} with destination {destination.position}");
        // Add newThing to your game logic's list of things, if needed
    }

    public void OnTick(int tickNumber)
    {
        // Check if the spawner should spawn a thing this tick
        if (rate > 0 && tickNumber % rate == 0)
        {
            Debug.Log($"Spawner {id}: Fire in the hole!");
            Spawn();
        }
    }
}
