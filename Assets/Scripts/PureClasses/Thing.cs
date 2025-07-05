using UnityEngine;

/// Logistics requires a thing going to a place using a system. This is the Thing
public class Thing
{
    private static int nextId = 0;
    public int id { get; }
    public string name;
    public Vector2Int position { get; private set; }
    public Vector2Int destination { get; private set; }

    public Thing(string name)
    {
        this.id = nextId++;
        this.name = name;
    }

    public void SetPosition(Vector2Int newPosition)
    {
        position = newPosition;
    }
    public void SetDestination(Vector2Int newDestination)
    {
        destination = newDestination;
    }

    public override bool Equals(object obj)
    {
        if (obj is Thing other)
            return id == other.id;
        return false;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}
