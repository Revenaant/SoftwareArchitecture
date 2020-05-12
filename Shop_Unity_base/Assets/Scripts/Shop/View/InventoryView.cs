namespace View
{
    using Controller;
    using Model;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class InventoryView : MonoBehaviour, IObserver<RedrawNotification>
    {
        [Header("Items")]

        [SerializeField]
        private LayoutGroup itemLayoutGroup = null;

        [SerializeField]
        private GameObject itemPrefab = null;

        [Header("Text")]

        [SerializeField]
        private Text inventoryNameText = null;

        [SerializeField]
        private Text goldText = null;

        [Header("Buttons")]

        [SerializeField]
        private Button buyButton = null;

        [SerializeField]
        private Button consumeButton = null;

        [SerializeField]
        private Button sortButton = null;

        [SerializeField]
        private Button restockButton = null;

        [SerializeField]
        private Button clearButton = null;

        private InventoryController inventoryController;
        private UnityInputManager inputManager;
        private IDisposable RedrawUnsubscriber;

        public void Initialize(InventoryController inventoryController, UnityInputManager inputManager)
        {
            this.inventoryController = inventoryController;
            this.inputManager = inputManager;

            inventoryNameText.text = $"{inventoryController.Trader.Name}'s Inventory";
            goldText.text = $"Gold: {inventoryController.Trader.Gold}";

            PopulateItemIconView();
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            inputManager.BindCommandToButton(buyButton, new SellCommand(), inventoryController);
            inputManager.BindCommandToButton(consumeButton, new ConsumeCommand(), inventoryController);
            inputManager.BindCommandToButton(restockButton, new RestockCommand(), inventoryController);
            inputManager.BindCommandToButton(sortButton, new SortCommand(), inventoryController);
            inputManager.BindCommandToButton(clearButton, new ClearCommand(), inventoryController);
        }

        private void OnShopUpdated()
        {
            RepopulateItemIconView();
            goldText.text = $"Gold: {inventoryController.Trader.Gold}";
        }

        // Clears the icon gridview and repopulates it with new icons (updates the visible icons)
        private void RepopulateItemIconView()
        {
            ClearIconView();
            PopulateItemIconView();
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

        // Adds one icon for each item in the shop
        private void PopulateItemIconView()
        {
            foreach (Item item in inventoryController.GetItems())
                AddItemToView(item);
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
            bool isSelected = (item == inventoryController.GetSelectedItem());
            itemContainer.Initialize(item, isSelected);

            // Click behaviour for the button is done here. It seemed more convenient to do this inline than in the editor.
            Button itemButton = itemContainer.GetComponent<Button>();
            itemButton.onClick.AddListener(
                delegate
                {
                    inventoryController.SelectItem(item);
                    RepopulateItemIconView();
                }
            );
        }

        public void SubscribeToObservable(IObservable<RedrawNotification> observable)
        {
            RedrawUnsubscriber = observable?.Subscribe(this);
        }

        void IObserver<RedrawNotification>.OnNext(RedrawNotification value)
        {
            OnShopUpdated();
        }

        void IObserver<RedrawNotification>.OnCompleted()
        {
            RedrawUnsubscriber.Dispose();
        }

        void IObserver<RedrawNotification>.OnError(Exception error)
        {
            throw new NotSupportedException("There was an error sending data, this method should never be called");
        }
    }
}
