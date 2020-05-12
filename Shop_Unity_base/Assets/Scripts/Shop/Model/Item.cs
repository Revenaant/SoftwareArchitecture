
namespace Model
{
    using System;
    using Model.Items;
    using System.Collections.Generic;
    using Utility;

    public abstract class Item : IClonable, ITradeable, IComparable<Item>, IComponentOwner<Item>
    {
        public enum ItemType { Potion, Weapon }

        protected Dictionary<Type, Component<Item>> TypeToComponent = new Dictionary<Type, Component<Item>>();

        protected string name;
        public string Name => name;

        protected int cost;
        public int Cost => cost;

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;

                DrawableComponent drawable = GetComponent<DrawableComponent>();
                if (drawable != null)
                    drawable.SetQuantity(quantity);
            }
        }

        protected Item()
        {
        }

        protected Item(string name, int cost, int quantity = 1)
        {
            this.name = name;
            this.cost = cost;
            this.quantity = quantity;
        }

        public void AddComponent(Component<Item> component)
        {
            if (TypeToComponent.ContainsKey(component.GetType()))
                return;

            Type type = component.GetType();
            TypeToComponent.Add(type, component);
            component.SetOwner(this);
        }

        public void AddComponents(params Component<Item>[] components)
        {
            for (int i = 0; i < components.Length; i++)
                AddComponent(components[i]);
        }

        public T GetComponent<T>() where T : Component<Item>
        {
            if (!TypeToComponent.ContainsKey(typeof(T)))
                return null;

            return (T)TypeToComponent[typeof(T)];
        }

        public void RemoveComponent<T>() where T : Component<Item>
        {
            Type type = typeof(T);

            if (TypeToComponent.ContainsKey(type))
            {
                TypeToComponent[type].SetOwner(null);
                TypeToComponent.Remove(type);
            }
        }

        Dictionary<Type, Component<Item>> IComponentOwner<Item>.GetComponents()
        {
            return TypeToComponent;
        }

        protected virtual void CloneMembers(Item original)
        {
            name = original.name;
            cost = original.cost;
            quantity = 1;

            foreach (Component<Item> component in original.TypeToComponent.Values)
                AddComponent((Component<Item>)component.Clone());
        }

        public abstract IClonable Clone();

        int IComparable<Item>.CompareTo(Item other)
        {
            if (other == null)
                return 1;

            return name.CompareTo(other.name);
        }
    }
}
