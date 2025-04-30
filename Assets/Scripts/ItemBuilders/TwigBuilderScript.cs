using Assets.Scripts.Core;
using Assets.Scripts.Grid;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ItemBuilders
{
    public class TwigBuilderScript : ItemBuilderBase<TwigBuilderScript>
    {
        public override Vector2Int Size { get { return new Vector2Int(1, 1); } }
    }
}