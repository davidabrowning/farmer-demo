using FarmerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMScript : BuildingBase
{
    public override List<ResourceAmount> ConstructionCosts { get { return new() {
        new ResourceAmount(ResourceType.Circuit, 50),
        new ResourceAmount(ResourceType.Iron, 50)
    }; } }

    protected override void PopulateActions()
    {
        Actions.Add(new ObjectAction(this, "research_cure", "Research a cure for X87R-56 (3 seeds, 3 circuits)"));
        Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
    }
    public override void Interact(string actionId)
    {
        switch (actionId)
        {
            case "research_cure":
                List<ResourceAmount> researchCost = new List<ResourceAmount>() {
                        new ResourceAmount(ResourceType.Seed, 3),
                        new ResourceAmount(ResourceType.Circuit, 3)
                };
                if (!PlayerScript.Instance.ElectricityIsOn)
                {
                    DialogueManagerScript.Instance.ShowDialogue("The ARM requires electricity!");
                    break;
                }
                if (!PlayerScript.Instance.HasInInventory(researchCost))
                {
                    DialogueManagerScript.Instance.ShowDialogue("We are missing some resources for that.");
                    break;
                }
                StartCoroutine(ResearchCure(researchCost));
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

    private IEnumerator ResearchCure(List<ResourceAmount> researchCost)
    {
        PlayerScript.Instance.RemoveFromInventory(researchCost);
        StartWorkingAnimation();
        yield return new WaitForSeconds(5);
        StartIdleAnimation();
        yield return new WaitForSeconds(1);
        EraManagerScript.Instance.AdvanceEra();
    }
}
