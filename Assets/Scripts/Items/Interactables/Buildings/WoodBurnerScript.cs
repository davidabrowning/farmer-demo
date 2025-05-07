using FarmerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBurnerScript : ItemInteractable, IConstructable
{
    public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }

    protected override void PopulateActions()
    {
        Actions.Add(new ObjectAction(this, "add_wood", "Add a twig to the burner"));
        Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
    }
    public override void Interact(string actionId)
    {
        switch (actionId)
        {
            case "add_wood":
                if (PlayerScript.Instance.HasInInventory(new ResourceAmount(ResourceType.Twig, 1)))
                {
                    PlayerScript.Instance.RemoveFromInventory(new ResourceAmount(ResourceType.Twig, 1));
                    StartCoroutine(BurnATwig());
                }
                else
                {
                    DialogueManagerScript.Instance.ShowDialogue("We need to gather some twigs first.");
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
        constructionCosts.Add(new ResourceAmount(ResourceType.Stone, 50));
        return constructionCosts;
    }

    private IEnumerator BurnATwig()
    {
        StartWorkingAnimation();
        PlayerScript.Instance.AddActivePowerProducer(this);
        yield return new WaitForSeconds(10);
        StartIdleAnimation();
        PlayerScript.Instance.RemoveActivePowerProducer(this);
    }
}
