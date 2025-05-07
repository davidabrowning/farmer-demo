using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo {
    public abstract class ItemInteractable : ItemBase
    {
        public List<ObjectAction> Actions = new();

        protected override void Start()
        {
            base.Start();
            PopulateActions();
        }

        protected abstract void PopulateActions();
        public abstract void Interact(string actionId);
    }
}