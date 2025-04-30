using Assets.Scripts.UI;
using Assets.Scripts.Core;
using Assets.Scripts.Player;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameManagerScript : MonoBehaviourSingleton<GameManagerScript>
    {
        public GameObject Player;
        public GameObject UIController;
        private void Start()
        {
            UIControllerScript.Instance.UpdateInstructions("Collect 5 twigs");
        }
    }
}
