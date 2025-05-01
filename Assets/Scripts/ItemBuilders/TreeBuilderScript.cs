using UnityEngine;

namespace FarmerDemo
{
    public class TreeBuilderScript : ItemBuilderBase<TreeBuilderScript>
    {
        public override Vector2Int Size { get { return new Vector2Int(1, 2); } }
    }
}