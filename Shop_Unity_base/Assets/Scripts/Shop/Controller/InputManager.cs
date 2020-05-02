namespace Controller
{
    using System.Collections.Generic;

    /// <summary>
    /// InputManager abstracts the input Type so it can be used with multiple Input systems.
    /// GXPEngine uses int, while Unity uses KeyCode or string.
    /// </summary>
    /// <typeparam name="T">The type class of the input codes</typeparam>
    public abstract class InputManager<T>
    {
        protected Dictionary<T, ICommand> keyCodeToCommand = new Dictionary<T, ICommand>();

        public abstract ICommand HandleInput();

        public virtual void Update(InventoryController controller)
        {
            ICommand input = HandleInput();
            input?.Execute(controller);
        }

        protected void SetKeyCodeToCommand(T keycode, ICommand command)
        {
            keyCodeToCommand.Add(keycode, command);
        }

        protected ICommand GetCommand(T key)
        {
            if (!GetKey(key))
                return null;

            return keyCodeToCommand[key];
        }

        protected ICommand GetCommandDown(T key)
        {
            if (!GetKeyDown(key))
                return null;

            return keyCodeToCommand[key];
        }

        protected ICommand GetCommandUp(T key)
        {
            if (!GetKeyUp(key))
                return null;

            return keyCodeToCommand[key];
        }

        protected abstract bool GetKey(T key);
        protected abstract bool GetKeyDown(T key);
        protected abstract bool GetKeyUp(T key);
    }
}
