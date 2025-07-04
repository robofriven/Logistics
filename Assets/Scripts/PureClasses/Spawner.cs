using UnityEngine;

public class Spawner
{
    public int rate { get; private set; }
    public Tile destination {get; private set; }
    public Vector2Int position { get; private set; } // Position of the spawner in the world
    public int id {get; private set; } // Unique identifier for the spawner

    private static int nextId = 0;

    // Add in more perperties later to complicate things

    public Spawner(int rate, Tile destination)
    {
        this.rate = rate;
        this.destination = destination;
        this.position = destination.position; // Assuming the spawner is at the destination tile's position
        this.id = GenerateUniqueId();
    }

    private static int GenerateUniqueId()
    {
        return nextId++;
    }
}
