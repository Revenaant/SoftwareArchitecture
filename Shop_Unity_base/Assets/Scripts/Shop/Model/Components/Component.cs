
namespace Model
{
    public abstract class Component<T> : IClonable
    {
        private T owner;
        public T Owner => owner;

        private bool enabled;
        public bool Enabled => enabled;

        public void SetOwner(T owner)
        {
            this.owner = owner;
        }

        public void SetEnabled(bool value)
        {
            enabled = value;
        }

        public abstract IClonable Clone();
    }
}
