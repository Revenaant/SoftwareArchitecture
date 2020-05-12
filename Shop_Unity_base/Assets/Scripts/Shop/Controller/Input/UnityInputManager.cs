namespace Controller
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnityInputManager : InputManager<KeyCode>
    {
        private Dictionary<Button, ICommand> buttonToCommand = new Dictionary<Button, ICommand>();

        public UnityInputManager()
        {
            BindCommands();
        }

        private void BindCommands()
        {
            SetKeyCodeToCommand(KeyCode.Space, new SellCommand());
            SetKeyCodeToCommand(KeyCode.E, new ConsumeCommand());
            SetKeyCodeToCommand(KeyCode.C, new ClearCommand());
            SetKeyCodeToCommand(KeyCode.S, new SortCommand());
            SetKeyCodeToCommand(KeyCode.R, new RestockCommand());
        }

        public void BindCommandToButton(Button button, ICommand command, InventoryController inventoryController)
        {
            buttonToCommand.Add(button, command);
            button.onClick.AddListener(() => buttonToCommand[button]?.Execute(inventoryController));
        }

        public override ICommand HandleInput()
        {
            ICommand frameInput = null;

            // This implementation has an order priority to the Input pressed in the same frame
            foreach (KeyCode key in keyCodeToCommand.Keys)
            {
                frameInput = GetCommandDown(key);
                if (frameInput != null)
                    return frameInput;
            }

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
