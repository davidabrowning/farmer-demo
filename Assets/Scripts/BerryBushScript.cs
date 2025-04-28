using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BerryBushScript : MonoBehaviour
{
    public float BerryCount { get { return _berries.Count; } }
    public float MinBerryGrowthInterval = 2f;
    public float MaxBerryGrowthInterval = 8f;
    private float MaxBerryCount = 2;
    public Sprite BerrySprite;
    private List<GameObject> _berries;

    public void Awake()
    {
        _berries = new List<GameObject>();
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
        float offsetX = Random.Range(-0.7f, 0.7f);
        float offsetY = Random.Range(-0.7f, 0.7f);
        GameObject berry = new GameObject("Berry");
        berry.transform.SetParent(transform);
        berry.transform.position = transform.position + new Vector3(offsetX, offsetY, 0);
        berry.transform.localScale = Vector3.one * 0.1f;
        SpriteRenderer renderer = berry.AddComponent<SpriteRenderer>();
        renderer.sprite = BerrySprite;
        renderer.sortingOrder = 2;
        _berries.Add(berry);
    }

    public void ClearBerries()
    {
        foreach (GameObject berry in _berries)
            Destroy(berry);
        _berries.Clear();
        StartCoroutine(GrowBerries());
    }
}