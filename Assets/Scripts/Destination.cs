using UnityEngine;

public class Destination : MonoBehaviour
{
    public ushort id; // Aquired from GameController
    public string destinationName;  // Pulled from data
    public Sprite destinationSprite; // Pulled from data
    public Color destinationColor = Color.white; // Default color, can be changed later
    public ThingPool thingPool;
    public Vector2Int position;
    public DestinationData data; // Keep reference for less common properties


    private GameController gameController;

    
    public void Initialize(DestinationData data, Vector2Int position, ThingPool thingPool, Color color = default)
    {
        if (color != default)
        {
            this.destinationColor = color;
        }

        gameController = FindFirstObjectByType<GameController>();
        this.id = gameController.GetNextDestinationId();
        this.data = data;
        this.destinationName = data.destinationName; // Pull commonly used values
        this.destinationSprite = data.destinationSprite;
        this.position = position;
        this.thingPool = thingPool;
    }
    public void ReceiveThing(Thing thing)
    {
        Debug.Log($"Destination {destinationName} received {thing.name}"); // Use cached value
        gameController.IncrementScore(1); // Use cached value 
        thingPool.ReturnThing(thing);
    }
}