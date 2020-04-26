namespace View
{
    using System.Collections.Generic;
    using System.Drawing;
    using UnityEngine;
    using UnityEngine.UI;

    using Model;
    using Controller;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ShopController()
    //------------------------------------------------------------------------------------------------------------------------        
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
        private ShopController shopController;

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Initialize()
        //------------------------------------------------------------------------------------------------------------------------        
        //this method is used to initialize the view, as we can't use a constructor (monobehaviour)
        public void Initialize(ShopModel shopModel, ShopController shopController)
        {
            this.shopModel = shopModel;
            this.shopController = new ShopController(shopModel);

            ShopModel.OnBuyEvent += OnShopUpdated;
            ShopModel.OnSellEvent += OnShopUpdated;

            PopulateItemIconView();
            InitializeButtons();
        }

        private void OnShopUpdated(Item item, Customer customer)
        {
            RepopulateItemIconView();
        }

        private void OnDestroy()
        {
            ShopModel.OnBuyEvent -= OnShopUpdated;
            ShopModel.OnSellEvent -= OnShopUpdated;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  RepopulateItems()
        //------------------------------------------------------------------------------------------------------------------------        
        //clears the icon gridview and repopulates it with new icons (updates the visible icons)
        private void RepopulateItemIconView()
        {
            ClearIconView();
            PopulateItemIconView();
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  PopulateItems()
        //------------------------------------------------------------------------------------------------------------------------        
        //adds one icon for each item in the shop
        private void PopulateItemIconView()
        {
            foreach (Item item in shopModel.GetItems())
            {
                AddItemToView(item);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ClearIconView()
        //------------------------------------------------------------------------------------------------------------------------        
        //remove all existing icons in the gridview
        private void ClearIconView()
        {
            Transform[] allIcons = itemLayoutGroup.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in allIcons)
            {
                if (child != itemLayoutGroup.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  AddItemToView()
        //------------------------------------------------------------------------------------------------------------------------        
        //Adds a new icon. An icon is a prefab Button with some additional scripts to link it to the store Item
        private void AddItemToView(Item item)
        {
            GameObject newItemIcon = GameObject.Instantiate(itemPrefab);
            newItemIcon.transform.SetParent(itemLayoutGroup.transform);
            newItemIcon.transform.localScale = Vector3.one;//The scale would automatically change in Unity so we set it back to Vector3.one.

            ItemContainer itemContainer = newItemIcon.GetComponent<ItemContainer>();
            Debug.Assert(itemContainer != null);
            bool isSelected = (item == shopModel.GetSelectedItem());
            itemContainer.Initialize(item, isSelected);

            //Click behaviour for the button is done here. It seemed more convenient to do this inline than in the editor.
            Button itemButton = itemContainer.GetComponent<Button>();
            itemButton.onClick.AddListener(
                delegate
                {
                    shopController.SelectItem(item);
                    RepopulateItemIconView(); //we need an Event system instead of this
                }
            );
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  InitializeButtons()
        //------------------------------------------------------------------------------------------------------------------------        
        //This method adds a listener to the 'Buy' and 'Sell' button. They are forwarded to the controller to the shop.
        private void InitializeButtons()
        {
            buyButton.onClick.AddListener(
                delegate
                {
                    shopController.Buy();
                    RepopulateItemIconView(); //we need an Event system instead of this
                }
            );
            sellButton.onClick.AddListener(
                delegate
                {
                    shopController.Sell();
                    RepopulateItemIconView(); //we need an Event system instead of this
                }
            );
        }
    }
}