using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class WoodBurnerScript : BuildingBase
    {
        public override List<ResourceAmount> ConstructionCosts { get { return CostCalculator.WoodBurnerConstruction(); } }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "add_wood", "Add a twig to the burner"));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "add_wood":
                    if (PlayerScript.Instance.HasInInventory(new ResourceAmount(ResourceType.Twig, 1)))
                    {
                        PlayerScript.Instance.RemoveFromInventory(new ResourceAmount(ResourceType.Twig, 1));
                        StartCoroutine(BurnATwig());
                    }
                    else
                    {
                        DialogueManagerScript.Instance.ShowDialogue("We need to gather some twigs first.");
                    }
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator BurnATwig()
        {
            StartWorkingAnimation();
            PlayerScript.Instance.AddActivePowerProducer(this);
            yield return new WaitForSeconds(10);
            StartIdleAnimation();
            PlayerScript.Instance.RemoveActivePowerProducer(this);
        }
    }
}