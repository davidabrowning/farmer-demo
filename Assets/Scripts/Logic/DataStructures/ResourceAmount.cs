
using System.Collections.Generic;

namespace FarmerDemo
{
    [System.Serializable]
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
        public static string ListOut(List<ResourceAmount> resourceAmountList)
        {
            string result = "";
            foreach (ResourceAmount resourceAmount in resourceAmountList)
                result += resourceAmount.ToString() ;
            return result;
        }
    }
}