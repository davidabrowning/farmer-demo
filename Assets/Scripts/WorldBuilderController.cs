using UnityEngine;

public class WorldBuilderController : MonoBehaviour
{
    public GameObject RegionBuilder;
    void Start()
    {
        RegionBuilderController regionBuilderController = RegionBuilder.GetComponent<RegionBuilderController>();
        for (int x = -10; x < 10; x++)
            for (int y = -10; y < 10; y++)
                regionBuilderController.BuildRegion(new Vector2(x, y));
    }
}
