namespace FarmerDemo
{
    [System.Serializable]
    public class ObjectAction
    {
        public ItemInteractable Target;
        public string ActionId;
        public string ActionName;
        
        public ObjectAction(ItemInteractable target, string actionId, string actionName)
        {
            Target = target;
            ActionId = actionId;
            ActionName = actionName;
        }
    }
}