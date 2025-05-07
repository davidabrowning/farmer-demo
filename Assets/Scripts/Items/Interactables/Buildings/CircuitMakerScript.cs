using FarmerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitMakerScript : ItemInteractableBase, IConstructable
{
    public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }

    protected override void PopulateActions()
    {
        Actions.Add(new ObjectAction(this, "craft_circuit", "Make a batch of 5 circuits (2 berry, 2 iron)"));
        Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
    }
    public override void Interact(string actionId)
    {
        switch (actionId)
        {
            case "craft_circuit":
                List<ResourceAmount> circuitCost = new List<ResourceAmount>() {
                        new ResourceAmount(ResourceType.Berry, 2),
                        new ResourceAmount(ResourceType.Iron, 2)
                };
                if (!PlayerScript.Instance.ElectricityIsOn)
                {
                    DialogueManagerScript.Instance.ShowDialogue("The circuit maker requires electricity!");
                    break;
                }
                if (!PlayerScript.Instance.HasInInventory(circuitCost))
                {
                    DialogueManagerScript.Instance.ShowDialogue("We are missing some resources for that.");
                    break;
                }
                StartCoroutine(CraftCircuit(circuitCost));
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
        constructionCosts.Add(new ResourceAmount(ResourceType.Iron, 7));
        return constructionCosts;
    }

    private IEnumerator CraftCircuit(List<ResourceAmount> circuitCost)
    {
        PlayerScript.Instance.RemoveFromInventory(circuitCost);
        StartWorkingAnimation();
        yield return new WaitForSeconds(5);
        StartIdleAnimation();
        yield return new WaitForSeconds(1);
        PlayerScript.Instance.AddToInventory(new ResourceAmount(ResourceType.Circuit, 5));
    }
}
