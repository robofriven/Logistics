using UnityEngine;

/// Logistics requires a thing going to a place using a system. This is the Thing
public class Thing : MonoBehaviour
{
    //Location Data
    public Vector2Int position{get; private set;}
    public Vector2Int destination{get; private set;}

    //Item Data
    [Header("Item Data")]
    public string name = "Thing";
    public Vector2Int size = new Vector2Int(1, 1);
    public int weight = 1;
    public int lifeTime = -1; // -1 means infinite lifetime   
}
