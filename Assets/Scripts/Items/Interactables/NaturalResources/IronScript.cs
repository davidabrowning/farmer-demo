using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class IronScript : ItemInteractable, IHarvestable
    {
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "mine_iron", "Mine iron (requires pickaxe)"));
            Actions.Add(new ObjectAction(this, "kick_iron", "Kick"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "mine_iron":
                    if (PlayerScript.Instance.HasPickaxe)
                        PlayerScript.Instance.AddToInventory(ResourceType.Iron, Random.Range(10, 20));
                    else
                        DialogueManagerScript.Instance.ShowDialogue("We need a pickaxe to do that.");
                    break;
                case "kick_iron":
                    DialogueManagerScript.Instance.ShowDialogue("Ouch!");
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }

        public List<ResourceAmount> Harvest()
        {
            List<ResourceAmount> harvestedIron = new List<ResourceAmount>(){
                new ResourceAmount(ResourceType.Iron, 1)
            };
            return harvestedIron;
        }
    }
}