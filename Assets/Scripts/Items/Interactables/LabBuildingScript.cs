using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class LabBuildingScript : ItemInteractable, IConstructable
    {
        public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }
        public int ResearchProgress = 0;
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "berry_research", "Study berries"));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }

        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "berry_research":
                    while (ResearchProgress < 100 && PlayerScript.Instance.HasInInventory(new ResourceAmount(ResourceType.Berry, 1)))
                    {
                        PlayerScript.Instance.RemoveFromInventory(new ResourceAmount(ResourceType.Berry, 1));
                        ResearchProgress += 10;
                    }
                    if (ResearchProgress < 100)
                    {
                        DialogueManagerScript.Instance.ShowDialogue("Research progress: " + ResearchProgress + "%. We need to input a few more berries for study.");
                    }
                    else
                    {
                        GameManagerScript.Instance.AdvanceEra();
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


        private List<ResourceAmount> GetConstructionCosts()
        {
            List<ResourceAmount> constructionCosts = new();
            constructionCosts.Add(new ResourceAmount(ResourceType.Berry, 6));
            return constructionCosts;
        }
    }
}