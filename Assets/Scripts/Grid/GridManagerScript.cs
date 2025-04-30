using Assets.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class GridManagerScript : MonoBehaviourSingleton<GridManagerScript>
    {
        private Dictionary<Vector2Int, GameObject> objectMap = new Dictionary<Vector2Int, GameObject>();
        public bool TryPlaceObject(Vector2Int location, GameObject gameObject)
        {
            if (IsOccupied(location))
                return false;

            objectMap[location] = gameObject;
            return true;
        }

        public GameObject GetObjectAt(Vector2Int cell)
        {
            objectMap.TryGetValue(cell, out var obj);
            return obj;
        }

        public bool IsOccupied(Vector2Int cell)
        {
            return objectMap.ContainsKey(cell);
        }
    }
}