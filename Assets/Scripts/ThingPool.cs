using UnityEngine;
using System.Collections.Generic;

public class ThingPool : MonoBehaviour
{
    [SerializeField] private int initialPoolSize = 10;
    
    private Queue<Thing> availableThings = new Queue<Thing>();
    private List<Thing> activeThings = new List<Thing>();

    private GameObject pool;
    
    void Start()
    {
        pool = new GameObject("ThingPool");
        // Pre-populate the pool
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewThing();
        }
    }
    
    private Thing CreateNewThing()
    {
        // Create a new GameObject for the Thing
        GameObject thingObj = new GameObject("PooledThing");
        thingObj.transform.parent = pool.transform; // Keep organized under pool
        thingObj.SetActive(false);
        
        // Create the sprite child object
        GameObject spriteObj = new GameObject("Sprite");
        spriteObj.transform.parent = thingObj.transform;
        SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Things";
        
        Thing thing = thingObj.AddComponent<Thing>();
        
        availableThings.Enqueue(thing);
        return thing;
    }
    
    public Thing GetThing(int id, ThingData data, Vector2Int startPosition, Vector2Int destination, Destination destinationObject = null)
    {
        Thing thing;
        
        if (availableThings.Count > 0)
        {
            thing = availableThings.Dequeue();
        }
        else
        {
            // Pool is empty, create a new one
            thing = CreateNewThing();
            availableThings.Dequeue(); // Remove the one we just added
        }
        
        // Configure the thing when it's pulled from pool
        thing.Initialize(id, data, startPosition, destination, destinationObject);
        thing.gameObject.SetActive(true);
        activeThings.Add(thing);
        return thing;
    }
    
    public void ReturnThing(Thing thing)
    {
        if (activeThings.Contains(thing))
        {
            activeThings.Remove(thing);
            thing.gameObject.SetActive(false);
            availableThings.Enqueue(thing);
        }
    }
    
    public List<Thing> GetActiveThings()
    {
        return new List<Thing>(activeThings);
    }
}