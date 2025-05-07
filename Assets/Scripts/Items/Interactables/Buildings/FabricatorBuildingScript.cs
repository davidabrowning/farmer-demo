using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class FabricatorBuildingScript : BuildingBase
    {
        public override List<ResourceAmount> ConstructionCosts { get { return CostCalculator.FabricatorConstruction(); } }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "craft_berry_basket", "Craft berry basket " + ResourceAmount.ListOut(CostCalculator.BerryBasket())));
            Actions.Add(new ObjectAction(this, "craft_pickaxe", "Craft pickaxe " + ResourceAmount.ListOut(CostCalculator.Pickaxe())));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "craft_berry_basket":
                    StartCoroutine(TryCraftBerryBasket());
                    break;
                case "craft_pickaxe":
                    StartCoroutine(TryCraftPickaxe());
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator TryCraftBerryBasket()
        {
            if (PlayerScript.Instance.HasInInventory(CostCalculator.BerryBasket()))
            {
                PlayerScript.Instance.RemoveFromInventory(CostCalculator.BerryBasket());
                StartWorkingAnimation();
                yield return new WaitForSeconds(5);
                StartIdleAnimation();
                yield return new WaitForSeconds(1);
                PlayerScript.Instance.SetHasBasket(true);
            }
            else
            { 
                DialogueManagerScript.Instance.ShowDialogue("We don't have enough resources.");
            }
        }

        private IEnumerator TryCraftPickaxe()
        {
            if (PlayerScript.Instance.HasInInventory(CostCalculator.Pickaxe()))
            {
                PlayerScript.Instance.RemoveFromInventory(CostCalculator.Pickaxe());
                StartWorkingAnimation();
                yield return new WaitForSeconds(5);
                StartIdleAnimation();
                yield return new WaitForSeconds(1);
                PlayerScript.Instance.SetHasPickaxe(true);
            }
            else
            {
                DialogueManagerScript.Instance.ShowDialogue("We don't have enough resources.");
            }
        }
    }
}