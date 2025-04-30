using Assets.Scripts.Core;
using Assets.Scripts.Grid;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class WorldBuilderScript : MonoBehaviourSingleton<WorldBuilderScript>
    {
        void Start()
        {
            RegionBuilderScript.Instance.BuildRegion(new Vector2(0, 0), RegionTypeEnum.Dirt);
            RegionBuilderScript.Instance.BuildRegion(new Vector2(1, 0), RegionTypeEnum.Dirt);
            RegionBuilderScript.Instance.BuildRegion(new Vector2(0, 1), RegionTypeEnum.Tree);
            RegionBuilderScript.Instance.BuildRegion(new Vector2(1, 1), RegionTypeEnum.Bush);
            for (int x = -5; x < 5; x++)
            {
                for (int y = -5; y < 5; y++)
                {
                    if (x < 0 || x > 1 || y < 0 || y > 1)
                        RegionBuilderScript.Instance.BuildRegion(new Vector2(x, y), RegionTypeEnum.Water);
                }
            }
        }
    }
}