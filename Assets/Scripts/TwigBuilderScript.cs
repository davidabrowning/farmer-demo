using System.Collections;
using UnityEngine;

public class TwigBuilderScript : MonoBehaviour
{
    public GameObject TwigPrefab;
    
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void CreateTwigs(Vector2 bottomLeft, Vector2 topRight)
    {
        StartCoroutine(RunTwigCreation(bottomLeft, topRight));
    }

    private IEnumerator RunTwigCreation(Vector2 bottomLeft, Vector2 topRight)
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            Vector2 randomPosition = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
            GameObject twig = Instantiate(TwigPrefab, randomPosition, Quaternion.identity);
            twig.transform.SetParent(transform);
        }
    }
}
