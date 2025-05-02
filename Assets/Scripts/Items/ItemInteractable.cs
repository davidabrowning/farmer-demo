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
                    Debug.Log("Shaking tree...");
                    break;
                case "cut_tree":
                    Debug.Log("Cutting down tree...");
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }
    }
}