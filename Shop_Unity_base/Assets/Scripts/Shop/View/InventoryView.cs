namespace View
{
    using UnityEngine;
    using System;
    using Model;
    using Controller;
    using UnityEngine.UI;

    public class InventoryView : MonoBehaviour, IObserver<TradeNotification>
    {
        [SerializeField]
        private LayoutGroup itemLayoutGroup;

        [SerializeField]
        private GameObject itemPrefab;

        [SerializeField]
        private Text inventoryName;

        [Header("Buttons")]

        [SerializeField]
        private Button buyButton;

        [SerializeField]
        private Button consumeButton;

        [SerializeField]
        private Button SortButton;

        [SerializeField]
        private Button RestockButton;

        [SerializeField]
        private Button ClearButton;

        private InventoryController inventoryController;
        private UnityInputManager inputManager;
        private IDisposable unsubscriber;

        public void Initialize(InventoryController inventoryController, UnityInputManager inputManager)
        {
            this.inventoryController = inventoryController;
            this.inputManager = inputManager;


            inventoryName.text = $"{inventoryController.Trader.Name}'s Inventory";
            PopulateItemIconView();
            InitializeButtons();
        }

        public void SubscribeToObservable(IObservable<TradeNotification> observable)
        {
            unsubscriber = observable?.Subscribe(this);
        }

        private void OnShopUpdated()
        {
            RepopulateItemIconView();
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
                    inventoryController.Sell();

                    // TODO We need an Event system instead of this
                    RepopulateItemIconView();
                }
            );
        }

        void IObserver<TradeNotification>.OnNext(TradeNotification value)
        {
            OnShopUpdated();
        }

        void IObserver<TradeNotification>.OnCompleted()
        {
            unsubscriber.Dispose();
        }

        void IObserver<TradeNotification>.OnError(Exception error)
        {
            throw new NotSupportedException("There was an error sending data, this method should never be called");
        }
    }
}
