using UnityEngine;

public class TreeBuilderScript : MonoBehaviour
{
    public GameObject TreePrefab;
    public void CreateTree(Vector2 bottomLeft, Vector2 topRight)
    {
        Vector2 position = (topRight - bottomLeft) / 2 + bottomLeft;
        GameObject tree = Instantiate(TreePrefab, position, Quaternion.identity);
        tree.transform.SetParent(transform);
    }
}
