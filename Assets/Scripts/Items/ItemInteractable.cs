using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo {
    public class ItemInteractable : MonoBehaviour
    {
        public List<ObjectAction> Actions = new();

        void Start()
        {
            if (gameObject.name.Contains("Tree"))
            {
                Actions.Add(new ObjectAction(this, "shake_tree", "Shake down some twigs"));
                Actions.Add(new ObjectAction(this, "cut_tree", "Cut down tree"));
            }
        }
        public void Interact(string actionId)
        {
            switch (actionId)
            {
                case "shake_tree":
                    gameObject.GetComponent<TreeScript>().DropMaxTwigs();
                    break;
                case "cut_tree":
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }
    }
}