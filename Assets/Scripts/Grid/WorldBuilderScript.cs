using UnityEngine;

namespace FarmerDemo
{
    public class WorldBuilderScript : MonoBehaviourSingleton<WorldBuilderScript>
    {
        public void BuildInitialWorld()
        {
            RegionBuilderScript.Instance.BuildRegion(new Vector2Int(0, 0), RegionTypeEnum.Dirt);
            RegionBuilderScript.Instance.BuildRegion(new Vector2Int(1, 0), RegionTypeEnum.Bush);
            RegionBuilderScript.Instance.BuildRegion(new Vector2Int(0, 1), RegionTypeEnum.Bush);
            RegionBuilderScript.Instance.BuildRegion(new Vector2Int(1, 1), RegionTypeEnum.Bush);
            for (int x = -5; x < 5; x++)
            {
                for (int y = -5; y < 5; y++)
                {
                    if (x < 0 || x > 1 || y < 0 || y > 1)
                        RegionBuilderScript.Instance.BuildRegion(new Vector2Int(x, y), RegionTypeEnum.Dirt);
                }
            }
        }
    }
}