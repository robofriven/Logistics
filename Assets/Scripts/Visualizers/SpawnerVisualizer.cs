using UnityEngine;
using System.Collections.Generic;

public class SpawnerVisualizer : MonoBehaviour
{
    public GameObject spawnerPrefab;
    private Dictionary<Spawner, GameObject> spawnerVisuals = new Dictionary<Spawner, GameObject>();

    /// <summary>
    /// Updates or creates visuals for all spawners in the list.
    /// </summary>
    public void UpdateSpawners(List<Spawner> spawners)
    {
        // Remove visuals for spawners that no longer exist
        List<Spawner> toRemove = new List<Spawner>();
        foreach (Spawner s in spawnerVisuals.Keys)
        {
            if (!spawners.Contains(s))
            {
                Destroy(spawnerVisuals[s]);
                toRemove.Add(s);
            }
        }
        foreach (Spawner s in toRemove)
            spawnerVisuals.Remove(s);

        // Update or create visuals for current spawners
        foreach (Spawner spawner in spawners)
        {
            GameObject visual;
            if (spawnerVisuals.TryGetValue(spawner, out visual))
            {
                visual.transform.position = new Vector3(spawner.position.x, spawner.position.y, 0);
            }
            else
            {
                GameObject spawnerObj = Instantiate(spawnerPrefab, new Vector3(spawner.position.x, spawner.position.y, 0), Quaternion.identity);
                spawnerObj.name = "Spawner_" + spawner.id;
                spawnerVisuals[spawner] = spawnerObj;
            }
        }
    }
}