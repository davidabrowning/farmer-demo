using System.Collections;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class TreeScript : ItemInteractable
    {
        public const int MaxTwigDelay = 100;
        protected override void Start()
        {
            base.Start();
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
            for (int i = 0; i < 5; i++)
                ItemBuilderScript.Instance.TryBuildItem(BottomLeft - Vector2Int.one, TopRight + Vector2Int.one, "Twig", out GameObject builtTwig);
        }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "shake_tree", "Shake down some twigs"));
            Actions.Add(new ObjectAction(this, "cut_tree", "Cut down tree"));
        }

        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "shake_tree":
                    gameObject.GetComponent<TreeScript>().DropMaxTwigs();
                    break;
                case "cut_tree":
                    PlayerScript.Instance.AddToInventory(ResourceType.Twig, 500);
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }
    }
}