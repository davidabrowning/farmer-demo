using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class CircuitMakerScript : BuildingBase
    {
        public override List<ResourceAmount> ConstructionCosts { get { return CostCalculator.CircuitMakerConstruction(); } }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "craft_circuit", "Make a batch of 5 circuits " + ResourceAmount.ListOut(CostCalculator.CircuitBatch())));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "craft_circuit":
                    StartCoroutine(TryCraftCircuitBatch());
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator TryCraftCircuitBatch()
        {
            if (!PlayerScript.Instance.ElectricityIsOn)
            {
                DialogueManagerScript.Instance.ShowDialogue("The circuit maker requires electricity!");
                yield break;
            }
            if (!PlayerScript.Instance.HasInInventory(CostCalculator.CircuitBatch()))
            {
                DialogueManagerScript.Instance.ShowDialogue("We are missing some resources for that.");
                yield break;
            }
            PlayerScript.Instance.RemoveFromInventory(CostCalculator.CircuitBatch());
            StartWorkingAnimation();
            yield return new WaitForSeconds(5);
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
            PlayerScript.Instance.AddToInventory(new ResourceAmount(ResourceType.Circuit, 5));
        }
    }
}