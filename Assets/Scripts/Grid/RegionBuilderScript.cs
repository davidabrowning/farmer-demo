using Assets.Scripts.ItemBuilders;
using Assets.Scripts.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace Assets.Scripts.Grid
{
    public class RegionBuilderScript : MonoBehaviourSingleton<RegionBuilderScript>
    {
        private float RegionSize = 15f;

        public void BuildRegion(Vector2 regionCoords, RegionTypeEnum regionType)
        {
            float minX = regionCoords.x * RegionSize;
            float maxX = regionCoords.x * RegionSize + RegionSize - 1;
            float minY = regionCoords.y * RegionSize;
            float maxY = regionCoords.y * RegionSize + RegionSize - 1;
            switch (regionType)
            {
                case RegionTypeEnum.Bush:
                    BerryBushBuilderScript.Instance.CreateBerryBushes(new Vector2(minX, minY), new Vector2(maxX, maxY));
                    break;
                case RegionTypeEnum.Tree:
                    TreeBuilderScript.Instance.CreateTree(new Vector2(minX, minY), new Vector2(maxX, maxY));
                    TwigBuilderScript.Instance.CreateTwigs(new Vector2(minX, minY), new Vector2(maxX, maxY));
                    break;
                case RegionTypeEnum.Dirt:
                    WorkBenchBuilderScript.Instance.CreateWorkBench(new Vector2(minX, minY), new Vector2(maxX, maxY));
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
                    TileBuilderScript.Instance.PlaceTile(regionType, new Vector2(x, y));
                }
            }
        }
    }
}