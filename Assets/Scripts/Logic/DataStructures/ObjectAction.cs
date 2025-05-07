namespace FarmerDemo
{
    [System.Serializable]
    public class ObjectAction
    {
        public ItemInteractableBase Target;
        public string ActionId;
        public string ActionName;
        
        public ObjectAction(ItemInteractableBase target, string actionId, string actionName)
        {
            Target = target;
            ActionId = actionId;
            ActionName = actionName;
        }
    }
}