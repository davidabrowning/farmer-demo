using Assets.Scripts;
using UnityEngine;

public class WorldBuilderController : MonoBehaviour
{
    public GameObject RegionBuilder;
    void Start()
    {
        RegionBuilderController regionBuilderController = RegionBuilder.GetComponent<RegionBuilderController>();
        regionBuilderController.BuildRegion(new Vector2(0, 0), RegionType.Dirt);
        regionBuilderController.BuildRegion(new Vector2(1, 0), RegionType.Dirt);
        regionBuilderController.BuildRegion(new Vector2(0, 1), RegionType.Tree);
        regionBuilderController.BuildRegion(new Vector2(1, 1), RegionType.Bush);
        for (int x = -5; x < 5; x++)
        {
            for (int y = -5; y < 5; y++)
            {
                if (x < 0 || x > 1 || y < 0 || y > 1)
                    regionBuilderController.BuildRegion(new Vector2(x, y), RegionType.Water);
            }
        }
    }
}
