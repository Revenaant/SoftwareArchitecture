namespace View
{
    using Controller;
    using GXPEngine;
    using GXPEngine.Core;
    using Model;
    using System;
    using System.Drawing;

    // This Class draws the icons for the items in the store
    public class InventoryView : Canvas, IObserver<RedrawNotification>
    {
        const int COLUMNS = 6;
        const int SPACING = 100;
        const int MARGIN = 18;

        private IDisposable unsubscriber;

        private InventoryController inventoryController;
        private GXPInputManager inputManager;
        private TextureCache TextureCache;
        private Color InventoryColor;

        public InventoryView(InventoryController inventoryController, GXPInputManager inputManager, Color color) : base(640, 340)
        {
            this.inventoryController = inventoryController;
            this.inputManager = inputManager;
            this.InventoryColor = color;

            TextureCache = new TextureCache();

            x = (game.width - width) / 2;
            y = (game.height - height) / 4;

            DrawBackground();
            DrawItems();
            DrawInfo();
        }

        public void SetViewScreenPosition(float xPercentage, float yPercentage)
        {
            x = (game.width - width) * xPercentage;
            y = (game.height - height) * yPercentage;
        }

        public void Step()
        {
            HandleNavigation();

            // Left here to keep the blinking item functionality
            DrawBackground();
            DrawItems();
            DrawInfo();
        }

        private void OnShopUpdated()
        {
            DrawBackground();
            DrawItems();
            DrawInfo();
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
        }

        private void DrawSelectedElement(DrawableComponent drawable, int iconX, int iconY)
        {
            if (Utils.Random(0, 2) == 0)
                DrawElement(drawable, iconX, iconY);
        }

        private void DrawElement(DrawableComponent drawable, int iconX, int iconY)
        {
            Texture2D iconTexture = TextureCache.GetCachedTexture(drawable.IconName);
            graphics.DrawImage(iconTexture.bitmap, iconX, iconY);
            graphics.DrawString(drawable.DisplayName, SystemFonts.CaptionFont, Brushes.Black, iconX + 8, iconY + 8);
            graphics.DrawString($"Buy: {drawable.DisplayCost.ToString()}", SystemFonts.CaptionFont, Brushes.Black, iconX + 8, iconY + 24);
            graphics.DrawString($"{drawable.DisplayQuantity.ToString()}x", SystemFonts.CaptionFont, Brushes.SlateGray, iconX + 8, iconY + 64);
        }

        private void DrawInfo()
        {
            graphics.DrawString($"{inventoryController.Trader.Name}'s Inventory", SystemFonts.CaptionFont, Brushes.Black, 20, height - 20);
            graphics.DrawString($"Gold: {inventoryController.Trader.Gold}", SystemFonts.CaptionFont, Brushes.Black, width * 0.8f, height - 20);
        }

        public void SubscribeToObservable(IObservable<RedrawNotification> observable)
        {
            unsubscriber = observable?.Subscribe(this);
        }

        void IObserver<RedrawNotification>.OnNext(RedrawNotification value)
        {
            OnShopUpdated();
        }

        void IObserver<RedrawNotification>.OnCompleted()
        {
            unsubscriber.Dispose();
        }

        void IObserver<RedrawNotification>.OnError(Exception error)
        {
            throw new NotSupportedException("There was an error sending data, this method should never be called");
        }
    }
}
