using UnityEngine;

namespace FarmerDemo
{
    public class GridHighlighterScript : MonoBehaviourSingleton<GridHighlighterScript>
    {
        public GameObject HighlightPrefab;
        private GameObject currentHighlight;

        private void Start()
        {
            currentHighlight = Instantiate(HighlightPrefab);
            currentHighlight.SetActive(false);
        }

        public void Highlight(Vector2Int tile)
        {
            currentHighlight.SetActive(true);
            currentHighlight.transform.position = new Vector3Int(tile.x, tile.y, 1);
        }

        public void Hide()
        {
            currentHighlight.SetActive(false);
        }
    }
}
