namespace FarmerDemo
{
    public class ResourceAmount
    {
        public ResourceType Type;
        public int Amount;
        public ResourceAmount(ResourceType resourceType, int resourceAmount)
        {
            Type = resourceType;
            Amount = resourceAmount;
        }
        public override string ToString()
        {
            return $"[{Type}: {Amount}]";
        }
    }
}