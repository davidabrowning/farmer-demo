using Assets.Scripts;
using UnityEngine;

public class TileBuilderController : MonoBehaviour
{
    public GameObject TileFolder;
    public GameObject DirtBackground;
    public GameObject TreeBackground;
    public GameObject BushBackground;
    public GameObject WaterBackground;

    public void PlaceTile(RegionTypeEnum regionType, Vector2 coords)
    {
        GameObject tile;
        switch (regionType)
        {
            case RegionTypeEnum.Tree:
                tile = TreeBackground;
                break;
            case RegionTypeEnum.Dirt:
                tile = DirtBackground;
                break;
            case RegionTypeEnum.Bush:
                tile = BushBackground;
                break;
            case RegionTypeEnum.Water:
                tile = WaterBackground;
                break;
            default:
                tile = WaterBackground;
                break;
        }
        PlaceBackgroundTile(tile, new Vector3(coords.x, coords.y, 0));
    }
    void CreateBackgroundArea(GameObject backgroundTile, Vector3 center, float radius)
    {
        Vector3 bottomLeftCorner = new Vector3(center.x - radius, center.y - radius, 0);
        Vector3 topRightCorner = new Vector3(center.x + radius, center.y + radius, 0);
        CreateBackgroundArea(backgroundTile, bottomLeftCorner, topRightCorner);
    }

    public void CreateBackgroundArea(GameObject backgroundTile, Vector3 cornerA, Vector3 cornerB)
    {
        for (float x = cornerA.x; x <= cornerB.x; x++)
        {
            for (float y = cornerA.y; y <= cornerB.y; y++)
            {
                PlaceBackgroundTile(backgroundTile, new Vector3(x, y, 0));
            }
        }
    }

    void PlaceBackgroundTile(GameObject backgroundTile, Vector3 position)
    {
        GameObject area = Instantiate(backgroundTile, position, Quaternion.identity);

        SpriteRenderer renderer = area.GetComponent<SpriteRenderer>();
        Vector2 spriteSize = renderer.sprite.bounds.size;
        area.transform.localScale = new Vector3(1 / spriteSize.x, 1 / spriteSize.y, 1);
        area.transform.SetParent(TileFolder.transform);
    }
}
