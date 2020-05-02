namespace View
{
    using System.Collections.Generic;
    using System.Drawing;
    using UnityEngine;
    using UnityEngine.UI;

    using Model;
    using Controller;
    using Model.Items;

    public class ShopView : MonoBehaviour
    {
        [SerializeField]
        private LayoutGroup itemLayoutGroup;

        [SerializeField]
        private GameObject itemPrefab;

        [SerializeField]
        private Button buyButton;

        [SerializeField]
        private Button sellButton;

        private ShopModel shopModel;
        private InventoryController inventoryController;

        // This method is used to initialize the view, as we can't use a constructor (monobehaviour)
        public void Initialize(ShopModel shopModel, InventoryController shopController)
        {
            this.shopModel = shopModel;
            this.inventoryController = shopController;

            PopulateItemIconView();
            InitializeButtons();
        }

        public void RegisterEvents(ITrader trader)
        {
            trader.OnItemSoldEvent += OnShopUpdated;
        }

        private void OnShopUpdated(ITradeable tradeable, ITrader trader)
        {
            RepopulateItemIconView();
        }

        // Clears the icon gridview and repopulates it with new icons (updates the visible icons)
        private void RepopulateItemIconView()
        {
            ClearIconView();
            PopulateItemIconView();
        }

        // Adds one icon for each item in the shop
        private void PopulateItemIconView()
        {
            foreach (Item item in shopModel.Inventory.GetItems())
                AddItemToView(item);
        }

        // Remove all existing icons in the gridview
        private void ClearIconView()
        {
            Transform[] allIcons = itemLayoutGroup.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in allIcons)
            {
                if (child != itemLayoutGroup.transform)
                    Destroy(child.gameObject);
            }
        }

        // Adds a new icon. An icon is a prefab Button with some additional scripts to link it to the store Item
        private void AddItemToView(Item item)
        {
            GameObject newItemIcon = GameObject.Instantiate(itemPrefab);
            newItemIcon.transform.SetParent(itemLayoutGroup.transform);

            // The scale would automatically change in Unity so we set it back to Vector3.one.
            newItemIcon.transform.localScale = Vector3.one;

            ItemContainer itemContainer = newItemIcon.GetComponent<ItemContainer>();
            Debug.Assert(itemContainer != null);
            bool isSelected = (item == shopModel.Inventory.GetSelectedItem());
            itemContainer.Initialize(item, isSelected);

            // Click behaviour for the button is done here. It seemed more convenient to do this inline than in the editor.
            Button itemButton = itemContainer.GetComponent<Button>();
            itemButton.onClick.AddListener(
                delegate
                {
                    inventoryController.SelectItem(item);

                    // TODO We need an Event system instead of this
                    RepopulateItemIconView();
                }
            );
        }

        // This method adds a listener to the 'Buy' and 'Sell' button. They are forwarded to the controller to the shop.
        private void InitializeButtons()
        {
            buyButton.onClick.AddListener(
                delegate
                {
                    inventoryController.Buy();

                    // TODO We need an Event system instead of this
                    RepopulateItemIconView();
                }
            );
            sellButton.onClick.AddListener(
                delegate
                {
                    inventoryController.Sell();

                    // TODO We need an Event system instead of this
                    RepopulateItemIconView();
                }
            );
        }
    }
}
