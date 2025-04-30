using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class BerryBushScript : ItemBase
    {
        public float BerryCount = 0f;
        public float MinBerryGrowthInterval = 2f;
        public float MaxBerryGrowthInterval = 8f;
        private float MaxBerryCount = 2f;
        public Sprite EmptyBushSprite;
        public Sprite FullBushSprite;

        public void Awake()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = EmptyBushSprite;
        }

        public void Start()
        {
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
    }
}