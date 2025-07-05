using UnityEngine;

public class TileVisual : MonoBehaviour
{
    private Tile tile;
    private GameObject emptyPrefab;
    private GameObject occupiedPrefab;
    private GameObject rightPrefab;
    private GameObject leftPrefab;
    private GameObject upPrefab;
    private GameObject downPrefab;
    private GameObject wallPrefab;
    private GameObject currentVisual;

    public void Initialize(
        Tile tile,
        GameObject emptyPrefab,
        GameObject occupiedPrefab,
        GameObject rightPrefab,
        GameObject leftPrefab,
        GameObject upPrefab,
        GameObject downPrefab,
        GameObject wallPrefab)
    {
        this.tile = tile;
        this.emptyPrefab = emptyPrefab;
        this.occupiedPrefab = occupiedPrefab;
        this.rightPrefab = rightPrefab;
        this.leftPrefab = leftPrefab;
        this.upPrefab = upPrefab;
        this.downPrefab = downPrefab;
        this.wallPrefab = wallPrefab;
        UpdateVisual(tile.isOccupied, tile.type);
    }

    public void UpdateVisual(bool isOccupied, TileType type)
    {
        if (currentVisual != null)
            Destroy(currentVisual);

        GameObject prefabToUse = GetPrefabForTileType(type, isOccupied);
        currentVisual = Instantiate(prefabToUse, transform);
        currentVisual.transform.localPosition = Vector3.zero;
    }

    private GameObject GetPrefabForTileType(TileType type, bool isOccupied)
    {
        if (isOccupied)
            return occupiedPrefab;

        switch (type)
        {
            case TileType.Right: return rightPrefab;
            case TileType.Left:  return leftPrefab;
            case TileType.Up:    return upPrefab;
            case TileType.Down:  return downPrefab;
            case TileType.Wall:  return wallPrefab;
            case TileType.Empty:
            default:             return emptyPrefab;
        }
    }
}