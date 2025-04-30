using Assets.Scripts;
using Assets.Scripts.Core;
using Assets.Scripts.Grid;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIControllerScript : MonoBehaviourSingleton<UIControllerScript>
    {
        public TMP_Text TwigInventoryCountText;
        public TMP_Text BerryInventoryCountText;
        public TMP_Text InstructionsText;
        public GameObject Player;

        private void Update()
        {
            TwigInventoryCountText.text = Player.GetComponent<PlayerScript>().TwigInventory.ToString();
            BerryInventoryCountText.text = Player.GetComponent<PlayerScript>().BerryInventory.ToString();
        }
        public void UpdateInstructions(string instructionsText)
        {
            InstructionsText.text = instructionsText;
        }
        public void ToggleMenuSection(GameObject menuSection)
        {
            if (menuSection.activeSelf)
                menuSection.SetActive(false);
            else
                menuSection.SetActive(true);
        }
    }
}