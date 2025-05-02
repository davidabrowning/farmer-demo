using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class ConstructionManagerScript : MonoBehaviourSingleton<ConstructionManagerScript>
    {
        public bool BuildModeOn = false;
        public string ItemName = "";
        public GameObject ItemPrefab;
        public Vector2Int ItemPrefabSize;
        private void Update()
        {
            if (BuildModeOn)
            {
                Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int mousePos = new Vector2Int((int)Mathf.Round(mouseWorld.x), (int)Mathf.Round(mouseWorld.y));
                List<Vector2Int> targetTiles = new();
                for (int x = 0; x < ItemPrefabSize.x; x++)
                    for (int y = 0; y < ItemPrefabSize.y; y++)
                        targetTiles.Add(mousePos + new Vector2Int(x, y));

                if (!GridManagerScript.Instance.IsOccupied(targetTiles))
                    GridHighlighterScript.Instance.Highlight(targetTiles);

                if (Input.GetMouseButtonDown(0)) // left-click
                {
                    if (ItemBuilderScript.Instance.TryBuildItemAtSpecificLocation(targetTiles, ItemName, out GameObject builtObj))
                    {
                        GridHighlighterScript.Instance.Hide();
                        ExitBuildMode();
                    }
                }
            }
        }
        public void EnterBuildMode()
        {
            BuildModeOn = true;
        }
        public void ExitBuildMode()
        {
            BuildModeOn = false;
            GridHighlighterScript.Instance.Hide();
        }
        public void ToggleBuildMode(string itemName)
        {
            ItemName = itemName;
            ItemPrefab = Resources.Load<GameObject>("Prefabs/" + ItemName);
            ItemPrefabSize = ItemPrefab.GetComponent<ItemBase>().Size;
            if (BuildModeOn)
                ExitBuildMode();
            else
                EnterBuildMode();
        }
    }
}