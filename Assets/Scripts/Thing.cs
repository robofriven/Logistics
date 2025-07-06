using UnityEngine;

public class Thing : MonoBehaviour
{
    public ThingData data;
    public Vector2Int position;
    public Vector2Int destination;
    public Destination destinationObject; // Direct reference instead of lookup

    public void Initialize(ThingData data, Vector2Int startPosition, Vector2Int destination, Destination destinationObject = null)
    {
        this.data = data;
        this.position = startPosition;
        this.destination = destination;
        this.destinationObject = destinationObject;
        name = data.thingName;

        // Set sprite if you have a SpriteRenderer
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && data.sprite != null)
        {
            sr.sprite = data.sprite;
            sr.sortingLayerName = "Things";
        }

        transform.position = new Vector3(position.x, position.y, 0);
    }

    public void MoveTo(Vector2Int newPosition)
    {
        position = newPosition;
        transform.position = new Vector3(position.x, position.y, 0);
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
                Debug.Log($"{name} reached coordinate destination {destination}");
                // Handle coordinate-only logic here
            }
        }
    }
}