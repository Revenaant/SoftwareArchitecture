namespace View
{
    using System.Collections.Generic;
    using System.Drawing;
    using GXPEngine;
    using GXPEngine.Core;

    using Model;
    using Controller;

    // This Class draws the icons for the items in the store
    public class ShopView : Canvas
    {
        const int COLUMNS = 4;
        const int SPACING = 80;
        const int MARGIN = 18;

        private ShopModel shopModel;
        private ShopController shopController;

        // The icon cache is built in here, that violates the S.R. principle.
        private Dictionary<string, Texture2D> iconCache;

        public ShopView(ShopModel shop, ShopController shopController) : base(340, 340)
        {
            this.shopModel = shop;
            this.shopController = shopController;

            iconCache = new Dictionary<string, Texture2D>();

            x = (game.width - width) / 2;
            y = (game.height - height) / 2;

            ShopModel.OnBuyEvent += OnShopUpdated;
            ShopModel.OnSellEvent += OnShopUpdated;
        }

        ~ShopView()
        {
            ShopModel.OnBuyEvent -= OnShopUpdated;
            ShopModel.OnSellEvent -= OnShopUpdated;
        }

        private void OnShopUpdated(Item item, Customer customer)
        {
            DrawBackground();
            DrawItems();
        }

        public void Step()
        {
            DrawBackground();
            DrawItems();
            HandleNavigation();
        }

        private void HandleNavigation()
        {
            if (Input.GetKeyDown(Key.LEFT))
            {
                MoveSelection(-1, 0);
            }
            if (Input.GetKeyDown(Key.RIGHT))
            {
                MoveSelection(1, 0);
            }
            if (Input.GetKeyDown(Key.UP))
            {
                MoveSelection(0, -1);
            }
            if (Input.GetKeyDown(Key.DOWN))
            {
                MoveSelection(0, 1);
            }

            if (Input.GetKeyDown(Key.SPACE))
            {
                shopController.Buy();
            }
            if (Input.GetKeyDown(Key.BACKSPACE))
            {
                shopController.Sell();
            }
        }

        private void MoveSelection(int moveX, int moveY)
        {
            int itemIndex = shopModel.GetSelectedItemIndex();
            int currentSelectionX = GetColumnByIndex(itemIndex);
            int currentSelectionY = GetRowByIndex(itemIndex);
            int requestedSelectionX = currentSelectionX + moveX;
            int requestedSelectionY = currentSelectionY + moveY;

            // Check horizontal boundaries
            if (requestedSelectionX >= 0 && requestedSelectionX < COLUMNS)
            {
                // Check vertical boundaries
                int newItemIndex = GetIndexFromGridPosition(requestedSelectionX, requestedSelectionY);
                if (newItemIndex >= 0 && newItemIndex <= shopModel.GetItemCount())
                {
                    Item item = shopModel.GetItemByIndex(newItemIndex);
                    shopController.SelectItem(item);
                }
            }
        }

        private int GetIndexFromGridPosition(int column, int row)
        {
            return row * COLUMNS + column;
        }

        private int GetColumnByIndex(int index)
        {
            return index % COLUMNS;
        }

        private int GetRowByIndex(int index)
        {
            // Rounds down
            return index / COLUMNS;
        }

        private void DrawBackground()
        {
            graphics.Clear(Color.White);
        }

        private void DrawItems()
        {
            List<Item> items = shopModel.GetItems();
            for (int index = 0; index < items.Count; index++)
            {
                Item item = items[index];
                int iconX = GetColumnByIndex(index) * SPACING + MARGIN;
                int iconY = GetRowByIndex(index) * SPACING + MARGIN;

                if (item == shopModel.GetSelectedItem())
                    DrawSelectedItem(item, iconX, iconY);
                else
                    DrawItem(item, iconX, iconY);
            }
        }

        private void DrawItem(Item item, int iconX, int iconY)
        {
            Texture2D iconTexture = GetCachedTexture(item.iconName);
            graphics.DrawImage(iconTexture.bitmap, iconX, iconY);
            graphics.DrawString(item.name, SystemFonts.CaptionFont, Brushes.Black, iconX + 16, iconY + 16);
            graphics.DrawString(item.Cost.ToString(), SystemFonts.CaptionFont, Brushes.Black, iconX + 16, iconY + 32);
        }

        private void DrawSelectedItem(Item item, int iconX, int iconY)
        {
            if (Utils.Random(0, 2) == 0)
                DrawItem(item, iconX, iconY);
        }

        private Texture2D GetCachedTexture(string filename)
        {
            if (!iconCache.ContainsKey(filename))
                iconCache.Add(filename, new Texture2D("media/" + filename + ".png"));

            return iconCache[filename];
        }
    }
}
