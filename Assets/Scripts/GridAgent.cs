using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeightedDirection
{
    public Vector2Int direction;
    public float weight;
}

public class GridAgent : MonoBehaviour
{
    public List<WeightedDirection> directions = new List<WeightedDirection>
    {
        new WeightedDirection { direction = Vector2Int.up, weight = 1f },
        new WeightedDirection { direction = Vector2Int.right, weight = 1f },
        new WeightedDirection { direction = Vector2Int.down, weight = 1f },
        new WeightedDirection { direction = Vector2Int.left, weight = 1f }
    };

    public float stepTime = 0.5f;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stepTime)
        {
            timer = 0f;
            Vector2Int dir = PickDirection();
            transform.position += new Vector3(dir.x, dir.y, 0f);
        }
    }

    private Vector2Int PickDirection()
    {
        if (directions == null || directions.Count == 0)
            return Vector2Int.zero;

        float total = 0f;
        foreach (var d in directions)
        {
            total += d.weight;
        }

        float random = Random.Range(0f, total);
        float cumulative = 0f;
        foreach (var d in directions)
        {
            cumulative += d.weight;
            if (random <= cumulative)
                return d.direction;
        }
        return directions[directions.Count - 1].direction;
    }
}
