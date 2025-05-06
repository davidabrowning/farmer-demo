using FarmerDemo;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelScript : ItemInteractable, IConstructable
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

    private List<ResourceAmount> GetConstructionCosts()
    {
        List<ResourceAmount> constructionCosts = new();
        constructionCosts.Add(new ResourceAmount(ResourceType.Circuit, 5));
        constructionCosts.Add(new ResourceAmount(ResourceType.Twig, 15));
        return constructionCosts;
    }
}
