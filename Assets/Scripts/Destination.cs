using UnityEngine;

public class Destination : MonoBehaviour
{
    public ThingPool thingPool;
    public Vector2Int position;
    
    public void ReceiveThing(Thing thing)
    {
        Debug.Log($"Destination received {thing.name}");
        thingPool.ReturnThing(thing);
    }
}