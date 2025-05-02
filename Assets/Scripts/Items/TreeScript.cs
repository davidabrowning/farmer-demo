using System.Collections;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class TreeScript : ItemBase
    {
        public const int MaxTwigDelay = 100;
        public void Start()
        {
            StartCoroutine(DropTwigs());
        }

        private IEnumerator DropTwigs()
        {
            while(true)
            {
                yield return new WaitForSeconds(Random.Range(1, MaxTwigDelay));
                ItemBuilderScript.Instance.TryBuildItem(BottomLeft - Vector2Int.one, TopRight + Vector2Int.one, "Twig", out GameObject builtTwig);
            }
        }

        public void DropMaxTwigs()
        {
            for (int i = 0; i < 100; i++)
                ItemBuilderScript.Instance.TryBuildItem(BottomLeft - Vector2Int.one, TopRight + Vector2Int.one, "Twig", out GameObject builtTwig);
        }
    }
}