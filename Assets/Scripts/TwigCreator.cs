using System.Collections;
using UnityEngine;

public class TwigCreator : MonoBehaviour
{
    public GameObject TwigPrefab;
    
    void Start()
    {
        StartCoroutine(CreateTwigs());
    }

    void Update()
    {
        
    }

    private IEnumerator CreateTwigs()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            float spawnRange = 1;

            Vector2 randomPosition = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
            GameObject twig = Instantiate(TwigPrefab, randomPosition, Quaternion.identity);
            twig.transform.SetParent(transform);
        }
    }
}
