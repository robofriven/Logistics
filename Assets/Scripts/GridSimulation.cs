using UnityEngine;

public class GridSimulation : MonoBehaviour
{
    public GridAgent agentPrefab;
    public int agentCount = 10;
    public Vector2Int spawnAreaSize = new Vector2Int(10,10);

    private void Start()
    {
        if (agentPrefab == null)
        {
            Debug.LogError("GridSimulation: agentPrefab is not assigned.");
            return;
        }

        for (int i = 0; i < agentCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-spawnAreaSize.x, spawnAreaSize.x+1),
                Random.Range(-spawnAreaSize.y, spawnAreaSize.y+1),
                0f);
            Instantiate(agentPrefab, pos, Quaternion.identity, transform);
        }
    }
}
