
namespace Model
{
    using System;
    using Model.Items;
    using System.Collections.Generic;

    public abstract class Item : IClonable, ITradeable, IComponentOwner<Item>
    {
        public enum ItemType { Potion, Weapon }

        protected Dictionary<Type, Component<Item>> TypeToComponent = new Dictionary<Type, Component<Item>>();

        protected string name;
        protected int cost;

        public string Name => name;
        public int Cost => cost;

        protected Item()
        {
        }

        protected Item(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
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

        protected virtual void CloneMembers(Item item)
        {
            name = item.name;
            cost = item.cost;

            foreach (Component<Item> component in item.TypeToComponent.Values)
                AddComponent((Component<Item>)component.Clone());
        }

        public abstract IClonable Clone();
    }
}
