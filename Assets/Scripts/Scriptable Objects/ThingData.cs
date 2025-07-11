using UnityEngine;

[CreateAssetMenu(menuName = "Logistics/ThingData")]
public class ThingData : ScriptableObject
{
    public string thingName;
    public Sprite sprite;

    public int scoreValue = 1; // The score value for delivering this thing
    public int size = 1; // Size of the thing, can be used for stacking
    public int weight = 1; // Weight of the thing, can be used for physics or other calculations
    public int maxStackSize = 10;
    public float value =3.50f;

    public int deliveryDeadline = 45; // Deadline in ticks to deliver this thing, can be used for time-based challenges
    // Add more fields as needed (size, weight, etc.)
}