using UnityEngine;
using System.Collections.Generic;

public class ThingVisualizer : MonoBehaviour
{
    public GameObject thingPrefab;
    private Dictionary<Thing, GameObject> thingVisuals = new Dictionary<Thing, GameObject>();

    public void UpdateThings(List<Thing> things)
    {
        // Remove visuals for things that no longer exist
        List<Thing> toRemove = new List<Thing>();
        foreach (Thing t in thingVisuals.Keys)
        {
            if (!things.Contains(t))
            {
                Destroy(thingVisuals[t]);
                toRemove.Add(t);
            }
        }
        foreach (Thing t in toRemove)
            thingVisuals.Remove(t);

        // Update or create visuals for current things
        foreach (Thing thing in things)
        {
            GameObject visual;
            if (thingVisuals.TryGetValue(thing, out visual))
            {
                visual.transform.position = new Vector3(thing.position.x, thing.position.y, 0);
            }
            else
            {
                CreateThingVisual(thing);
            }
        }
    }

    public void CreateThingVisual(Thing thing)
    {
        Debug.Log($"Creating visual for thing: {thing.name} at position {thing.position}"); 
        GameObject thingObj = Instantiate(thingPrefab, new Vector3(thing.position.x, thing.position.y, 0), Quaternion.identity);
        thingObj.name = thing.name;
        thingVisuals[thing] = thingObj;
    }
}