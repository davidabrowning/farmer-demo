using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public interface IHarvestable
    {
        List<ResourceAmount> Harvest();
    }
}