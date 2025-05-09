﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FarmerDemo
{
    public class ItemBuilderScript : MonoBehaviourSingletonBase<ItemBuilderScript>
    {
        private GameObject _prefab;
        public Transform ParentObject;

        public bool TryBuildItemAtSpecificLocation(List<Vector2Int> location, string prefabName, out GameObject obj)
        {
            obj = null;

            if (GridManagerScript.Instance.IsOccupied(location))
                return false;

            _prefab = Resources.Load<GameObject>("Prefabs/World/" + prefabName);
            obj = InstantiateObject(location);
            PlaceObject(obj);
            return true;
        }
        public bool TryBuildItem(Vector2 bottomLeft, Vector2 topRight, string prefabName, out GameObject obj)
        {
            obj = null;

            _prefab = Resources.Load<GameObject>("Prefabs/World/" + prefabName);
            ItemPlacementLogic itemPlacementLogic = new ItemPlacementLogic(_prefab.GetComponent<ItemBase>().Size, GridManagerScript.Instance);
            if (!itemPlacementLogic.TryGetOpenTiles(bottomLeft, topRight, out List<Vector2Int> openTiles))
                return false;

            obj = InstantiateObject(openTiles);
            PlaceObject(obj);
            return true;
        }

        private GameObject InstantiateObject(List<Vector2Int> openTiles)
        {
            GameObject obj = Instantiate(_prefab, new Vector2(), Quaternion.identity);
            Vector2 visualCenter = CalculateVisualCenter(openTiles);
            obj.transform.position = visualCenter;
            obj.GetComponent<ItemBase>().AnchorPosition = openTiles.First();
            obj.GetComponent<ItemBase>().OccupiedTiles = openTiles;
            return obj;
        }

        private void PlaceObject(GameObject obj)
        {
            obj.transform.SetParent(ParentObject);
            GridManagerScript.Instance.AddItem(obj.GetComponent<ItemBase>());
        }

        private Vector2 CalculateVisualCenter(List<Vector2Int> tiles)
        {
            float visualX = (float)tiles.Average(t => t.x);
            float visualY = (float)tiles.Average(t => t.y);
            return new Vector2(visualX, visualY);
        }
    }
}
