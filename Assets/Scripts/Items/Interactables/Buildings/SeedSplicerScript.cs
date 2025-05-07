using FarmerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSplicerScript : ItemInteractableBase, IConstructable
{
    public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }

    protected override void PopulateActions()
    {
        Actions.Add(new ObjectAction(this, "splice_seeds", "Splice seeds (100 berry, 100 fish, 1 circuit)"));
        Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
    }
    public override void Interact(string actionId)
    {
        switch (actionId)
        {
            case "splice_seeds":
                List<ResourceAmount> spliceCost = new List<ResourceAmount>() {
                        new ResourceAmount(ResourceType.Berry, 100),
                        new ResourceAmount(ResourceType.Fish, 100),
                        new ResourceAmount(ResourceType.Circuit, 1)
                };
                if (!PlayerScript.Instance.ElectricityIsOn)
                {
                    DialogueManagerScript.Instance.ShowDialogue("The seed splicer requires electricity!");
                    break;
                }
                if (!PlayerScript.Instance.HasInInventory(spliceCost))
                {
                    DialogueManagerScript.Instance.ShowDialogue("We are missing some resources for that.");
                    break;
                }
                StartCoroutine(SpliceSeeds(spliceCost));
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
        constructionCosts.Add(new ResourceAmount(ResourceType.Circuit, 3));
        constructionCosts.Add(new ResourceAmount(ResourceType.Stone, 5));
        return constructionCosts;
    }

    private IEnumerator SpliceSeeds(List<ResourceAmount> spliceCost)
    {
        PlayerScript.Instance.RemoveFromInventory(spliceCost);
        StartWorkingAnimation();
        yield return new WaitForSeconds(5);
        StartIdleAnimation();
        yield return new WaitForSeconds(1);
        PlayerScript.Instance.AddToInventory(new ResourceAmount(ResourceType.Seed, 1));
    }
}
