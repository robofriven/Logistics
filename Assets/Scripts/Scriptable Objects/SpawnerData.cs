using UnityEngine;

[CreateAssetMenu(menuName = "Logistics/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    public string spawnerName;
    public Sprite spawnerSprite;
    public Sprite destinationSprite;
    public ThingData thingToSpawn;
    public byte ticksPerSpawn = 5;
    // Add more fields as needed
}