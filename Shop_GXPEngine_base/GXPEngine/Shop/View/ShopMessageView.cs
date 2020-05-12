namespace View
{
    using GXPEngine;
    using Model;
    using System;
    using System.Drawing;

    // This class will draw a messagebox containing messages from the TradeLog
    public class ShopMessageView : Canvas, IObserver<RedrawNotification>
    {
        private const int FONT_HEIGHT = 20;
        private IDisposable unsubscriber;

        public ShopMessageView() : base(800, 100)
        {
            DrawLogElements();
        }

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
            graphics.Clear(Color.White);
            graphics.FillRectangle(Brushes.Gray, new Rectangle(0, 0, game.width, FONT_HEIGHT));
        }

        // Draw messages onto this messagebox
        private void DrawMessages()
        {
            graphics.DrawString("Use ARROWKEYS : Navigate, SPACE : Buy, TAB : Switch inventory, C : Clear, R : Restock, S : Sort, E : Consume", SystemFonts.CaptionFont, Brushes.White, 0, 0);

            string[] messages = TradeLog.GetMessages();
            for (int index = 0; index < messages.Length; index++)
            {
                String message = messages[index];
                graphics.DrawString(message, SystemFonts.CaptionFont, Brushes.Black, 0, FONT_HEIGHT + index * FONT_HEIGHT);
            }
        }

        public void SubscribeToObservable(IObservable<RedrawNotification> observable)
        {
            unsubscriber = observable?.Subscribe(this);
        }

        void IObserver<RedrawNotification>.OnNext(RedrawNotification notification)
        {
            TradeLog.AddMessage(notification.message);
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
