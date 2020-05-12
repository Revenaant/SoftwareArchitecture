namespace Model
{
    using System;
    using System.Collections.Generic;

    public interface IComponentOwner<U> where U : IComponentOwner<U>
    {
        Dictionary<Type, CustomComponent<U>> GetComponents();

        void AddComponent(CustomComponent<U> component);

        void AddComponents(params CustomComponent<U>[] components);

        T GetComponent<T>() where T : CustomComponent<U>;

        void RemoveComponent<T>() where T : CustomComponent<U>;
    }
}
