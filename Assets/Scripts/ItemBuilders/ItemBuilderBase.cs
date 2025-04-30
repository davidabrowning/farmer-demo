using Assets.Scripts.Core;
using Assets.Scripts.Grid;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ItemBuilders
{
    public abstract class ItemBuilderBase<T> : MonoBehaviourSingleton<T> where T : MonoBehaviourSingleton<T>
    {
        public abstract Vector2Int Size { get;  }
        public GameObject Prefab;
        public Transform ParentObject;

        public bool TryBuildItem(Vector2 bottomLeft, Vector2 topRight, out GameObject obj)
        {
            obj = null;
            if (!TryGetOpenTiles(bottomLeft, topRight, out List<Vector2Int> openTiles))
                return false;

            obj = InstantiateObject(openTiles);
            PlaceObject(obj);
            return true;
        }

        private bool TryGetOpenTiles(Vector2 bottomLeft, Vector2 topRight, out List<Vector2Int> openTiles){
            openTiles = new();
            int attempts = 0;
            while (attempts++ < 100)
            {
                Vector2Int anchor = GetRandomAnchor(bottomLeft, topRight);
                openTiles = GetTargetTiles(anchor);

                if (!GridManagerScript.Instance.IsOccupied(openTiles))
                    return true;
            }
            openTiles = null;
            return false;
        }

        private Vector2Int GetRandomAnchor(Vector2 bottomLeft, Vector2 topRight)
        {
            int randomX = Mathf.RoundToInt(Random.Range(bottomLeft.x, topRight.x - (Size.x - 1)));
            int randomY = Mathf.RoundToInt(Random.Range(bottomLeft.y, topRight.y - (Size.y - 1)));
            return new Vector2Int(randomX, randomY);
        }

        private List<Vector2Int> GetTargetTiles(Vector2Int anchor)
        {
            List<Vector2Int> targetTiles = new();
            for (int x = 0; x < Size.x; x++)
                for (int y = 0; y < Size.y; y++)
                    targetTiles.Add(new Vector2Int(anchor.x + x, anchor.y + y));
            return targetTiles;
        }

        private GameObject InstantiateObject(List<Vector2Int> openTiles)
        {
            Vector2 visualCenter = CalculateVisualCenter(openTiles);
            GameObject obj = Instantiate(Prefab, visualCenter, Quaternion.identity);
            return obj;
        }

        private void PlaceObject(GameObject obj)
        {
            obj.transform.SetParent(ParentObject);
            GridManagerScript.Instance.AddObject(obj);
        }

        private Vector2 CalculateVisualCenter(List<Vector2Int> tiles)
        {
            float visualX = (float)tiles.Average(t => t.x);
            float visualY = (float)tiles.Average(t => t.y);
            return new Vector2(visualX, visualY);
        }
    }
}
