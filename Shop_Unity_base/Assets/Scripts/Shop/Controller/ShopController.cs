namespace Controller
{
    using System;
    using Model;

    // This class provides a controller for a ShopModel. The Controller acts as a public interface for a ShopModel.
    // These methods are being called by ShopView, as it implements the user interface. The exception is Initialize(),
    // it is being called by ShopState. We use Initialize() as a replacement for the constructor, as this class is a MonoBehaviour.
    public class ShopController
    {
        private ShopModel shopModel;

        // Ties this controller to a model
        public ShopController(ShopModel shopModel)
        {
            this.shopModel = shopModel;
            Browse();
        }

        // Attempt to select an item
        public void SelectItem(Item item)
        {
            if (item != null)
                shopModel.SelectItem(item);
        }

        public void Browse()
        {
            // Right now all this function does is select the first item in shopModel.
            shopModel.SelectItemByIndex(0);
        }

        public void Buy()
        {
            shopModel.Buy();
        }

        public void Sell()
        {
            shopModel.Sell();
        }
    }
}
