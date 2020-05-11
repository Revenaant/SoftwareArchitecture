
namespace Model
{
    using System;
    using System.Collections.Generic;

    public interface IComponentOwner<U> where U : IComponentOwner<U>
    {
        Dictionary<Type, Component<U>> GetComponents();

        void AddComponent(Component<U> component);

        void AddComponents(params Component<U>[] components);

        T GetComponent<T>() where T : Component<U>;

        void RemoveComponent<T>() where T : Component<U>;
    }
}
