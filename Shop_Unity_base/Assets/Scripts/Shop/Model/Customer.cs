using System;
using System.Collections.Generic;

namespace Model
{
    public class CustomerModel : TraderModel, IComponentOwner<CustomerModel>, IConsumer
    {
        private const int INVENTORY_CAPACITY = 24;

        private Dictionary<Type, Component<CustomerModel>> TypeToComponent = new Dictionary<Type, Component<CustomerModel>>();

        public CustomerModel(string name, int startingGold) : base()
        {
            Initialize(name, startingGold, INVENTORY_CAPACITY);
        }

        protected override void Initialize(string name, int startingGold, int inventoryCapacity)
        {
            Name = name;
            Gold = startingGold;
            Inventory = new Inventory(inventoryCapacity);
        }

        public override void Restock()
        {
            int goldGained = GetGoldFromAdventure();
            AddGold(goldGained);
            NotifyRedrawObservers(new RedrawNotification($"{Name} returned from Adventure, obtaining {goldGained} gold"));
        }

        // Simulate adventuring functionality
        private int GetGoldFromAdventure()
        {
            return Utility.Random.Get(200, 350);
        }

        void IConsumer.Consume(Item item)
        {
            if (item == null)
                return;

            ConsumableComponent consumable = item.GetComponent<ConsumableComponent>();
            RPGStatsComponent stats = GetComponent<RPGStatsComponent>();

            if (consumable == null || stats == null)
                return;

            stats.Consume(consumable);

            if (consumable.IsSpent)
                Inventory.Remove(item);

            NotifyRedrawObservers(new RedrawNotification($"{Name} consumed {item.Name}, " +
                $"receiving effect: [{consumable.Effect.ToString()}] for {consumable.Potency} points"));
        }

        public void AddComponent(Component<CustomerModel> component)
        {
            if (TypeToComponent.ContainsKey(component.GetType()))
                return;

            Type type = component.GetType();
            TypeToComponent.Add(type, component);
            component.SetOwner(this);
        }

        public void AddComponents(params Component<CustomerModel>[] components)
        {
            for (int i = 0; i < components.Length; i++)
                AddComponent(components[i]);
        }

        public T GetComponent<T>() where T : Component<CustomerModel>
        {
            if (!TypeToComponent.ContainsKey(typeof(T)))
                return null;

            return (T)TypeToComponent[typeof(T)];
        }

        public void RemoveComponent<T>() where T : Component<CustomerModel>
        {
            Type type = typeof(T);

            if (TypeToComponent.ContainsKey(type))
            {
                TypeToComponent[type].SetOwner(null);
                TypeToComponent.Remove(type);
            }
        }

        Dictionary<Type, Component<CustomerModel>> IComponentOwner<CustomerModel>.GetComponents()
        {
            return TypeToComponent;
        }
    }
}
