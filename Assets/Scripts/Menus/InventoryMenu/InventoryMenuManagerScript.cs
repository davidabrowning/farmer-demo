using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmerDemo
{
    public class InventoryMenuManagerScript : MonoBehaviourSingleton<InventoryMenuManagerScript>
    {
        public GameObject TwigInventorySection;
        public TMP_Text TwigInventoryCountText;
        public GameObject BerryInventorySection;
        public TMP_Text BerryInventoryCountText;
        public GameObject CircuitInventorySection;
        public TMP_Text CircuitInventoryCountText;
        public TMP_Text InstructionsText;
        public GameObject Player;
        public List<GameObject> BuildList;
        public GameObject BuildListParentPanel;
        public GameObject BuildListButtonPrefab;
        public GameObject BuildListButtonTextPrefab;
        public GameObject BuildListResourceIconPrefab;
        public GameObject BuildListResourceTextPrefab;

        protected override void Awake()
        {
            base.Awake();
            InstantiateBuildList();
        }

        private void Update()
        {
            int twigs = (int)Player.GetComponent<PlayerScript>().AmountInInventory(ResourceType.Twig);
            int berries = (int)Player.GetComponent<PlayerScript>().AmountInInventory(ResourceType.Berry);
            int circuits = Player.GetComponent<PlayerScript>().AmountInInventory(ResourceType.Circuit);

            TwigInventoryCountText.text = "Twigs: " + twigs;
            BerryInventoryCountText.text = "Berries: " + berries;
            CircuitInventoryCountText.text = "Circuits: " + circuits;

            TwigInventorySection.SetActive(twigs > 0);
            BerryInventorySection.SetActive(berries > 0);
            CircuitInventorySection.SetActive(circuits > 0);
        }
        public void UpdateInstructions(string instructionsText)
        {
            InstructionsText.text = instructionsText;
        }
        public void ToggleMenuSection(GameObject menuSection)
        {
            if (menuSection.activeSelf)
                menuSection.SetActive(false);
            else
                menuSection.SetActive(true);
        }

        private void InstantiateBuildList()
        {
            BuildList = new();
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/Fabricator"));
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/LabBuilding"));
            //BuildList.Add(Resources.Load<GameObject>("Prefabs/World/WoodBurner"));
            //BuildList.Add(Resources.Load<GameObject>("Prefabs/World/CircuitMaker"));
            //BuildList.Add(Resources.Load<GameObject>("Prefabs/World/AutoHarvester"));
            //BuildList.Add(Resources.Load<GameObject>("Prefabs/World/HydroPlant"));
            //BuildList.Add(Resources.Load<GameObject>("Prefabs/World/SeedSplicer"));
            //BuildList.Add(Resources.Load<GameObject>("Prefabs/World/ARM"));
            foreach (GameObject obj in BuildList)
            {           
                GameObject buildListButton = Instantiate(BuildListButtonPrefab);
                buildListButton.transform.SetParent(BuildListParentPanel.transform, false);
                buildListButton.transform.DetachChildren();
                Button buttonComponent = buildListButton.GetComponent<Button>();
                buttonComponent.onClick.AddListener(() => {
                    ConstructionManagerScript.Instance.ToggleBuildMode(obj.name);
                });


                GameObject buildListButtonText = Instantiate(BuildListButtonTextPrefab);
                buildListButtonText.transform.SetParent(buildListButton.transform);
                buildListButtonText.GetComponent<TMP_Text>().text = obj.name;

                foreach (ResourceAmount resourceAmount in obj.GetComponent<IConstructable>().ConstructionCosts)
                {
                    GameObject buildListResourceIcon = Instantiate(BuildListResourceIconPrefab);
                    buildListResourceIcon.transform.SetParent(buildListButton.transform);
                    buildListResourceIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/ResourceIcons/" + resourceAmount.Type);

                    GameObject buildListResourceText = Instantiate(BuildListResourceTextPrefab);
                    buildListResourceText.transform.SetParent(buildListButton.transform);
                    buildListResourceText.GetComponent<TMP_Text>().text = resourceAmount.Amount.ToString();
                }               
            }
        }
    }
}