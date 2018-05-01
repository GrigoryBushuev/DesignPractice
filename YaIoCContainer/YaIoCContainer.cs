using System;
using System.Collections.Generic;
using System.Linq;

namespace YaIoCContainer
{
    public class YaIoCContainer
    {
        private class RegisteredObject
        {
            public object Instance { private set; get; }

            public Type ConcreteType { private set; get; }

            public RegisteredObject(Type concreteType) : this(concreteType, null)
            {
                ConcreteType = concreteType;
            }

            public RegisteredObject(Type concreteType, object instance)
            {
                ConcreteType = concreteType;
                Instance = instance;
            }

            public object CreateInstance(params object[] args)
            {
                Instance = Activator.CreateInstance(ConcreteType, args);
                return Instance;
            }
        }

        private static readonly Dictionary<Type, RegisteredObject> _registeredTypes = new Dictionary<Type, RegisteredObject>(); 

        public YaIoCContainer RegisterType<TTypeToResolve, TConcreteType>()
        {
            _registeredTypes[typeof(TTypeToResolve)] = new RegisteredObject(typeof(TConcreteType));
            return this;
        }

        public YaIoCContainer RegisterInstanceOf<TTypeToResolve>(object instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            _registeredTypes[typeof(TTypeToResolve)] = new RegisteredObject(instance.GetType(), instance);
            return this;
        }

        public TTypeToResolve Resolve<TTypeToResolve>() 
        { 
            return (TTypeToResolve)Resolve(typeof(TTypeToResolve));
        }

        private object Resolve(Type typeToResolve)
        {
            var resolvingType = _registeredTypes[typeToResolve];
            if (resolvingType == null)
                return null;

            var result = resolvingType.Instance;            
            return result ?? ResolveConstructor(resolvingType);
        }

        private object ResolveConstructor(RegisteredObject resolvingType)
        {
            object result = null;
            foreach (var constructor in resolvingType.ConcreteType.GetConstructors().OrderBy(c => c.GetParameters().Count()))
            {
                var constructorParams = constructor.GetParameters();
                if (!constructorParams.Select(p => p.ParameterType).Except(_registeredTypes.Keys).Any())
                {
                    var paramInfos = constructorParams.Select(c => Resolve(c.ParameterType)).ToArray();
                    result = resolvingType.CreateInstance(paramInfos);
                }
            }
            return result;
        }
    }
}
