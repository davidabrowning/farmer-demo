using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmerDemo
{
    public class EraManagerScript : MonoBehaviourSingletonBase<EraManagerScript>
    {
        public GameObject EraPanel;
        public Image EraImage;
        public TMP_Text EraText;
        public TMP_Text StartNextEraButtonText;
        public EraType CurrentEra = EraType.Survival;
        public event Action EraUpdate; // Holds listeners to era updates

        private void Start()
        {
            EraPanel.SetActive(false);
        }
        public void AdvanceEra()
        {
            UpdateEraPanelContents();
            ShowAdvanceEraPanel();
            CurrentEra++;
            EraUpdate.Invoke(); // Notify all listeners that era has updated
        }
        private void UpdateEraPanelContents()
        {
            switch (CurrentEra)
            {
                case EraType.Survival:
                    EraImage.sprite = Resources.Load<Sprite>("Art/SplashImages/Berries");
                    EraText.text = "Research complete!";
                    StartNextEraButtonText.text = "Begin power phase";
                    break;
                case EraType.Power:
                    EraImage.sprite = Resources.Load<Sprite>("Art/SplashImages/WoodFire");
                    EraText.text = "Research complete!";
                    StartNextEraButtonText.text = "Begin automation phase";
                    break;
                case EraType.Automation:
                    EraImage.sprite = Resources.Load<Sprite>("Art/SplashImages/RobotField");
                    EraText.text = "Research complete!";
                    StartNextEraButtonText.text = "Begin disease research phase";
                    break;
                case EraType.ScientificAdvancement:
                    EraImage.sprite = Resources.Load<Sprite>("Art/SplashImages/PlantBuilding");
                    EraText.text = "Cure research complete. You win the game!";
                    StartNextEraButtonText.text = "Close";
                    break;
            }
        }
        public void ShowAdvanceEraPanel()
        {
            EraPanel.SetActive(true);
        }

        public void HideAdvanceEraPanel()
        {
            EraPanel.SetActive(false);
        }
    }
}