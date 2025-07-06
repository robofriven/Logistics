using UnityEngine;

[CreateAssetMenu(menuName = "Logistics/ThingData")]
public class ThingData : ScriptableObject
{
    public string thingName;
    public Sprite sprite;
    // Add more fields as needed (size, weight, etc.)
}