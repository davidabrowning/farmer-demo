using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class BerryBushScript : ItemInteractable
    {
        public int BerryCount = 0;
        public float MinBerryGrowthInterval = 2f;
        public float MaxBerryGrowthInterval = 8f;
        private float MaxBerryCount = 2f;
        public Sprite EmptyBushSprite;
        public Sprite FullBushSprite;

        public void Awake()
        {
            base.Awake();
            gameObject.GetComponent<SpriteRenderer>().sprite = EmptyBushSprite;
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(GrowBerries());
        }

        public IEnumerator GrowBerries()
        {
            while (BerryCount < MaxBerryCount)
            {
                float waitTime = Random.Range(MinBerryGrowthInterval, MaxBerryGrowthInterval);
                yield return new WaitForSeconds(waitTime);
                GrowOneBerry();
            }
        }

        private void GrowOneBerry()
        {
            BerryCount++;
            gameObject.GetComponent<SpriteRenderer>().sprite = FullBushSprite;
        }

        public void ClearBerries()
        {
            BerryCount = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = EmptyBushSprite;
            StartCoroutine(GrowBerries());
        }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "collect_berries", "Collect berries"));
            Actions.Add(new ObjectAction(this, "trample_bush", "Trample"));
        }

        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "collect_berries":
                    if (PlayerScript.Instance.HasBasket)
                    {
                        PlayerScript.Instance.AddToInventory(ResourceType.Berry, BerryCount);
                        ClearBerries();
                        if (PlayerScript.Instance.AmountInInventory(ResourceType.Berry) > 10)
                        {
                            DialogueManagerScript.Instance.ShowDialogue("Wow, that's a lot of berries!");
                        }
                    }
                    else
                    {
                        DialogueManagerScript.Instance.ShowDialogue("We cannot collect berries without a basket :-(");
                    }
                        break;
                case "trample_bush":
                    PlayerScript.Instance.AddToInventory(ResourceType.Twig, 2);
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }
    }
}