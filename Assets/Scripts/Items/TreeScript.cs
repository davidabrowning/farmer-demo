using System.Collections;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class TreeScript : ItemBase
    {
        public const int TwigDelay = 10;
        public void Start()
        {
            StartCoroutine(DropTwigs());
        }

        private IEnumerator DropTwigs()
        {
            while(true)
            {
                yield return new WaitForSeconds(TwigDelay);
                TwigBuilderScript.Instance.TryBuildItem(BottomLeft - Vector2Int.one, TopRight + Vector2Int.one, out GameObject builtTwig);
            }
        }
    }
}