using Assets.Scripts;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RegionBuilderController : MonoBehaviour
{
    public GameObject TileLayer;
    public GameObject TreeCreator;
    public GameObject TwigCreator;
    public GameObject BerryBushBuilder;
    private float RegionSize = 15f;

    public void BuildRegion(Vector2 regionCoords, RegionType regionType)
    {
        TileLayerController tileLayerController = TileLayer.GetComponent<TileLayerController>();
        float minX = regionCoords.x * RegionSize;
        float maxX = regionCoords.x * RegionSize + RegionSize - 1;
        float minY = regionCoords.y * RegionSize;
        float maxY = regionCoords.y * RegionSize + RegionSize - 1;
        switch (regionType)
        {
            case RegionType.Bush:
                BerryBushBuilderController berryBushBuilderController = BerryBushBuilder.GetComponent<BerryBushBuilderController>();
                berryBushBuilderController.CreateBerryBushes(new Vector2(minX, minY), new Vector2(maxX, maxY));
                break;
            case RegionType.Tree:
                TreeCreatorController tcc = TreeCreator.GetComponent<TreeCreatorController>();
                tcc.CreateTree(new Vector2(minX, minY), new Vector2(maxX, maxY));
                TwigCreator twigCreator = TwigCreator.GetComponent<TwigCreator>();
                twigCreator.CreateTwigs(new Vector2(minX, minY), new Vector2(maxX, maxY));
                break;
            case RegionType.Dirt:
                break;
            case RegionType.Water:
                break;
            default:
                break;
        }
        for (float x = minX; x <= maxX; x++)
        {
            for (float y = minY; y <= maxY; y++)
            {
                tileLayerController.PlaceTile(regionType, new Vector2(x, y));
            }
        }
    }
}
