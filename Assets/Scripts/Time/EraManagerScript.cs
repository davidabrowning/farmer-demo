using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmerDemo
{
    public class EraManagerScript : MonoBehaviourSingleton<EraManagerScript>
    {
        public GameObject EraPanel;
        public Image EraImage;
        public TMP_Text EraText;
        public EraType CurrentEra = EraType.Survival;
        public event Action EraUpdate; // Holds listeners to era updates

        private void Start()
        {
            EraPanel.SetActive(false);
        }
        public void AdvanceEra()
        {
            CurrentEra++;
            EraUpdate.Invoke(); // Notify all listeners that era has updated
            UpdateEraPanelContents();
            StartCoroutine(ShowAdvanceEraPanel());
        }
        private void UpdateEraPanelContents()
        {
            switch (CurrentEra)
            {
                case EraType.Power:
                    EraText.text = "Research complete. Great job! Now beginning the Power phase.";
                    break;
                case EraType.Automation:
                    EraText.text = "Research complete. Great job! Now beginning the Automation phase.";
                    break;
                case EraType.ScientificAdvancement:
                    EraText.text = "Research complete. Great job! Now beginning the Scientific Advancement phase.";
                    break;
            }
        }
        private IEnumerator ShowAdvanceEraPanel()
        {
            EraPanel.SetActive(true);
            yield return new WaitForSeconds(3);
            EraPanel.SetActive(false);
        }
    }
}