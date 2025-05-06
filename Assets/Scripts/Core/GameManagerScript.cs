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
        public int CurrentEra = 0;
        private void Start()
        {
            WorldBuilderScript.Instance.BuildInitialWorld();
            EraPanel.SetActive(false);
        }
        public void AdvanceEra()
        {
            StartCoroutine(ShowAdvanceEraPanel());
            CurrentEra++;
        }

        private IEnumerator ShowAdvanceEraPanel()
        {

            EraPanel.SetActive(true);
            yield return new WaitForSeconds(3);
            EraPanel.SetActive(false);
        }
    }
}
