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
        uIControllerScript.UpdateInstructions("I bet if I collect 5 twigs I can make a berry basket...");
    }
    public void UpdateUI()
    {
        if (Player.GetComponent<PlayerScript>().TwigInventory >= 5)
        {
            UIController.GetComponent<UIControllerScript>().UpdateInstructions("Great, now let's collect some berries...");
        }
    }
}
