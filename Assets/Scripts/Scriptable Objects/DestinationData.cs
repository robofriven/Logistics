using UnityEngine;

[CreateAssetMenu(menuName = "Logistics/DestinationData")]
public class DestinationData : ScriptableObject
{
    public string destinationName;
    public Vector2Int position;
    public Sprite destinationSprite;
    public int capacity = -1; // Default to unlimited capacity
    public bool isActive = true; // Default to active
}