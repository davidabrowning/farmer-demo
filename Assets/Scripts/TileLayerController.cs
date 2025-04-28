using UnityEngine;

public class TileLayerController : MonoBehaviour
{
    public GameObject GrayBackground;
    public GameObject GreenBackground;
    public GameObject DarkGreenBackground;

    public void PlaceTile(string type, Vector2 coords)
    {
        GameObject tile;
        switch (type)
        {
            case "Green":
                tile = GreenBackground;
                break;
            case "Gray":
                tile = GrayBackground;
                break;
            case "DarkGreen":
                tile = DarkGreenBackground;
                break;
            default:
                tile = GrayBackground;
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
    }
}
