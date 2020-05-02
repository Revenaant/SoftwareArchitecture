namespace Controller
{
    using UnityEngine;
    using Model;

    public class UnityInputManager : InputManager<KeyCode>
    {
        public UnityInputManager()
        {
            BindCommands();
        }

        private void BindCommands()
        {
            SetKeyCodeToCommand(KeyCode.Space, new BuyCommand());
            SetKeyCodeToCommand(KeyCode.Backspace, new SellCommand());
            SetKeyCodeToCommand(KeyCode.C, new ClearCommand());
            SetKeyCodeToCommand(KeyCode.S, new SortCommand());
            SetKeyCodeToCommand(KeyCode.R, new RestockCommand());
        }

        public void Update(Inventory inventory)
        {
            ICommand input = HandleInput();
            input?.Execute(inventory);
        }

        public override ICommand HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                return keyCodeToCommand[KeyCode.Space];

            return null;
        }
    }
}
