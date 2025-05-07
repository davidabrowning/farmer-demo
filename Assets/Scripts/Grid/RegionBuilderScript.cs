using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace FarmerDemo
{
    public class RegionBuilderScript : MonoBehaviourSingletonBase<RegionBuilderScript>
    {
        private int _regionSize = 10;

        public void BuildRegion(Vector2Int regionCoords, RegionTypeEnum regionType)
        {
            Vector2Int bottomLeft = GetRegionBottomLeft(regionCoords);
            Vector2Int topRight = GetRegionTopRight(regionCoords);
            PlaceTiles(bottomLeft, topRight, regionType);
            PlaceItems(bottomLeft, topRight, regionType);
        }

        private Vector2Int GetRegionBottomLeft(Vector2Int regionCoords)
        {
            int minX = regionCoords.x * _regionSize;
            int minY = regionCoords.y * _regionSize;
            return new Vector2Int(minX, minY);
        }

        private Vector2Int GetRegionTopRight(Vector2Int regionCoords)
        {
            int maxX = regionCoords.x * _regionSize + _regionSize - 1;
            int maxY = regionCoords.y * _regionSize + _regionSize - 1;
            return new Vector2Int(maxX, maxY);
        }

        private void PlaceItems(Vector2Int bottomLeft, Vector2Int topRight, RegionTypeEnum regionType)
        {
            switch (regionType)
            {
                case RegionTypeEnum.Bush:
                    for (int i = 0; i < 5; i++)
                        ItemBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, "BerryBush", out GameObject builtBerryBush);
                    for (int i = 0; i < 3; i++)
                        ItemBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, "Tree", out GameObject builtTree);
                    for (int i = 0; i < 1; i++)
                        ItemBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, "Squirrel", out GameObject builtSquirrel);
                    break;
                case RegionTypeEnum.Tree:
                    for (int i = 0; i < 3; i++)
                        ItemBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, "Tree", out GameObject builtTree);
                    break;
                case RegionTypeEnum.Dirt:
                    ItemBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, "Stone", out GameObject builtStoneDeposit);
                    ItemBuilderScript.Instance.TryBuildItem(bottomLeft, topRight, "Iron", out GameObject builtIronDeposit);
                    break;
                case RegionTypeEnum.Water:
                    break;
                default:
                    break;
            }
        }

        private void PlaceTiles(Vector2Int bottomLeft, Vector2Int topRight, RegionTypeEnum regionType)
        {
            for (int x = bottomLeft.x; x <= topRight.x; x++)
            {
                for (int y = bottomLeft.y; y <= topRight.y; y++)
                {
                    TileBuilderScript.Instance.PlaceTile(regionType, new Vector2Int(x, y));
                }
            }
        }
    }
}