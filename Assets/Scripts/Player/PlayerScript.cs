using UnityEngine;

namespace FarmerDemo
{
    public class PlayerScript : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public float TwigInventory = 0f;
        public float BerryInventory = 0f;
        public bool HasBasket = false;
        public GameObject BasketVisual;
        public GameObject BasketWithFewBerriesVisual;
        public GameObject BasketWithBerriesVisual;

        private Rigidbody2D rb;
        private Vector2 movement;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            BasketVisual.SetActive(false);
            BasketWithFewBerriesVisual.SetActive(false);
            BasketWithBerriesVisual.SetActive(false);
        }

        // Update is called once per frame
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

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Twig"))
            {
                GridManagerScript.Instance.Remove(gameObject);
                Destroy(collision.gameObject);
                TwigInventory++;
                if (TwigInventory >= 5)
                    UIControllerScript.Instance.UpdateInstructions("Craft a berry basket");
            }
            if (collision.gameObject.CompareTag("WorkBench"))
            {
                if (TwigInventory >= 5)
                {
                    TwigInventory -= 5;
                    HasBasket = true;
                    BasketVisual.SetActive(true);
                    UIControllerScript.Instance.UpdateInstructions("Collect 20 berries");
                }
            }
            if (collision.gameObject.CompareTag("BerryBush"))
            {
                if (HasBasket)
                {
                    BerryBushScript berryBush = collision.gameObject.GetComponent<BerryBushScript>();
                    BerryInventory += berryBush.BerryCount;
                    berryBush.ClearBerries();
                    if (BerryInventory >5)
                    {
                        BasketVisual.SetActive(false);
                        BasketWithFewBerriesVisual.SetActive(true);
                    }
                    if (BerryInventory > 10)
                    {
                        BasketVisual.SetActive(false);
                        BasketWithFewBerriesVisual.SetActive(false);
                        BasketWithBerriesVisual.SetActive(true);
                    }
                    if (BerryInventory >= 20)
                        UIControllerScript.Instance.UpdateInstructions("You win!");
                }
            }
        }
    }
}