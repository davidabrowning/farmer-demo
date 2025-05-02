using UnityEngine;

namespace FarmerDemo
{
    public class StoneScript : ItemInteractable
    {
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "search_stone", "Search for berries"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "search_stone":
                    PlayerScript.Instance.AddToInventory(ResourceType.Stone, 25);
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }
    }
}