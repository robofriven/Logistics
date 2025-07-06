using UnityEngine;

[CreateAssetMenu(menuName = "Logistics/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    public string spawnerName;
    public int spawnerID;
    public Sprite spawnerSprite;
    public Sprite destinationSprite;
    public ThingData thingToSpawn;
    public int ticksPerSpawn = 5;
    public Vector2Int position;
    public Vector2Int direction;
    public Vector2Int thingDestinationCoords;
    // Add more fields as needed
}