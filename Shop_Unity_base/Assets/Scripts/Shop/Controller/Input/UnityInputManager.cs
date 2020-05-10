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
            SetKeyCodeToCommand(KeyCode.Space, new SellCommand());
            SetKeyCodeToCommand(KeyCode.C, new ClearCommand());
            SetKeyCodeToCommand(KeyCode.S, new SortCommand());
            SetKeyCodeToCommand(KeyCode.R, new RestockCommand());
        }

        public override ICommand HandleInput()
        {
            ICommand frameInput = null;

            // This implementation has an order priority to the Input pressed in the same frame
            foreach (KeyCode key in keyCodeToCommand.Keys)
                frameInput = GetCommandDown(key);

            return frameInput;
        }

        protected override bool GetKey(KeyCode key)
        {
            return Input.GetKey(key);
        }

        protected override bool GetKeyDown(KeyCode key)
        {
            return Input.GetKeyDown(key);
        }

        protected override bool GetKeyUp(KeyCode key)
        {
            return Input.GetKeyUp(key);
        }
    }
}
