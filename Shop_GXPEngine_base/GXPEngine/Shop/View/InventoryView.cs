namespace View
{
    using System.Collections.Generic;
    using System.Drawing;
    using GXPEngine;
    using GXPEngine.Core;

    using Model;
    using Controller;
    using Model.Items;
    using System;

    // This Class draws the icons for the items in the store
    public class InventoryView : Canvas, IObserver<TradeNotification>
    {
        const int COLUMNS = 6;
        const int SPACING = 80;
        const int MARGIN = 18;

        private IDisposable unsubscriber;

        private InventoryController inventoryController;
        private GXPInputManager inputManager;
        private Color InventoryColor;

        // TODO The icon cache is built in here, that violates the S.R. principle.
        private Dictionary<string, Texture2D> iconCache;

        public InventoryView(InventoryController inventoryController, GXPInputManager inputManager, Color color) : base(498, 340)
        {
            this.inventoryController = inventoryController;
            this.inputManager = inputManager;
            this.InventoryColor = color;

            iconCache = new Dictionary<string, Texture2D>();

            x = (game.width - width) / 2;
            y = (game.height - height) / 4;

            DrawBackground();
            DrawItems();
        }

        public void SubscribeToObservable(IObservable<TradeNotification> observable)
        {
            unsubscriber = observable?.Subscribe(this);
        }

        public void SetViewScreenPosition(float xPercentage, float yPercentage)
        {
            x = (game.width - width) * xPercentage;
            y = (game.height - height) * yPercentage;
        }

        public void Step()
        {
            DrawBackground();
            DrawItems();
            HandleNavigation();
        }

        private void OnShopUpdated()
        {
            DrawBackground();
            DrawItems();
        }

        private void HandleNavigation()
        {
            if (Input.GetKeyDown(Key.LEFT))
                MoveSelection(-1, 0);
            if (Input.GetKeyDown(Key.RIGHT))
                MoveSelection(1, 0);
            if (Input.GetKeyDown(Key.UP))
                MoveSelection(0, -1);
            if (Input.GetKeyDown(Key.DOWN))
                MoveSelection(0, 1);
        }

        public void MoveSelection(int moveX, int moveY)
        {
            int itemIndex = inventoryController.GetSelectedItemIndex();
            int currentSelectionX = GetColumnByIndex(itemIndex);
            int currentSelectionY = GetRowByIndex(itemIndex);
            int requestedSelectionX = currentSelectionX + moveX;
            int requestedSelectionY = currentSelectionY + moveY;

            // Check horizontal boundaries
            if (requestedSelectionX >= 0 && requestedSelectionX < COLUMNS)
            {
                // Check vertical boundaries
                int newItemIndex = GetIndexFromGridPosition(requestedSelectionX, requestedSelectionY);
                if (newItemIndex >= 0 && newItemIndex <= inventoryController.GetItemCount())
                    inventoryController.SelectItemByIndex(newItemIndex);
            }
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

        private int GetIndexFromGridPosition(int column, int row)
        {
            return row * COLUMNS + column;
        }

        private void DrawBackground()
        {
            graphics.Clear(InventoryColor);
        }

        private void DrawItems()
        {
            for (int i = 0; i < inventoryController.Trader.Inventory.ItemCount; i++)
            {
                DrawableComponent drawable = inventoryController.GetItemByIndex(i).GetComponent<DrawableComponent>();
                if (drawable == null)
                    continue;

                int iconX = GetColumnByIndex(i) * SPACING + MARGIN;
                int iconY = GetRowByIndex(i) * SPACING + MARGIN;

                if (drawable == inventoryController.GetSelectedItem().GetComponent<DrawableComponent>())
                    DrawSelectedElement(drawable, iconX, iconY);
                else
                    DrawElement(drawable, iconX, iconY);
            }

            //List<DrawableComponent> items = inventoryController.Trader.Inventory.GetItems();
            //for (int index = 0; index < items.Count; index++)
            //{
            //    Item item = items[index];
            //    int iconX = GetColumnByIndex(index) * SPACING + MARGIN;
            //    int iconY = GetRowByIndex(index) * SPACING + MARGIN;

            //    if (item == inventoryController.Trader.Inventory.GetSelectedItem())
            //        DrawSelectedElement(item, iconX, iconY);
            //    else
            //        DrawElement(item, iconX, iconY);
            //}
        }

        private void DrawSelectedElement(DrawableComponent drawable, int iconX, int iconY)
        {
            if (Utils.Random(0, 2) == 0)
                DrawElement(drawable, iconX, iconY);
        }

        private void DrawElement(DrawableComponent drawable, int iconX, int iconY)
        {
            Texture2D iconTexture = GetCachedTexture(drawable.IconName);
            graphics.DrawImage(iconTexture.bitmap, iconX, iconY);
            graphics.DrawString(drawable.DisplayName, SystemFonts.CaptionFont, Brushes.Black, iconX + 8, iconY + 8);
            graphics.DrawString($"Buy: {drawable.DisplayCost.ToString()}", SystemFonts.CaptionFont, Brushes.Black, iconX + 8, iconY + 24);
            graphics.DrawString($"Sell: {drawable.DisplayCost.ToString()}", SystemFonts.CaptionFont, Brushes.White, iconX + 8, iconY + 40);
        }

        private Texture2D GetCachedTexture(string filename)
        {
            if (!iconCache.ContainsKey(filename))
                iconCache.Add(filename, new Texture2D("media/" + filename + ".png"));

            return iconCache[filename];
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
