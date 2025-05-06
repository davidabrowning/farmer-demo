using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class LabBuildingScript : ItemInteractable, IConstructable
    {
        public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }
        public int ResearchProgress = 0;
        private int currentEraChecker = 0;
        protected override void PopulateActions()
        {
            if (GameManagerScript.Instance.CurrentEra == 0)
                Actions.Add(new ObjectAction(this, "berry_research", "Study berries"));
            if (GameManagerScript.Instance.CurrentEra == 1)
                Actions.Add(new ObjectAction(this, "circuit_research", "Study circuits"));
            if (GameManagerScript.Instance.CurrentEra == 2)
                Actions.Add(new ObjectAction(this, "fish_research", "Study fish"));
            if (GameManagerScript.Instance.CurrentEra == 3)
                Actions.Add(new ObjectAction(this, "seed_research", "Study seeds"));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }

        private void Update()
        {
            // Update Actions if game era has changed
            if (GameManagerScript.Instance.CurrentEra != currentEraChecker)
            {
                currentEraChecker = GameManagerScript.Instance.CurrentEra;
                Actions.Clear();
                PopulateActions();
            }
        }

        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "berry_research":
                    StartCoroutine(PerformResearch(new ResourceAmount(ResourceType.Berry, 1)));
                    break;
                case "circuit_research":
                    StartCoroutine(PerformResearch(new ResourceAmount(ResourceType.Circuit, 1)));
                    break;
                case "fish_research":
                    StartCoroutine(PerformResearch(new ResourceAmount(ResourceType.Fish, 1)));
                    break;
                case "seed_research":
                    StartCoroutine(PerformResearch(new ResourceAmount(ResourceType.Seed, 1)));
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

        private IEnumerator PerformResearch(ResourceAmount researchStepCost)
        {
            if (!PlayerScript.Instance.HasInInventory(researchStepCost))
            {
                DialogueManagerScript.Instance.ShowDialogue("We don't have any units of " + researchStepCost.Type.ToString().ToLower() + ".");
                yield break;
            }
            while (ResearchProgress < 100 && PlayerScript.Instance.HasInInventory(researchStepCost))
            {
                PlayerScript.Instance.RemoveFromInventory(researchStepCost);
                StartWorkingAnimation();
                yield return new WaitForSeconds(2);
                ResearchProgress += 10;
            }
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
            if (ResearchProgress >= 100)
            {
                GameManagerScript.Instance.AdvanceEra();
                ResearchProgress = 0;                
            }
            else
            {
                DialogueManagerScript.Instance.ShowDialogue("Research progress: " + ResearchProgress + "%. We need to input a few more units of " + researchStepCost.Type.ToString().ToLower() + " for study.");
            }
        }
    }
}