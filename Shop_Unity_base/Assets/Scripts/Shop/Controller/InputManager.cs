﻿namespace Controller
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

        protected void SetKeyCodeToCommand(T keycode, ICommand command)
        {
            keyCodeToCommand.Add(keycode, command);
        }
    }
}
