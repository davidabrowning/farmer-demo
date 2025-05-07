using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public abstract class BuildingBase : ItemInteractableBase, IConstructable
    {
        abstract public List<ResourceAmount> ConstructionCosts { get; }
    }
}