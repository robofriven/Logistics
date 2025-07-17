using UnityEngine;

public class Destination : MonoBehaviour
{
    public ushort id { get; private set; }
    public string destinationName { get; private set; }
    public Sprite destinationSprite { get; private set; }
    public Color destinationColor { get; private set; } = Color.white;
    public ThingPool thingPool { get; private set; }
    public Vector2Int position { get; private set; }
    public DestinationData data { get; private set; }

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
        this.destinationName = data.destinationName;
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