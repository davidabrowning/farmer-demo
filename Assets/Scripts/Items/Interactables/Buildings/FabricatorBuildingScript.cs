using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class FabricatorBuildingScript : ItemInteractableBase, IConstructable
    {
        public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "craft_berry_basket", "Create a berry basket (5 twigs)"));
            Actions.Add(new ObjectAction(this, "craft_pickaxe", "Create a pickaxe (5 twigs + 2 stones)"));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "craft_berry_basket":
                    List<ResourceAmount> basketCost = new List<ResourceAmount>() { 
                        new ResourceAmount(ResourceType.Twig, 5) };
                    if (PlayerScript.Instance.HasInInventory(basketCost))
                    {
                        StartCoroutine(CraftBasket(basketCost));
                    }
                    else
                    {
                        DialogueManagerScript.Instance.ShowDialogue("We need a few more twigs first.");
                    }
                    break;
                case "craft_pickaxe":
                    List<ResourceAmount> pickaxeCost = new List<ResourceAmount>() { 
                        new ResourceAmount(ResourceType.Twig, 5), 
                        new ResourceAmount(ResourceType.Stone, 2) };
                    if (PlayerScript.Instance.HasInInventory(pickaxeCost))
                    {
                        StartCoroutine(CraftPickaxe(pickaxeCost));
                    }
                    else
                    {
                        DialogueManagerScript.Instance.ShowDialogue("We don't quite have the resources for a pickaxe yet.");
                    }
                    break;
                case "deconstruct":
                    PlayerScript.Instance.AddToInventory(ConstructionCosts);
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private List<ResourceAmount> GetConstructionCosts() {
            List<ResourceAmount> constructionCosts = new();
            constructionCosts.Add(new ResourceAmount(ResourceType.Twig, 5));
            return constructionCosts;
        }

        private IEnumerator CraftBasket(List<ResourceAmount> basketCost)
        {
            PlayerScript.Instance.RemoveFromInventory(basketCost);
            StartWorkingAnimation();
            yield return new WaitForSeconds(5);
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
            PlayerScript.Instance.SetHasBasket(true);
        }

        private IEnumerator CraftPickaxe(List<ResourceAmount> pickaxeCost)
        {
            PlayerScript.Instance.RemoveFromInventory(pickaxeCost);
            StartWorkingAnimation();
            yield return new WaitForSeconds(5);
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
            PlayerScript.Instance.SetHasPickaxe(true);
        }
    }
}