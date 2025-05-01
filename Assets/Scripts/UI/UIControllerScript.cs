using TMPro;
using UnityEngine;

namespace FarmerDemo
{
    public class UIControllerScript : MonoBehaviourSingleton<UIControllerScript>
    {
        public GameObject TwigInventorySection;
        public TMP_Text TwigInventoryCountText;
        public GameObject BerryInventorySection;
        public TMP_Text BerryInventoryCountText;
        public TMP_Text InstructionsText;
        public GameObject Player;

        private void Update()
        {
            int twigs = (int)Player.GetComponent<PlayerScript>().TwigInventory;
            int berries = (int)Player.GetComponent<PlayerScript>().BerryInventory;

            TwigInventoryCountText.text = "Twigs: " + twigs;
            BerryInventoryCountText.text = "Berries: " + berries;

            TwigInventorySection.SetActive(twigs > 0);
            BerryInventorySection.SetActive(berries > 0);
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