using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    [System.Serializable]
    public class PlayerScript : MonoBehaviourSingleton<PlayerScript>
    {
        public List<ResourceAmount> ResourceInventory = new();
        public List<ItemBase> ActivePowerProducers = new();
        public float MoveSpeed = 5f;
        public bool HasBasket = false;
        public bool HasPickaxe = false;
        public bool ElectricityIsOn { get { return ActivePowerProducers.Count > 0; } }
        public event Action ElectricityStatusUpdate; // Holds listeners to electricity updates
        public GameObject BasketVisual;
        public GameObject BasketWithFewBerriesVisual;
        public GameObject BasketWithBerriesVisual;
        public GameObject PickaxeVisual;

        private Rigidbody2D rb;
        private Vector2 movement;
        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            BasketVisual.SetActive(false);
            BasketWithFewBerriesVisual.SetActive(false);
            BasketWithBerriesVisual.SetActive(false);

            // Temporary settings for testing/development
            AddToInventory(ResourceType.Twig, 100);
            AddToInventory(ResourceType.Berry, 1000);
            AddToInventory(ResourceType.Circuit, 100);
            AddToInventory(ResourceType.Iron, 100);
            AddToInventory(ResourceType.Stone, 100);
            AddToInventory(ResourceType.Fish, 1000);
            AddToInventory(ResourceType.Seed, 1000);
        }

        void Update()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            movement = new Vector2(moveX, moveY);

            if (HasBasket)
            {
                if (AmountInInventory(ResourceType.Berry) < 5)
                {
                    BasketVisual.SetActive(true);
                    BasketWithFewBerriesVisual.SetActive(false);
                    BasketWithBerriesVisual.SetActive(false);
                }
                if (AmountInInventory(ResourceType.Berry) > 5)
                {
                    BasketVisual.SetActive(false);
                    BasketWithFewBerriesVisual.SetActive(true);
                    BasketWithBerriesVisual.SetActive(false);
                }
                if (AmountInInventory(ResourceType.Berry) > 10)
                {
                    BasketVisual.SetActive(false);
                    BasketWithFewBerriesVisual.SetActive(false);
                    BasketWithBerriesVisual.SetActive(true);
                }
            }

            PickaxeVisual.SetActive(HasPickaxe);
        }

        void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime);
        }

        protected void LateUpdate()
        {
            // Multiply by -100 to convert Y to int and get a good range
            _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
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

        public void SetHasPickaxe(bool hasPickaxe)
        {
            HasPickaxe = hasPickaxe;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Twig"))
            {
                GridManagerScript.Instance.RemoveItem(gameObject.GetComponent<ItemBase>());
                Destroy(collision.gameObject);
                AddToInventory(ResourceType.Twig, 1);
                if (AmountInInventory(ResourceType.Twig) >= 5)
                    InventoryMenuManagerScript.Instance.UpdateInstructions("Craft a berry basket");
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

        public void AddActivePowerProducer(ItemBase activePowerProducer)
        {
            ActivePowerProducers.Add(activePowerProducer);
            ElectricityStatusUpdate.Invoke(); // Notify all listeners that power status has updated
        }

        public void RemoveActivePowerProducer(ItemBase powerProducer)
        {
            ActivePowerProducers.Remove(powerProducer);
            ElectricityStatusUpdate.Invoke(); // Notify all listeners that power status has updated
        }

    }
}