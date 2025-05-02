using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class FabricatorBuildingScript : ItemInteractable, IConstructable
    {
        public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
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
            constructionCosts.Add(new ResourceAmount(ResourceType.Circuit, 5));
            return constructionCosts;
        }
    }
}