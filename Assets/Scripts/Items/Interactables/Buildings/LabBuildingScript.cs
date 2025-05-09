using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class LabBuildingScript : BuildingBase
    {
        public override List<ResourceAmount> ConstructionCosts { get { return CostCalculator.LaboratoryConstruction(); } }
        public int ResearchProgress = 0;

        private void OnEnable()
        {
            EraManagerScript.Instance.EraUpdate += HandleEraUpdate;
        }
        private void OnDisable()
        {
            EraManagerScript.Instance.EraUpdate -= HandleEraUpdate;
        }
        private void HandleEraUpdate()
        {
            Actions.Clear();
            PopulateActions();
        }
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "research", "Study a sample " + CostCalculator.StandardResearch()));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }

        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "research":
                    StartCoroutine(TryPerformResearch());
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator TryPerformResearch()
        {
            if (!PlayerScript.Instance.HasInInventory(CostCalculator.StandardResearch()))
            {
                DialogueManagerScript.Instance.ShowDialogue("We don't have any units of " + CostCalculator.StandardResearch().Type.ToString().ToLower() + ".");
                yield break;
            }
            while (ResearchProgress < 100 && PlayerScript.Instance.HasInInventory(CostCalculator.StandardResearch()))
            {
                PlayerScript.Instance.RemoveFromInventory(CostCalculator.StandardResearch());
                StartWorkingAnimation();
                yield return new WaitForSeconds(2);
                ResearchProgress += 10;
            }
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
            if (ResearchProgress >= 100)
            {
                EraManagerScript.Instance.AdvanceEra();
                ResearchProgress = 0;                
            }
            else
            {
                DialogueManagerScript.Instance.ShowDialogue("Research progress: " + ResearchProgress + "%. We need to input a few more units of " + CostCalculator.StandardResearch().Type.ToString().ToLower() + " for study.");
            }
        }
    }
}