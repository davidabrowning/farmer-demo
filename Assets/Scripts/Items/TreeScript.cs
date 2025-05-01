using System.Collections;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class TreeScript : ItemBase
    {
        public const int MaxTwigDelay = 10;
        public void Start()
        {
            StartCoroutine(DropTwigs());
        }

        private IEnumerator DropTwigs()
        {
            while(true)
            {
                yield return new WaitForSeconds(Random.Range(1, MaxTwigDelay));
                TwigBuilderScript.Instance.TryBuildItem(BottomLeft - Vector2Int.one, TopRight + Vector2Int.one, out GameObject builtTwig);
            }
        }
    }
}