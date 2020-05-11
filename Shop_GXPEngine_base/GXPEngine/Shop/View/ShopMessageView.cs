namespace View
{
    using System;
    using System.Drawing;
    using GXPEngine;
    using Model;
    using Model.Items;

    // This class will draw a messagebox containing messages from the Shop that is observed.
    public class ShopMessageView : Canvas, IObserver<TradeNotification>
    {
        private const int FONT_HEIGHT = 20;
        private IDisposable unsubscriber;

        public ShopMessageView() : base(800, 100)
        {
            DrawLogElements();
        }

        public void SubscribeToObservable(IObservable<TradeNotification> observable)
        {
            unsubscriber = observable?.Subscribe(this);
        }

        // TODO make this function an Interface for all Views
        private void OnShopUpdated()
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
            graphics.FillRectangle(Brushes.Gray, new Rectangle(0, 0, game.width, FONT_HEIGHT));
        }

        //Draw messages onto this messagebox
        private void DrawMessages()
        {
            graphics.DrawString("Use ARROWKEYS to navigate. Press SPACE to buy, BKSPACE to sell.", SystemFonts.CaptionFont, Brushes.White, 0, 0);

            string[] messages = TradeLog.GetMessages();
            for (int index = 0; index < messages.Length; index++)
            {
                String message = messages[index];
                graphics.DrawString(message, SystemFonts.CaptionFont, Brushes.Black, 0, FONT_HEIGHT + index * FONT_HEIGHT);
            }
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
