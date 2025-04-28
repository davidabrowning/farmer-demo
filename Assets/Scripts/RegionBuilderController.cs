using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RegionBuilderController : MonoBehaviour
{
    public GameObject TileLayer;
    public GameObject TreeCreator;
    public GameObject TwigCreator;
    public GameObject BerryBushBuilder;
    public float RegionSize = 15f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BuildRegion(Vector2 regionCoords)
    {
        TileLayerController backgroundController = TileLayer.GetComponent<TileLayerController>();
        float minX = regionCoords.x * RegionSize;
        float maxX = regionCoords.x * RegionSize + RegionSize - 1;
        float minY = regionCoords.y * RegionSize;
        float maxY = regionCoords.y * RegionSize + RegionSize - 1;
        float tileType = Random.Range(0, 2);
        string tileBackgroundType = "";
        switch (tileType)
        {
            case 0:
                tileBackgroundType = "Green";
                BerryBushBuilderController berryBushBuilderController = BerryBushBuilder.GetComponent<BerryBushBuilderController>();
                berryBushBuilderController.CreateBerryBushes(new Vector2(minX, minY), new Vector2(maxX, maxY));
                break;
            case 1:
                tileBackgroundType = "DarkGreen";
                TreeCreatorController tcc = TreeCreator.GetComponent<TreeCreatorController>();
                tcc.CreateTree(new Vector2(minX, minY), new Vector2(maxX, maxY));
                TwigCreator twigCreator = TwigCreator.GetComponent<TwigCreator>();
                twigCreator.CreateTwigs(new Vector2(minX, minY), new Vector2(maxX, maxY));
                break;
            case 2:
                tileBackgroundType = "Gray";
                break;
            default:
                break;
        }
        for (float x = minX; x <= maxX; x++)
        {
            for (float y = minY; y <= maxY; y++)
            {
                backgroundController.PlaceTile(tileBackgroundType, new Vector2(x, y));
            }
        }
    }
}
