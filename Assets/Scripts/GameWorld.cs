using UnityEngine;

public class GameWorld : MonoBehaviour
{

public int width = 10;
public int height = 10;




}

// TODO(Codex) : Refactor this to change from isWalkable to usable

public class Tile
{
    public Vector2Int position { get; private set; }
    public bool isWalkable { get; private set; }
    public bool isOccupied { get; private set; }

    public Tile(Vector2Int position, bool isWalkable)
    {
        this.position = position;
        this.isWalkable = isWalkable;
        this.isOccupied = false;
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }
}