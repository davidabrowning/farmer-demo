using UnityEngine;

namespace FarmerDemo
{
    public class UIItemClicker : MonoBehaviour
    {
        public GameObject MenuPrefab;
        private GameObject _currentMenu;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && _currentMenu == null)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider != null)
                {
                    ItemInteractable interactable = hit.collider.GetComponent<ItemInteractable>();
                    if (interactable != null)
                    {
                        _currentMenu = Instantiate(MenuPrefab, GameObject.Find("UICanvas").transform);
                        UIItemPopupMenu popup = _currentMenu.GetComponent<UIItemPopupMenu>();
                        popup.Setup(interactable.Actions, Input.mousePosition);
                    }
                }
            }
        }
    }
}