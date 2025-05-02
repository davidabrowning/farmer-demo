using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class FabricatorBuildingScript : ItemInteractable, IConstructable
    {
        public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "craft_berry_basket", "Create a berry basket (5 twigs)"));
            Actions.Add(new ObjectAction(this, "craft_drill", "Create a drill (5 twigs + 5 stone)"));
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
                        PlayerScript.Instance.RemoveFromInventory(basketCost);
                        PlayerScript.Instance.SetHasBasket(true);
                    }
                    break;
                case "craft_drill":
                    List<ResourceAmount> drillCost = new List<ResourceAmount>() { 
                        new ResourceAmount(ResourceType.Twig, 5), 
                        new ResourceAmount(ResourceType.Stone, 5) };
                    if (PlayerScript.Instance.HasInInventory(drillCost))
                    {
                        PlayerScript.Instance.RemoveFromInventory(drillCost);
                        PlayerScript.Instance.HasDrill = true;
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
    }
}