using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject UIController;
    private void Start()
    {
        UIControllerScript uIControllerScript = UIController.GetComponent<UIControllerScript>();
        uIControllerScript.UpdateInstructions("Collect 5 twigs");
    }
    public void UpdateUI()
    {
        if (Player.GetComponent<PlayerScript>().TwigInventory >= 5)
        {
            UIController.GetComponent<UIControllerScript>().UpdateInstructions("Collect 20 berries");
        }
    }
}
