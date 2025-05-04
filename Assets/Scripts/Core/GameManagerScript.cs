using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace FarmerDemo
{
    public class GameManagerScript : MonoBehaviourSingleton<GameManagerScript>
    {
        public GameObject Player;
        public GameObject UIController;
        public GameObject EraPanel;
        private void Start()
        {
            WorldBuilderScript.Instance.BuildInitialWorld();
            EraPanel.SetActive(false);
        }
        public void AdvanceEra()
        {
            EraPanel.SetActive(true);
        }
    }
}
