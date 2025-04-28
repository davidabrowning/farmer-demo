using System.Collections;
using UnityEngine;

public class BerryBushBuilderScript : MonoBehaviour
{
    public GameObject BerryBushPrefab;
    public void CreateBerryBushes(Vector2 bottomLeft, Vector2 topRight)
    {
        float bushCounter = 0;
        while (bushCounter < 5)
        {
            bushCounter++;
            Vector2 randomPosition = new Vector2(Random.Range(bottomLeft.x + 2, topRight.x - 1), Random.Range(bottomLeft.y + 2, topRight.y - 1));

            GameObject bush = Instantiate(BerryBushPrefab, randomPosition, Quaternion.identity);
            bush.transform.SetParent(transform);

            SpriteRenderer renderer = bush.GetComponent<SpriteRenderer>();
            Vector2 spriteSize = renderer.sprite.bounds.size;
            bush.transform.localScale = new Vector3(3 / spriteSize.x, 3 / spriteSize.y, 1);

        }
    }
}
