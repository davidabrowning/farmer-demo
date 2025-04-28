using Assets.Scripts;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RegionBuilderScript : MonoBehaviour
{
    public GameObject TileLayer;
    public GameObject TreeCreator;
    public GameObject TwigCreator;
    public GameObject BerryBushBuilder;
    private float RegionSize = 15f;

    public void BuildRegion(Vector2 regionCoords, RegionTypeEnum regionType)
    {
        TileBuilderController tileLayerController = TileLayer.GetComponent<TileBuilderController>();
        float minX = regionCoords.x * RegionSize;
        float maxX = regionCoords.x * RegionSize + RegionSize - 1;
        float minY = regionCoords.y * RegionSize;
        float maxY = regionCoords.y * RegionSize + RegionSize - 1;
        switch (regionType)
        {
            case RegionTypeEnum.Bush:
                BerryBushBuilderScript berryBushBuilderController = BerryBushBuilder.GetComponent<BerryBushBuilderScript>();
                berryBushBuilderController.CreateBerryBushes(new Vector2(minX, minY), new Vector2(maxX, maxY));
                break;
            case RegionTypeEnum.Tree:
                TreeBuilderScript tcc = TreeCreator.GetComponent<TreeBuilderScript>();
                tcc.CreateTree(new Vector2(minX, minY), new Vector2(maxX, maxY));
                TwigBuilderScript twigCreator = TwigCreator.GetComponent<TwigBuilderScript>();
                twigCreator.CreateTwigs(new Vector2(minX, minY), new Vector2(maxX, maxY));
                break;
            case RegionTypeEnum.Dirt:
                break;
            case RegionTypeEnum.Water:
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
