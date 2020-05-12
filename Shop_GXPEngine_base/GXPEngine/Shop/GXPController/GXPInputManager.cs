namespace Controller
{
    using GXPEngine;
    using Model;

    public class GXPInputManager : InputManager<int>
    {
        public GXPInputManager()
        {
            BindCommands();
        }

        private void BindCommands()
        {
            SetKeyCodeToCommand(Key.SPACE, new SellCommand());
            SetKeyCodeToCommand(Key.E, new ConsumeCommand());
            SetKeyCodeToCommand(Key.C, new ClearCommand());
            SetKeyCodeToCommand(Key.F, new SortCommand());
            SetKeyCodeToCommand(Key.R, new RestockCommand());
        }

        public override ICommand HandleInput()
        {
            ICommand frameInput = null;

            // This implementation has an order priority to the Input pressed in the same frame
            foreach (int key in keyCodeToCommand.Keys)
            {
                frameInput = GetCommandDown(key);

                if (frameInput != null)
                    return frameInput;
            }

            return frameInput;
        }

        protected override bool GetKey(int key)
        {
            return Input.GetKey(key);
        }

        protected override bool GetKeyDown(int key)
        {
            return Input.GetKeyDown(key);
        }

        protected override bool GetKeyUp(int key)
        {
            return Input.GetKeyUp(key);
        }
    }
}
