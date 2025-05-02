using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class PlayerScript : MonoBehaviourSingleton<PlayerScript>
    {
        public List<ResourceAmount> ResourceInventory = new();
        public float MoveSpeed = 5f;
        public bool HasBasket = false;
        public bool HasDrill = false;
        public GameObject BasketVisual;
        public GameObject BasketWithFewBerriesVisual;
        public GameObject BasketWithBerriesVisual;
        private int _allTimeTwigsCollected = 0;

        private Rigidbody2D rb;
        private Vector2 movement;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            BasketVisual.SetActive(false);
            BasketWithFewBerriesVisual.SetActive(false);
            BasketWithBerriesVisual.SetActive(false);
            ResourceInventory.Add(new ResourceAmount(ResourceType.Circuit, 5));
        }

        void Update()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            movement = new Vector2(moveX, moveY);
        }

        void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime);
        }

        public Vector2Int? LocationInt()
        {
            if (rb == null)
                return null;
            return new Vector2Int((int)Mathf.Round(rb.position.x), (int)Mathf.Round(rb.position.y));
        }

        public void SetHasBasket(bool hasBasket)
        {
            HasBasket = hasBasket;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Twig"))
            {
                GridManagerScript.Instance.Remove(gameObject);
                Destroy(collision.gameObject);
                AddToInventory(ResourceType.Twig, 1);
                if (AmountInInventory(ResourceType.Twig) >= 5)
                    InventoryMenuManagerScript.Instance.UpdateInstructions("Craft a berry basket");
                _allTimeTwigsCollected++;
                if (_allTimeTwigsCollected == 1)
                    DialogueManagerScript.Instance.ShowDialogue("Splendid! If we can collect 5 twigs we should be able to make a berry basket.");
            }
            if (collision.gameObject.CompareTag("WorkBench"))
            {
                if (AmountInInventory(ResourceType.Twig) >= 5)
                {
                    AddToInventory(ResourceType.Twig, -5);
                    HasBasket = true;
                    BasketVisual.SetActive(true);
                    InventoryMenuManagerScript.Instance.UpdateInstructions("Collect 20 berries");
                }
            }
            if (collision.gameObject.CompareTag("BerryBush"))
            {
                if (HasBasket)
                {
                    BerryBushScript berryBush = collision.gameObject.GetComponent<BerryBushScript>();
                    AddToInventory(ResourceType.Berry, berryBush.BerryCount);
                    berryBush.ClearBerries();
                    if (AmountInInventory(ResourceType.Berry) > 5)
                    {
                        BasketVisual.SetActive(false);
                        BasketWithFewBerriesVisual.SetActive(true);
                    }
                    if (AmountInInventory(ResourceType.Berry) > 10)
                    {
                        BasketVisual.SetActive(false);
                        BasketWithFewBerriesVisual.SetActive(false);
                        BasketWithBerriesVisual.SetActive(true);
                    }
                    if (AmountInInventory(ResourceType.Berry) >= 20)
                        InventoryMenuManagerScript.Instance.UpdateInstructions("You win!");
                }
            }
        }

        public void AddToInventory(ResourceType type, int amount)
        {
            if (ResourceInventory.Where(r => r.Type == type).Any())
                ResourceInventory.Where(r => r.Type == type).First().Amount += amount;
            else
                ResourceInventory.Add(new ResourceAmount(type, amount));
        }

        public void AddToInventory(ResourceAmount resourceAmount)
        {
            AddToInventory(resourceAmount.Type, resourceAmount.Amount);
        }

        public void AddToInventory(List<ResourceAmount> resourceAmounts)
        {
            foreach (ResourceAmount resourceAmount in resourceAmounts)
            {
                AddToInventory(resourceAmount.Type, resourceAmount.Amount);
            }
        }

        public void RemoveFromInventory(ResourceAmount resourceAmount)
        {
            AddToInventory(resourceAmount.Type, resourceAmount.Amount * -1);
        }

        public void RemoveFromInventory(List<ResourceAmount> resourceAmounts)
        {
            foreach (ResourceAmount resourceAmount in resourceAmounts)
            {
                RemoveFromInventory(resourceAmount);
            }
        }

        public int AmountInInventory(ResourceType type)
        {
            return ResourceInventory.Where(r => r.Type == type).Select(r => r.Amount).Sum();
        }

        public bool HasInInventory(ResourceAmount resourceAmount)
        {
            return ResourceInventory.Where(r => r.Type == resourceAmount.Type).Select(r => r.Amount).Sum() >= resourceAmount.Amount;
        }

        public bool HasInInventory(List<ResourceAmount> resourceAmounts)
        {
            foreach (ResourceAmount resourceAmount in resourceAmounts)
                if (!HasInInventory(resourceAmount))
                    return false;
            return true;
        }

        public string GetInventoryAsString()
        {
            string output = "";
            foreach (ResourceAmount resourceAmount in ResourceInventory)
                output += resourceAmount.ToString();
            return output;
        }
    }
}