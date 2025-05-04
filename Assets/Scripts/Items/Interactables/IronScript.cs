using UnityEngine;

namespace FarmerDemo
{
    public class IronScript : ItemInteractable
    {
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "kick_iron", "Kick"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "kick_iron":
                    DialogueManagerScript.Instance.ShowDialogue("Ouch!");
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }
    }
}