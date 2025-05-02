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
        public GameObject CircuitInventorySection;
        public TMP_Text CircuitInventoryCountText;
        public TMP_Text InstructionsText;
        public GameObject Player;

        private void Update()
        {
            int twigs = (int)Player.GetComponent<PlayerScript>().TwigInventory;
            int berries = (int)Player.GetComponent<PlayerScript>().BerryInventory;
            int circuits = Player.GetComponent<PlayerScript>().CircuitInventory;

            TwigInventoryCountText.text = "Twigs: " + twigs;
            BerryInventoryCountText.text = "Berries: " + berries;
            CircuitInventoryCountText.text = "Circuits: " + circuits;

            TwigInventorySection.SetActive(twigs > 0);
            BerryInventorySection.SetActive(berries > 0);
            CircuitInventorySection.SetActive(circuits > 0);
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