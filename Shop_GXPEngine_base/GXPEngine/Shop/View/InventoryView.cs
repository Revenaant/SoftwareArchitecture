namespace View
{
    using System.Collections.Generic;
    using System.Drawing;
    using GXPEngine;
    using GXPEngine.Core;

    using Model;
    using Controller;
    using Model.Items;

    // This Class draws the icons for the items in the store
    public class InventoryView : Canvas
    {
        const int COLUMNS = 6;
        const int SPACING = 80;
        const int MARGIN = 18;

        private Inventory inventory;
        private ShopController shopController;
        private Color InventoryColor;

        // The icon cache is built in here, that violates the S.R. principle.
        private Dictionary<string, Texture2D> iconCache;

        public InventoryView(Inventory inventory, ShopController shopController, Color color) : base(498, 340)
        {
            this.inventory = inventory;
            this.shopController = shopController;
            this.InventoryColor = color;

            iconCache = new Dictionary<string, Texture2D>();

            x = (game.width - width) / 2;
            y = (game.height - height) / 4;

            DrawBackground();
            DrawItems();

            ShopModel.OnBuyEvent += OnShopUpdated;
            ShopModel.OnSellEvent += OnShopUpdated;
        }

        ~InventoryView()
        {
            ShopModel.OnBuyEvent -= OnShopUpdated;
            ShopModel.OnSellEvent -= OnShopUpdated;
        }
        public void SetViewScreenPosition(float xPercentage, float yPercentage)
        {
            x = (game.width - width) * xPercentage;
            y = (game.height - height) * yPercentage;
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
            int itemIndex = inventory.GetSelectedItemIndex();
            int currentSelectionX = GetColumnByIndex(itemIndex);
            int currentSelectionY = GetRowByIndex(itemIndex);
            int requestedSelectionX = currentSelectionX + moveX;
            int requestedSelectionY = currentSelectionY + moveY;

            // Check horizontal boundaries
            if (requestedSelectionX >= 0 && requestedSelectionX < COLUMNS)
            {
                // Check vertical boundaries
                int newItemIndex = GetIndexFromGridPosition(requestedSelectionX, requestedSelectionY);
                if (newItemIndex >= 0 && newItemIndex <= inventory.ItemCount)
                {
                    Item item = inventory.GetItemByIndex(newItemIndex);
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
            graphics.Clear(InventoryColor);
        }

        private void DrawItems()
        {
            List<Item> items = inventory.GetItems();
            for (int index = 0; index < items.Count; index++)
            {
                Item item = items[index];
                int iconX = GetColumnByIndex(index) * SPACING + MARGIN;
                int iconY = GetRowByIndex(index) * SPACING + MARGIN;

                if (item == inventory.GetSelectedItem())
                    DrawSelectedItem(item, iconX, iconY);
                else
                    DrawItem(item, iconX, iconY);
            }
        }

        private void DrawItem(ITradeable item, int iconX, int iconY)
        {
            Texture2D iconTexture = GetCachedTexture(item.IconName);
            graphics.DrawImage(iconTexture.bitmap, iconX, iconY);
            graphics.DrawString(item.Name, SystemFonts.CaptionFont, Brushes.Black, iconX + 8, iconY + 8);
            graphics.DrawString($"Buy: {item.Cost.ToString()}", SystemFonts.CaptionFont, Brushes.Black, iconX + 8, iconY + 24);
            graphics.DrawString($"Sell: {item.Cost.ToString()}", SystemFonts.CaptionFont, Brushes.White, iconX + 8, iconY + 40);
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
