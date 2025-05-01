using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public abstract class ItemBase : MonoBehaviour
    {
        public Vector2Int AnchorPosition;
        public Vector2Int Size;
        public int ClockwiseRotationDegrees;
        public List<Vector2Int> OccupiedTiles;
        public Vector2Int BottomLeft { get { return AnchorPosition; } }
        public Vector2Int TopRight { get { return AnchorPosition + Size - Vector2Int.one; } }
    }
}