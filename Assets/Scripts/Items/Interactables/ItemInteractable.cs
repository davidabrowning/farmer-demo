using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo {
    public abstract class ItemInteractable : ItemBase
    {
        public List<ObjectAction> Actions = new();

        protected virtual void Start()
        {
            PopulateActions();
        }
        protected abstract void PopulateActions();
        public abstract void Interact(string actionId);
    }
}