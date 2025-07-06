using UnityEngine;

public class Spawner : MonoBehaviour, IOnTick
{
    public ThingPool thingPool;  // Direct reference to pool
    public SpawnerData data;
    public Destination targetDestination; // Direct reference to destination

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
        Vector2Int spawnPos = data.position + data.direction;
        
        // Get thing from pool and stamp it with info
        Thing thing = thingPool.GetThing();
        thing.Initialize(data.thingToSpawn, spawnPos, data.thingDestinationCoords, targetDestination);
        
        Debug.Log($"Spawner {name} spawned {thing.name} at {spawnPos}");
    }
}