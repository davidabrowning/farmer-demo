using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FarmerDemo
{
    public abstract class ItemBuilderBase<T> : MonoBehaviourSingleton<T> where T : MonoBehaviourSingleton<T>
    {
        public GameObject Prefab;
        public Transform ParentObject;

        public bool TryBuildItem(Vector2 bottomLeft, Vector2 topRight, out GameObject obj)
        {
            obj = null;

            ItemPlacementLogic itemPlacementLogic = new ItemPlacementLogic(Prefab.GetComponent<ItemBase>().Size, GridManagerScript.Instance);
            if (!itemPlacementLogic.TryGetOpenTiles(bottomLeft, topRight, out List<Vector2Int> openTiles))
                return false;

            obj = InstantiateObject(openTiles);
            PlaceObject(obj);
            return true;
        }

        private GameObject InstantiateObject(List<Vector2Int> openTiles)
        {
            GameObject obj = Instantiate(Prefab, new Vector2(), Quaternion.identity);
            Vector2 visualCenter = CalculateVisualCenter(openTiles);
            obj.transform.position = visualCenter;
            obj.GetComponent<ItemBase>().AnchorPosition = openTiles.First();
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
