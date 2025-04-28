using Assets.Scripts;
using TMPro;
using UnityEngine;

public class UIControllerScript : MonoBehaviour
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
}
