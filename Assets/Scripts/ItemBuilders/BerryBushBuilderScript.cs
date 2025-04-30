using Assets.Scripts.Core;
using Assets.Scripts.Grid;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ItemBuilders
{
    public class BerryBushBuilderScript : ItemBuilderBase<BerryBushBuilderScript>
    {
        public void CreateBerryBushes(Vector2 bottomLeft, Vector2 topRight)
        {
            float bushCounter = 0;
            while (bushCounter < 5)
            {
                bushCounter++;
                Vector2Int randomLocation;
                do
                {
                    int randomX = (int)Mathf.Round(Random.Range(bottomLeft.x, topRight.x));
                    int randomY = (int)Mathf.Round(Random.Range(bottomLeft.y, topRight.y));
                    randomLocation = new Vector2Int(randomX, randomY);
                } while (GridManagerScript.Instance.IsOccupied(randomLocation));

                GameObject bush = Instantiate(Prefab, new Vector3(randomLocation.x, randomLocation.y, 1), Quaternion.identity);
                bush.transform.parent = ParentObject;
                GridManagerScript.Instance.TryPlaceObject(randomLocation, bush);
            }
        }
    }
}