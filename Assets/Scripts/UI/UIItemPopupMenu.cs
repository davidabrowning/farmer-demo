using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace FarmerDemo
{
    public class UIItemPopupMenu : MonoBehaviour
    {
        public GameObject ButtonPrefab;
        public Transform ButtonContainer;
        public Button CancelButton;

        public void Setup(List<ObjectAction> actions, Vector3 screenPos)
        {
            transform.position = screenPos;

            // Clear old buttons
            foreach (Transform child in ButtonContainer)
                Destroy(child.gameObject);

            // Add a button for each action
            foreach (ObjectAction action in actions)
            {
                GameObject buttonObject = Instantiate(ButtonPrefab, ButtonContainer);
                Button button = buttonObject.GetComponent<Button>();
                TMP_Text buttonText = buttonObject.GetComponentInChildren<TMP_Text>();

                buttonText.text = action.ActionName;
                button.onClick.AddListener(() => action.Target.Interact(action.ActionId)); // Maybe add a CloseMenu call here
            }

            // Add Cancel button
            GameObject cancelButtonObject = Instantiate(ButtonPrefab, ButtonContainer);
            Button cancelButton = cancelButtonObject.GetComponent<Button>();
            TMP_Text cancelButtonText = cancelButtonObject.GetComponentInChildren<TMP_Text>();
            cancelButtonText.text = "Cancel";
            cancelButton.onClick.AddListener(CloseMenu);

            CancelButton.onClick.RemoveAllListeners();
            CancelButton.onClick.AddListener(CloseMenu);
        }

        public void CloseMenu()
        {
            Destroy(gameObject);
        }
    }
}