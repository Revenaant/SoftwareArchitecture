namespace View
{
    using Model;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShopMessageView : MonoBehaviour, IObserver<RedrawNotification>
    {
        private const int MAX_MESSAGES = 10;

        [SerializeField]
        private Transform messagePanelTransform = null;

        [SerializeField]
        private GameObject textPrefab = null;

        private IDisposable unsubscriber;

        public void SubscribeToObservable(IObservable<RedrawNotification> observable)
        {
            unsubscriber = observable?.Subscribe(this);
        }

        private void OnShopUpdated()
        {
            string[] messages = TradeLog.GetMessages();

            for (int i = 0; i < messages.Length; i++)
            {
                AddMessageToPanel(messages[i]);
                Debug.Log(messages[i]);
            }
        }

        private void AddMessageToPanel(string message)
        {
            if (messagePanelTransform.childCount > MAX_MESSAGES)
                Destroy(messagePanelTransform.GetChild(0).gameObject);

            GameObject newText = Instantiate(textPrefab, messagePanelTransform);
            Message newMessage = new Message(message, newText.GetComponent<Text>()); //TODO wtf was I doing
        }

        void IObserver<RedrawNotification>.OnNext(RedrawNotification notification)
        {
            OnShopUpdated();
            AddMessageToPanel(notification.message);
            Debug.Log(notification.message);
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

    public struct Message
    {
        public string message;
        public Text textObject;

        public Message(string message, Text textObject)
        {
            this.message = message;
            this.textObject = textObject;

            textObject.text = message;
        }
    }
}
