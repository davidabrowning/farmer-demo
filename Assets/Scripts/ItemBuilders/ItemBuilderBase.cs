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

        public bool TryBuildItem(Vector2 topRight, Vector2 bottomLeft, out GameObject builtObject)
        {
            builtObject = null;
            TryGetBuiltObjectTiles(topRight, bottomLeft, out List<Vector2Int> builtObjectOccupiedTiles);
            Vector2 visualCenter = CalculateVisualCenter(builtObjectOccupiedTiles);
            
            builtObject = Instantiate(Prefab, visualCenter, Quaternion.identity);
            builtObject.transform.SetParent(ParentObject);
            GridManagerScript.Instance.AddObject(builtObject);
            return true;
        }

        private bool TryGetBuiltObjectTiles(Vector2 topRight, Vector2 bottomLeft, out List<Vector2Int> builtObjectTiles){
            Vector2Int randomAnchorTilePosition;
            builtObjectTiles = new();
            do
            {
                builtObjectTiles.Clear();
                int randomX = Mathf.RoundToInt(Random.Range(bottomLeft.x, topRight.x - (Size.x - 1)));
                int randomY = Mathf.RoundToInt(Random.Range(bottomLeft.y, topRight.y - (Size.y - 1)));
                randomAnchorTilePosition = new Vector2Int(randomX, randomY);
                for (int x = 0; x < Size.x; x++)
                    for (int y = 0; y < Size.y; y++)
                        builtObjectTiles.Add(new Vector2Int(randomAnchorTilePosition.x + x, randomAnchorTilePosition.y + y));
            } while (
                GridManagerScript.Instance.IsOccupied(builtObjectTiles)
            );
            return true;
        }

        private Vector2 CalculateVisualCenter(List<Vector2Int> tiles)
        {
            float visualX = (float)tiles.Average(t => t.x);
            float visualY = (float)tiles.Average(t => t.y);
            return new Vector2(visualX, visualY);
        }
    }
}
