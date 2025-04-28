using Assets.Scripts;
using UnityEngine;

public class WorldBuilderScript : MonoBehaviour
{
    public GameObject RegionBuilder;
    void Start()
    {
        RegionBuilderScript regionBuilderController = RegionBuilder.GetComponent<RegionBuilderScript>();
        regionBuilderController.BuildRegion(new Vector2(0, 0), RegionTypeEnum.Dirt);
        regionBuilderController.BuildRegion(new Vector2(1, 0), RegionTypeEnum.Dirt);
        regionBuilderController.BuildRegion(new Vector2(0, 1), RegionTypeEnum.Tree);
        regionBuilderController.BuildRegion(new Vector2(1, 1), RegionTypeEnum.Bush);
        for (int x = -5; x < 5; x++)
        {
            for (int y = -5; y < 5; y++)
            {
                if (x < 0 || x > 1 || y < 0 || y > 1)
                    regionBuilderController.BuildRegion(new Vector2(x, y), RegionTypeEnum.Water);
            }
        }
    }
}
