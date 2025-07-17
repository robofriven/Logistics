using UnityEngine;

public class Thing : MonoBehaviour
{
    public int id { get; private set; }
    public ThingData data { get; private set; }
    public Vector2Int position { get; private set; }
    public Vector2Int destination { get; private set; }
    public Destination destinationObject { get; private set; }

    private GameController gameController;
    private SpriteRenderer spriteRenderer;

    public void Initialize(int id, ThingData data, Vector2Int startPosition, Vector2Int destination, Destination destinationObject = null)
    {
        this.id = id;
        this.data = data;
        this.position = startPosition;
        this.destination = destination;
        this.destinationObject = destinationObject;
        name = data.thingName;
        gameController = FindFirstObjectByType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("Thing: GameController not found! It exists, but you can't find it");
            return;
        }

        // Cache the sprite renderer
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null && data.sprite != null)
        {
            spriteRenderer.sprite = data.sprite;
            spriteRenderer.sortingLayerName = "Things";
        }

        transform.position = new Vector3(position.x, position.y, 0);
        
        // Update the child sprite position to be centered
        if (spriteRenderer != null)
        {
            spriteRenderer.transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        }
    }

    public void MoveTo(Vector2Int newPosition)
    {
        this.position = newPosition;
        transform.position = new Vector3(position.x, position.y, 0);
        
        // Use cached sprite renderer
        if (spriteRenderer != null)
        {
            spriteRenderer.transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        }
    }

    public void OnTick(int tick)
    {
        // Check if the thing has reached its destination
        if (position == destination)
        {
            Debug.Log($"{name} has reached its destination at {destination}");
            
            // Use direct reference if available, otherwise fall back to coords
            if (destinationObject != null)
            {
                destinationObject.ReceiveThing(this);
            }
            else
            {
                // Fallback for coord-only destinations
                Debug.Log($"{name} reached coordinate destination. Tis been delivered m'lord");
                gameController.IncrementScore(data.scoreValue);
            }
        }
    }
}