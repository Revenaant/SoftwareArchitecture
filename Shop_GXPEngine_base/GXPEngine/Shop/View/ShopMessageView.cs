using System;
using System.Drawing;
using GXPEngine;
using Model;
using Model.Items;

namespace View
{
    // This class will draw a messagebox containing messages from the Shop that is observed.
    public class ShopMessageView : Canvas
    {
        const int FontHeight = 20;

        private ShopModel shop;

        public ShopMessageView(ShopModel shop) : base(800, 100)
        {
            this.shop = shop;

            DrawLogElements();
        }

        public void RegisterEvents(ITrader trader, ITrader other)
        {
            trader.OnItemSoldEvent += OnShopUpdated;
            other.OnItemSoldEvent += OnShopUpdated;
        }


        // TODO make this function an Interface for all Views
        private void OnShopUpdated(ITradeable tradeable, ITrader trader)
        {
            DrawLogElements();
        }

        private void DrawLogElements()
        {
            DrawBackground();
            DrawMessages();
        }

        private void DrawBackground()
        {
            // Draw background color
            graphics.Clear(Color.White);
            graphics.FillRectangle(Brushes.Gray, new Rectangle(0, 0, game.width, FontHeight));
        }

        //Draw messages onto this messagebox
        private void DrawMessages()
        {
            graphics.DrawString("Use ARROWKEYS to navigate. Press SPACE to buy, BKSPACE to sell.", SystemFonts.CaptionFont, Brushes.White, 0, 0);

            string[] messages = shop.GetMessages();
            for (int index = 0; index < messages.Length; index++)
            {
                String message = messages[index];
                graphics.DrawString(message, SystemFonts.CaptionFont, Brushes.Black, 0, FontHeight + index * FontHeight);
            }
        }
    }
}
