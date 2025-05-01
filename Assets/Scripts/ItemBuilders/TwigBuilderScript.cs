using System.Collections;
using UnityEngine;

namespace FarmerDemo
{
    public class TwigBuilderScript : ItemBuilderBase<TwigBuilderScript>
    {
        public override Vector2Int Size { get { return new Vector2Int(1, 1); } }
    }
}