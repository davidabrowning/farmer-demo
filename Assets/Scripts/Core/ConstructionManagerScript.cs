using UnityEngine;

namespace FarmerDemo
{
    public class ConstructionManagerScript : MonoBehaviourSingleton<ConstructionManagerScript>
    {
        public bool BuildModeOn = false;
        private void Update()
        {
            if (BuildModeOn)
            {
                Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int tilePos = new Vector2Int((int)Mathf.Round(mouseWorld.x), (int)Mathf.Round(mouseWorld.y));

                GridHighlighterScript.Instance.Highlight(tilePos);

                if (Input.GetMouseButtonDown(0)) // left-click
                {
                    LabBuilderScript.Instance.TryBuildItem(tilePos, tilePos, out GameObject labObj);
                }
            }
        }
        public void EnterBuildMode()
        {
            BuildModeOn = true;
        }
        public void ExitBuildMode()
        {
            BuildModeOn = false;
            GridHighlighterScript.Instance.Hide();
        }
        public void ToggleBuildMode()
        {
            if (BuildModeOn)
                ExitBuildMode();
            else
                EnterBuildMode();
        }
    }
}