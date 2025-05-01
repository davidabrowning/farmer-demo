using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class GridManagerScript : MonoBehaviourSingleton<GridManagerScript>
    {
        private List<GameObject> placedObjects = new();
        public bool IsOccupied(Vector2Int cell)
        {
            return placedObjects
                .Where(obj => obj != null)
                .Select(obj => obj.GetComponent<ItemBase>())
                .Where(item => item != null)
                .Where(item => item.OccupiedTiles.Contains(cell))
                .Any();
        }

        public bool IsOccupied(List<Vector2Int> cells)
        {
            foreach (var cell in cells)
                if (IsOccupied(cell))
                    return true;
            return false;
        }

        public void AddObject(GameObject gameObject)
        {
            placedObjects.Add(gameObject);
        }

        public void Remove(GameObject gameObject)
        {
            placedObjects.Remove(gameObject);
        }
    }
}