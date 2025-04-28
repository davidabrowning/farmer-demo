using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement Player;
    public TMP_Text BerryCountText;

    void Start()
    {

    }

    void Update()
    {
        BerryCountText.text = $"Twigs: {Player.TwigInventory} | Berries: {Player.BerryInventory}";
    }
}
