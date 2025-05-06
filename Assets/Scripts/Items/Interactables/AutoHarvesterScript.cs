using FarmerDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoHarvesterScript : ItemInteractable, IConstructable
{
    public List<ResourceAmount> ConstructionCosts { get { return GetConstructionCosts(); } }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AutoHarvestAdjacentTiles());
    }
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
        constructionCosts.Add(new ResourceAmount(ResourceType.Circuit, 2));
        constructionCosts.Add(new ResourceAmount(ResourceType.Berry, 5));
        return constructionCosts;
    }

    private IEnumerator AutoHarvestAdjacentTiles()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            AutoHarvestTile(AnchorPosition + Vector2Int.left);
            AutoHarvestTile(AnchorPosition + Vector2Int.up);
            AutoHarvestTile(AnchorPosition + Vector2Int.right);
            AutoHarvestTile(AnchorPosition + Vector2Int.down);
        }
    }

    private void AutoHarvestTile(Vector2Int location)
    {
        AutoHarvestItem(location);
        AutoHarvestRegionType(location);
    }

    private void AutoHarvestItem(Vector2Int location)
    {
        ItemBase item = GridManagerScript.Instance.GetItemAt(location);
        if (item != null && item is IHarvestable)
        {
            IHarvestable harvestable = (IHarvestable)item;
            PlayerScript.Instance.AddToInventory(harvestable.Harvest());
        }
    }

    private void AutoHarvestRegionType(Vector2Int location)
    {
        if (TileBuilderScript.Instance.GetRegionType(location) == RegionTypeEnum.Water)
        {
            PlayerScript.Instance.AddToInventory(ResourceType.Fish, 1);
        }
    }
}
