using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FarmerDemo
{
    internal class ItemPlacementLogic
    {
        private readonly Vector2Int _size;
        private readonly GridManagerScript _checker;

        public ItemPlacementLogic(Vector2Int size, GridManagerScript checker)
        {
            _size = size;
            _checker = checker;
        }

        public bool TryGetOpenTiles(Vector2 bottomLeft, Vector2 topRight, out List<Vector2Int> openTiles)
        {
            openTiles = null;
            for (int attempts = 0; attempts < 100; attempts++)
            {
                Vector2Int anchor = GetRandomAnchor(bottomLeft, topRight);
                List<Vector2Int> targetTiles = GetTargetTiles(anchor);
                if (!_checker.IsOccupied(targetTiles))
                {
                    openTiles = targetTiles;
                    return true;
                }
            }
            return false;
        }

        private Vector2Int GetRandomAnchor(Vector2 bottomLeft, Vector2 topRight)
        {
            int randomX = Mathf.RoundToInt(UnityEngine.Random.Range(bottomLeft.x, topRight.x - (_size.x - 1)));
            int randomY = Mathf.RoundToInt(UnityEngine.Random.Range(bottomLeft.y, topRight.y - (_size.y - 1)));
            return new Vector2Int(randomX, randomY);
        }

        private List<Vector2Int> GetTargetTiles(Vector2Int anchor)
        {
            List<Vector2Int> targetTiles = new();
            for (int x = 0; x < _size.x; x++)
                for (int y = 0; y < _size.y; y++)
                    targetTiles.Add(new Vector2Int(anchor.x + x, anchor.y + y));
            return targetTiles;
        }

    }
}
