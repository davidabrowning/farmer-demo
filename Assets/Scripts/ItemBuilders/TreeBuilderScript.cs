using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.ItemBuilders 
{ 
public class TreeBuilderScript : ItemBuilderBase<TreeBuilderScript>
    {
    public void CreateTree(Vector2 bottomLeft, Vector2 topRight)
    {
        Vector2 position = (topRight - bottomLeft) / 2 + bottomLeft;
        GameObject tree = Instantiate(Prefab, position, Quaternion.identity);
        tree.transform.SetParent(transform);
    }
}
}