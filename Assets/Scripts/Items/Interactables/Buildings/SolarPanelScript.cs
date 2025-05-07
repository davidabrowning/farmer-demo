using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class SolarPanelScript : BuildingBase
    {
        public override List<ResourceAmount> ConstructionCosts { get { return CostCalculator.SolarPanelConstruction(); } }

        protected override void Start()
        {
            base.Start();
            PlayerScript.Instance.AddActivePowerProducer(this);
        }
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "deconstruct":
                    PlayerScript.Instance.RemoveActivePowerProducer(this);
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }
    }
}