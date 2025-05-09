using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class SeedSplicerScript : BuildingBase
    {
        public override List<ResourceAmount> ConstructionCosts { get { return CostCalculator.SeedSplicerConstruction(); } }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "splice_seeds", "Splice seeds " + ResourceAmount.ListOut(CostCalculator.SplicedSeeds())));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "splice_seeds":
                    StartCoroutine(TrySpliceSeeds());
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator TrySpliceSeeds()
        {
            if (!PlayerScript.Instance.ElectricityIsOn)
            {
                DialogueManagerScript.Instance.ShowDialogue("The seed splicer requires electricity!");
                yield break;
            }
            if (!PlayerScript.Instance.HasInInventory(CostCalculator.SplicedSeeds()))
            {
                DialogueManagerScript.Instance.ShowDialogue("We are missing some resources for that.");
                yield break;
            }
            PlayerScript.Instance.RemoveFromInventory(CostCalculator.SplicedSeeds());
            StartWorkingAnimation();
            yield return new WaitForSeconds(5);
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
            PlayerScript.Instance.AddToInventory(new ResourceAmount(ResourceType.Seed, 1));
        }
    }
}