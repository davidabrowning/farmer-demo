using System.Collections;
using UnityEngine;

public class BerryBushBuilderController : MonoBehaviour
{
    public GameObject BerryBushPrefab;
    public void CreateBerryBushes(Vector2 bottomLeft, Vector2 topRight)
    {
        float bushCounter = 0;
        while (bushCounter < 5)
        {
            bushCounter++;
            Vector2 randomPosition = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
            GameObject bush = Instantiate(BerryBushPrefab, randomPosition, Quaternion.identity);
            bush.transform.SetParent(transform);
        }
    }
}
