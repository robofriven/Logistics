using UnityEngine;

public class TileVisual : MonoBehaviour
{
    private Tile tile;
    private GameObject emptyPrefab;
    private GameObject occupiedPrefab;
    private GameObject currentVisual;

    public void Initialize(Tile tile, GameObject emptyPrefab, GameObject occupiedPrefab)
    {
        this.tile = tile;
        this.emptyPrefab = emptyPrefab;
        this.occupiedPrefab = occupiedPrefab;
        UpdateVisual(tile.isOccupied);
    }


    //This just makes empty or occupied visuals, it needs to make sure that it covers all of the types too.
    public void UpdateVisual(bool isOccupied)
    {
        if (currentVisual != null)
            Destroy(currentVisual);

        GameObject prefabToUse = isOccupied ? occupiedPrefab : emptyPrefab;
        currentVisual = Instantiate(prefabToUse, transform);
        currentVisual.transform.localPosition = Vector3.zero;
    }
}