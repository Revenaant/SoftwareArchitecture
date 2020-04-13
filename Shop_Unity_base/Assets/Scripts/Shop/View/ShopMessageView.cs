namespace View
{
    using UnityEngine;
    using Model;

    public class ShopMessageView : MonoBehaviour
    {
        private ShopModel shopModel;

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Initialize()
        //------------------------------------------------------------------------------------------------------------------------
        //This method is used to initialize values, because we can't use a constructor.
        public void Initialize(ShopModel shopModel)
        {
            this.shopModel = shopModel;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Update()
        //------------------------------------------------------------------------------------------------------------------------        
        //this method polls the shop for messages and prints them. Since the shop caches the messages, it prints the same
        //message each frame. An event system would work better.
        protected void Update() {
            string[] messages = shopModel.GetMessages();
            if (messages.Length > 0) {
                string message = messages[messages.Length - 1];
                Debug.Log(message);
            }
        }
    }
}
