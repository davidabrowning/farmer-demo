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
            Vector2 bottomLeft = new Vector2(minX, minY);
            Vector2 topRight = new Vector2(maxX, maxY);
            switch (regionType)
            {
                case RegionTypeEnum.Bush:
                    for(int i = 0; i < 5; i++)
                        BerryBushBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, out GameObject builtBerryBush);
                    break;
                case RegionTypeEnum.Tree:
                    for (int i = 0; i < 3; i++)
                        TreeBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, out GameObject builtTree);
                    break;
                case RegionTypeEnum.Dirt:
                    WorkBenchBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, out GameObject builtBench);
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