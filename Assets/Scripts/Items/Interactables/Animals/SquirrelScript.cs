using Codice.CM.Common;
using System.Collections;
using TMPro;
using UnityEngine;

namespace FarmerDemo
{
    public class SquirrelScript : AnimalBase
    {

        protected override void Start()
        {
            base.Start();
            AdjustAnimationSpeed(7);
        }
    }
}