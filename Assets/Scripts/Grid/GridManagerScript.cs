using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class GridManagerScript : MonoBehaviourSingletonBase<GridManagerScript>
    {
        private List<ItemBase> placedItems = new();
        public bool IsOccupied(Vector2Int cell)
        {
            if (PlayerScript.Instance.LocationInt() == cell)
                return true;
            if (TileBuilderScript.Instance.GetRegionType(cell) == RegionTypeEnum.Water)
                return true;
            return placedItems
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

        public void AddItem(ItemBase item)
        {
            placedItems.Add(item);
        }

        public void RemoveItem(ItemBase item)
        {
            placedItems.Remove(item);
        }

        public ItemBase GetItemAt(Vector2Int location)
        {
            return placedItems.Where(p => p.OccupiedTiles.Contains(location)).FirstOrDefault();
        }
    }
}