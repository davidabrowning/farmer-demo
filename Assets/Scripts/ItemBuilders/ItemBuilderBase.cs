using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FarmerDemo
{
    public abstract class ItemBuilderBase<T> : MonoBehaviourSingleton<T> where T : MonoBehaviourSingleton<T>
    {
        public abstract Vector2Int Size { get; }
        public GameObject Prefab;
        public Transform ParentObject;

        public bool TryBuildItem(Vector2 bottomLeft, Vector2 topRight, out GameObject obj)
        {
            obj = null;

            ItemPlacementLogic itemPlacementLogic = new ItemPlacementLogic(Size, GridManagerScript.Instance);
            if (!itemPlacementLogic.TryGetOpenTiles(bottomLeft, topRight, out List<Vector2Int> openTiles))
                return false;

            obj = InstantiateObject(openTiles);
            PlaceObject(obj);
            return true;
        }

        private GameObject InstantiateObject(List<Vector2Int> openTiles)
        {
            Vector2 visualCenter = CalculateVisualCenter(openTiles);
            GameObject obj = Instantiate(Prefab, visualCenter, Quaternion.identity);
            obj.GetComponent<ItemBase>().AnchorPosition = openTiles.First();
            obj.GetComponent<ItemBase>().Size = Size;
            obj.GetComponent<ItemBase>().OccupiedTiles = openTiles;
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
