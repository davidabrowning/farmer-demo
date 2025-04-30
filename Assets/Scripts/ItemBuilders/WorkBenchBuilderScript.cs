using Assets.Scripts.Core;
using Assets.Scripts.Grid;
using UnityEngine;

namespace Assets.Scripts.ItemBuilders
{
    public class WorkBenchBuilderScript : ItemBuilderBase<WorkBenchBuilderScript>
    {
        public void CreateWorkBench(Vector2 bottomLeft, Vector2 topRight)
        {
            Vector2Int randomLocation;
            do
            {
                int x = Mathf.RoundToInt(Random.Range(bottomLeft.x, topRight.x - 1));
                int y = Mathf.RoundToInt(Random.Range(bottomLeft.y, topRight.y));
                randomLocation = new Vector2Int(x, y);
            } while (
                GridManagerScript.Instance.IsOccupied(randomLocation) ||
                GridManagerScript.Instance.IsOccupied(randomLocation + Vector2Int.right)
            );

            Vector3 position = new Vector3(randomLocation.x + 0.5f, randomLocation.y, 1);
            GameObject bench = Instantiate(Prefab, position, Quaternion.identity);
            bench.transform.parent = ParentObject;
            GridManagerScript.Instance.TryPlaceObject(randomLocation, bench);
        }
    }
}