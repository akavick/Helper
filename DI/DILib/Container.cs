using System;
using System.Collections.Generic;
using System.Linq;

namespace DILib
{
    public class Container : IContainer
    {
        protected Dictionary<Type, Type> RegisteredTypes { get; } = new Dictionary<Type, Type>();

        protected Dictionary<Type, object> ScopedInstances { get; } = new Dictionary<Type, object>();

        protected static Dictionary<Type, object> Singletons { get; } = new Dictionary<Type, object>();


        public Container()
        {
            RegisterTransient<IContainer, Container>();
        }


        public bool IsScoped<TAbs>()
        {
            return IsScoped(typeof(TAbs));
        }


        public virtual bool IsScoped(Type abs)
        {
            return IsRegistered(abs) 
                ? ScopedInstances.ContainsKey(abs) 
                : throw new InvalidOperationException(
                    $"Тип {abs.FullName} не зарегистрирован в контейнере.");
        }


        public bool IsRegistered<TAbs>()
        {
            return IsRegistered(typeof(TAbs));
        }


        public virtual bool IsRegistered(Type abs)
        {
            return RegisteredTypes.ContainsKey(abs ?? throw new ArgumentNullException(nameof(abs)));
        }


        public Type GetImplementationType<TAbs>()
        {
            return GetImplementationType(typeof(TAbs));
        }


        public virtual Type GetImplementationType(Type abs)
        {
            return IsRegistered(abs)
                ? RegisteredTypes[abs]
                : throw new InvalidOperationException(
                    $"Тип {abs.FullName} не зарегистрирован в контейнере.");
        }


        public void RegisterSingleton<TImp>() where TImp : class
        {
            RegisterSingleton<TImp, TImp>();
        }


        public void RegisterSingleton<TAbs, TImp>(bool rewrite = false) where TImp : class, TAbs
        {
            RegisterSingleton(typeof(TAbs), typeof(TImp), rewrite);
        }


        public void RegisterSingleton(Type abs)
        {
            RegisterSingleton(abs, abs);
        }


        public virtual void RegisterSingleton(Type abs, Type imp, bool rewrite = false)
        {
            var hasKey = Singletons.TryGetValue(abs, out var oldValue);

            if (!hasKey)
            {
                Singletons.Add(abs, null);
            }
            else if (rewrite)
            {
                oldValue = Singletons[abs];
                Singletons[abs] = null;
            }
            else
            {
                throw new InvalidOperationException(
                    $"Тип {abs.FullName} уже зарегистрирован как синглтон.");
            }

            try
            {
                RegisterTransient(abs, imp, rewrite);
            }
            catch
            {
                if (!hasKey)
                {
                    Singletons.Remove(abs);
                }
                else
                {
                    Singletons[abs] = oldValue;
                }

                throw;
            }
        }


        public void RegisterScoped<TImp>() where TImp : class
        {
            RegisterScoped<TImp, TImp>();
        }


        public void RegisterScoped<TAbs, TImp>(bool rewrite = false) where TImp : class, TAbs
        {
            RegisterScoped(typeof(TAbs), typeof(TImp), rewrite);
        }


        public void RegisterScoped(Type abs)
        {
            RegisterScoped(abs, abs);
        }


        public void RegisterScoped(Type abs, Type imp, bool rewrite = false)
        {
            var hasKey = ScopedInstances.TryGetValue(abs, out var oldValue);

            if (!hasKey)
            {
                ScopedInstances.Add(abs, null);
            }
            else if (rewrite)
            {
                oldValue = ScopedInstances[abs];
                ScopedInstances[abs] = null;
            }
            else
            {
                throw new InvalidOperationException(
                    $"Тип {abs.FullName} уже зарегистрирован как синглтон для экземпляра контейнера.");
            }

            try
            {
                RegisterTransient(abs, imp, rewrite);
            }
            catch
            {
                if (!hasKey)
                {
                    ScopedInstances.Remove(abs);
                }
                else
                {
                    ScopedInstances[abs] = oldValue;
                }

                throw;
            }
        }


        public void RegisterTransient<TImp>()
            where TImp : class
        {
            RegisterTransient<TImp, TImp>();
        }


        public void RegisterTransient<TAbs, TImp>(bool rewrite = false)
            where TImp : class, TAbs
        {
            RegisterTransient(typeof(TAbs), typeof(TImp), rewrite);
        }


        public void RegisterTransient(Type abs)
        {
            RegisterTransient(abs, abs);
        }


        public virtual void RegisterTransient(Type abs, Type imp, bool rewrite = false)
        {
            abs = abs ?? throw new ArgumentNullException(nameof(abs));
            imp = imp ?? throw new ArgumentNullException(nameof(imp));

            if (!imp.IsClass)
            {
                throw new NotSupportedException(
                    $"Тип {imp.FullName} не является классом.");
            }

            if (!rewrite && RegisteredTypes.ContainsKey(abs))
            {
                throw new InvalidOperationException(
                    $"Тип {abs.FullName} уже зарегистрирован в контейнере.");
            }

            if (!abs.IsAssignableFrom(imp))
            {
                throw new InvalidOperationException(
                    $"Тип {imp.FullName} не может быть приведён к типу {abs.FullName}.");
            }

            var foundTypes = new[] { abs, imp }.Distinct().ToList();

            void CheckRecursion(Type type)
            {
                var ctors = type.GetConstructors();

                if (ctors.Length != 1)
                {
                    throw new NotSupportedException(
                        $"Неразрешимая зависимость: контейнер не может выбрать вызываемый конструктор для типа {type.FullName}.");
                }

                var constructorInfo = ctors.Single();

                var parameterTypes = constructorInfo.GetParameters()
                                                    .Select(p => p.ParameterType)
                                                    .Distinct()
                                                    .ToArray();

                foreach (var parameterType in parameterTypes)
                {
                    if (foundTypes.Contains(parameterType))
                    {
                        throw new NotSupportedException(
                            $"Неразрешимая зависимость: циклическая зависимость для типа {type.FullName} в типе параметра {parameterType.FullName}.");
                    }

                    if (parameterType.IsClass)
                    {
                        CheckRecursion(parameterType);
                    }
                }

                foundTypes.AddRange(parameterTypes);
            }

            CheckRecursion(imp);

            RegisteredTypes[abs] = imp;
        }


        public TAbs Resolve<TAbs>()
        {
            return (TAbs) Resolve(typeof(TAbs));
        }


        public virtual object Resolve(Type abs)
        {
            abs = abs ?? throw new ArgumentNullException(nameof(abs));

            if (!RegisteredTypes.ContainsKey(abs))
            {
                throw new NotSupportedException(
                    $"Тип {abs.FullName} не зарегистрирован в контейнере.");
            }

            if (Singletons.TryGetValue(abs, out var singleton) && singleton != null)
            {
                return singleton;
            } 
            
            if (ScopedInstances.TryGetValue(abs, out var scoped) && scoped != null)
            {
                return scoped;
            }

            var imp = RegisteredTypes[abs];
            var ctorInfo = imp.GetConstructors().Single();
            var parameters = ctorInfo.GetParameters();
            var args = parameters.Select(p => Resolve(p.ParameterType)).ToArray();
            var instance = Activator.CreateInstance(imp, args);

            if (Singletons.ContainsKey(abs))
            {
                Singletons[abs] = instance;
            }
            else if (ScopedInstances.ContainsKey(abs))
            {
                ScopedInstances[abs] = instance;
            }

            return instance;
        }
    }
}