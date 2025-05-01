using UnityEngine;

namespace FarmerDemo
{
    public class WorkBenchBuilderScript : ItemBuilderBase<WorkBenchBuilderScript>
    {
        public override Vector2Int Size { get { return new Vector2Int(2, 1); } }
    }
}