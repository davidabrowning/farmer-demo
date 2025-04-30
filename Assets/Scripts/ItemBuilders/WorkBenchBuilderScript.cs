using Assets.Scripts.Core;
using Assets.Scripts.Grid;
using UnityEngine;

namespace Assets.Scripts.ItemBuilders
{
    public class WorkBenchBuilderScript : ItemBuilderBase<WorkBenchBuilderScript>
    {
        public override Vector2Int Size { get { return new Vector2Int(2, 1); } }
    }
}