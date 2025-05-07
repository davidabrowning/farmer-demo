using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmerDemo
{
    public class InventoryMenuManagerScript : MonoBehaviourSingletonBase<InventoryMenuManagerScript>
    {
        public bool ShowInventoryRows = true;
        public GameObject TwigInventorySection;
        public TMP_Text TwigInventoryCountText;
        public GameObject BerryInventorySection;
        public TMP_Text BerryInventoryCountText;
        public GameObject StoneInventorySection;
        public TMP_Text StoneInventoryCountText;
        public GameObject IronInventorySection;
        public TMP_Text IronInventoryCountText;
        public GameObject CircuitInventorySection;
        public TMP_Text CircuitInventoryCountText;
        public GameObject FishInventorySection;
        public TMP_Text FishInventoryCountText;
        public GameObject SeedInventorySection;
        public TMP_Text SeedInventoryCountText;
        public TMP_Text InstructionsText;
        public GameObject Player;
        public List<GameObject> BuildList;
        public GameObject BuildListParentPanel;
        public GameObject BuildListButtonPrefab;
        public GameObject BuildListButtonTextPrefab;
        public GameObject BuildListResourceIconPrefab;
        public GameObject BuildListResourceTextPrefab;
        public Dictionary<string, GameObject> BuildOptionNameMapping = new();

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
            int stones = (int)Player.GetComponent<PlayerScript>().AmountInInventory(ResourceType.Stone);
            int irons = (int)Player.GetComponent<PlayerScript>().AmountInInventory(ResourceType.Iron);
            int fishes = Player.GetComponent<PlayerScript>().AmountInInventory(ResourceType.Fish);
            int seeds = Player.GetComponent<PlayerScript>().AmountInInventory(ResourceType.Seed);

            TwigInventoryCountText.text = "Twigs: " + twigs;
            BerryInventoryCountText.text = "Berries: " + berries;
            CircuitInventoryCountText.text = "Circuits: " + circuits;
            StoneInventoryCountText.text = "Stone: " + stones;
            IronInventoryCountText.text = "Iron: " + irons;
            FishInventoryCountText.text = "Fish: " + fishes;
            SeedInventoryCountText.text = "Seeds: " + seeds;

            TwigInventorySection.SetActive(twigs > 0 && ShowInventoryRows);
            BerryInventorySection.SetActive(berries > 0 && ShowInventoryRows);
            CircuitInventorySection.SetActive(circuits > 0 && ShowInventoryRows);
            StoneInventorySection.SetActive(stones > 0 && ShowInventoryRows);
            IronInventorySection.SetActive(irons > 0 && ShowInventoryRows);
            FishInventorySection.SetActive(fishes > 0 && ShowInventoryRows);
            SeedInventorySection.SetActive(seeds > 0 && ShowInventoryRows);

            switch (EraManagerScript.Instance.CurrentEra)
            {
                case EraType.Survival:
                    BuildOptionNameMapping.GetValueOrDefault("Fabricator").SetActive(true);
                    BuildOptionNameMapping.GetValueOrDefault("LabBuilding").SetActive(true);
                    break;
                case EraType.Power:
                    BuildOptionNameMapping.GetValueOrDefault("WoodBurner").SetActive(true);
                    BuildOptionNameMapping.GetValueOrDefault("CircuitMaker").SetActive(true);
                    break;
                case EraType.Automation:
                    BuildOptionNameMapping.GetValueOrDefault("AutoHarvester").SetActive(true);
                    BuildOptionNameMapping.GetValueOrDefault("SolarPanel").SetActive(true);
                    break;
                case EraType.ScientificAdvancement:
                    BuildOptionNameMapping.GetValueOrDefault("SeedSplicer").SetActive(true);
                    BuildOptionNameMapping.GetValueOrDefault("ARM").SetActive(true);
                    break;
            }
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

        public void ToggleShowInventoryRows()
        {
            ShowInventoryRows = !ShowInventoryRows;
        }

        private void InstantiateBuildList()
        {
            BuildList = new();
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/Fabricator"));
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/LabBuilding"));
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/WoodBurner"));
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/CircuitMaker"));
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/AutoHarvester"));
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/SolarPanel"));
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/SeedSplicer"));
            BuildList.Add(Resources.Load<GameObject>("Prefabs/World/ARM"));
            foreach (GameObject obj in BuildList)
            {           
                GameObject buildListButton = Instantiate(BuildListButtonPrefab);
                buildListButton.transform.SetParent(BuildListParentPanel.transform, false);
                buildListButton.transform.DetachChildren();
                Button buttonComponent = buildListButton.GetComponent<Button>();
                buttonComponent.onClick.AddListener(() => {
                    ConstructionManagerScript.Instance.ToggleBuildMode(obj.name);
                });

                BuildOptionNameMapping.Add(obj.name, buildListButton);
                buildListButton.SetActive(false);

                GameObject buildListButtonText = Instantiate(BuildListButtonTextPrefab);
                buildListButtonText.transform.SetParent(buildListButton.transform);
                buildListButtonText.GetComponent<TMP_Text>().text = obj.name;

                foreach (ResourceAmount resourceAmount in obj.GetComponent<IConstructable>().ConstructionCosts)
                {
                    GameObject buildListResourceIcon = Instantiate(BuildListResourceIconPrefab);
                    buildListResourceIcon.transform.SetParent(buildListButton.transform);
                    buildListResourceIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/ResourceIcons/" + resourceAmount.Type + "Icon");

                    GameObject buildListResourceText = Instantiate(BuildListResourceTextPrefab);
                    buildListResourceText.transform.SetParent(buildListButton.transform);
                    buildListResourceText.GetComponent<TMP_Text>().text = resourceAmount.Amount.ToString();
                }               
            }
        }
    }
}