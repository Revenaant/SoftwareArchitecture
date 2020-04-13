using System;
using System.Drawing;
using GXPEngine;
using Model;

namespace View
{
    //This class will draw a messagebox containing messages from the Shop that is observed.
    public class ShopMessageView : Canvas
    {
        const int FontHeight = 20;

        private ShopModel shop;

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopMessageDisplay()
        //------------------------------------------------------------------------------------------------------------------------
        public ShopMessageView(ShopModel shop) : base(800, 100)
        {
            this.shop = shop;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Step()
        //------------------------------------------------------------------------------------------------------------------------
        public void Step()
        {
            DrawBackground();
            DrawMessages();
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  DrawBackground()
        //------------------------------------------------------------------------------------------------------------------------
        //Draw background color
        private void DrawBackground()
        {
            graphics.Clear(Color.White);
            graphics.FillRectangle(Brushes.Gray, new Rectangle(0, 0, game.width, FontHeight));
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  DrawMessages()
        //------------------------------------------------------------------------------------------------------------------------
        //Draw messages onto this messagebox
        private void DrawMessages()
        {
            graphics.DrawString("Use ARROWKEYS to navigate. Press SPACE to buy, BKSPACE to sell.", SystemFonts.CaptionFont, Brushes.White, 0, 0);

            string[] messages = shop.GetMessages();
            for (int index = 0; index < messages.Length; index++) {
                String message = messages[index];
                graphics.DrawString(message, SystemFonts.CaptionFont, Brushes.Black, 0, FontHeight + index * FontHeight);
            }
        }

    }
}
