using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class AutoHarvesterScript : BuildingBase
    {
        public override List<ResourceAmount> ConstructionCosts { get { return CostCalculator.AutoHarvesterConstruction(); } }

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
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator AutoHarvestAdjacentTiles()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                if (PlayerScript.Instance.ElectricityIsOn)
                {
                    StartWorkingAnimation();
                    AutoHarvestTile(AnchorPosition + Vector2Int.left);
                    AutoHarvestTile(AnchorPosition + Vector2Int.up);
                    AutoHarvestTile(AnchorPosition + Vector2Int.right);
                    AutoHarvestTile(AnchorPosition + Vector2Int.down);
                }
                else
                {
                    StartIdleAnimation();
                }
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
}