using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject GrayBackground;
    public GameObject GreenBackground;
    public GameObject DarkGreenBackground;

    void Start()
    {
        CreateBackgroundArea(GrayBackground, new Vector3(-15, -5, 0), new Vector3(-1, 5, 0));
        CreateBackgroundArea(GreenBackground, new Vector3(0, -5, 0), new Vector3(14, 5, 0));
        CreateBackgroundArea(DarkGreenBackground, new Vector3(15, -5, 0), new Vector3(29, 5, 0));
    }

    void CreateBackgroundArea(GameObject backgroundTile, Vector3 center, float radius)
    {
        Vector3 bottomLeftCorner = new Vector3(center.x - radius, center.y - radius, 0);
        Vector3 topRightCorner = new Vector3(center.x + radius, center.y + radius, 0);
        CreateBackgroundArea(backgroundTile, bottomLeftCorner, topRightCorner);
    }

    void CreateBackgroundArea(GameObject backgroundTile, Vector3 cornerA, Vector3 cornerB)
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
