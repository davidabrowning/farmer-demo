using UnityEngine;


    public class PlayerMovement : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public float TwigInventory = 0f;
        public float BerryInventory = 0f;

        private Rigidbody2D rb;
        private Vector2 movement;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
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
                movement = Vector2.zero;
                Destroy(collision.gameObject);
                TwigInventory++;
            }
            if (collision.gameObject.CompareTag("BerryBush"))
            {
                movement = Vector2.zero;

                if (TwigInventory >= 5)
                {
                    BerryBush berryBush = collision.gameObject.GetComponent<BerryBush>();
                    BerryInventory += berryBush.BerryCount;
                    berryBush.ClearBerries();
                }
            }
        }
    }