using Assets.Scripts.Core;
using Assets.Scripts.Grid;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ItemBuilders
{
    public class TwigBuilderScript : ItemBuilderBase<TwigBuilderScript>
    {
        public override Vector2Int Size { get { return new Vector2Int(1, 1); } }
        public void CreateTwigs(Vector2 bottomLeft, Vector2 topRight)
        {
            StartCoroutine(RunTwigCreation(bottomLeft, topRight));
        }

        private IEnumerator RunTwigCreation(Vector2 bottomLeft, Vector2 topRight)
        {
            while (true)
            {
                yield return new WaitForSeconds(2);

                Vector2Int randomLocation;
                do
                {
                    int randomX = (int)Mathf.Round(Random.Range(bottomLeft.x, topRight.x));
                    int randomY = (int)Mathf.Round(Random.Range(bottomLeft.y, topRight.y));
                    randomLocation = new Vector2Int(randomX, randomY);
                } while (GridManagerScript.Instance.IsOccupied(randomLocation));

                GameObject twig = Instantiate(Prefab, new Vector3(randomLocation.x, randomLocation.y, 1), Quaternion.identity);
                twig.transform.SetParent(transform);
                GridManagerScript.Instance.AddObject(twig);
            }
        }
    }
}