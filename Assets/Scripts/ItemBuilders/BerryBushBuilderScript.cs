using System.Collections;
using UnityEngine;

namespace FarmerDemo
{
    public class BerryBushBuilderScript : ItemBuilderBase<BerryBushBuilderScript>
    {
        public override Vector2Int Size { get { return new Vector2Int(1, 1); } }
    }
}