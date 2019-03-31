using System;

namespace DILib
{
    /// <summary>
    /// DI контейнер.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Является ли зарегистрированный тип синглтоном.
        /// </summary>
        bool IsScoped<TAbs>();

        /// <summary>
        /// Является ли зарегистрированный тип синглтоном.
        /// </summary>
        bool IsScoped(Type abs);

        /// <summary>
        /// Зарегистрирован ли тип в контейнере.
        /// </summary>
        bool IsRegistered<TAbs>();

        /// <summary>
        /// Зарегистрирован ли тип в контейнере.
        /// </summary>
        bool IsRegistered(Type abs);

        /// <summary>
        /// Получить тип имплементации по типу абстракции.
        /// </summary>
        Type GetImplementationType<TAbs>();

        /// <summary>
        /// Получить тип имплементации по типу абстракции.
        /// </summary>
        Type GetImplementationType(Type abs);

        /// <summary>
        /// Зарегистрировать тип в контейнере как синглтон.
        /// </summary>
        void RegisterSingleton<TImp>() where TImp : class;

        /// <summary>
        /// Зарегистрировать тип в контейнере как синглтон.
        /// </summary>
        void RegisterSingleton<TAbs, TImp>(bool rewrite = false) where TImp : class, TAbs;

        /// <summary>
        /// Зарегистрировать тип в контейнере как синглтон.
        /// </summary>
        void RegisterSingleton(Type abs);

        /// <summary>
        /// Зарегистрировать тип в контейнере как синглтон.
        /// </summary>
        void RegisterSingleton(Type abs, Type imp, bool rewrite = false);

        /// <summary>
        /// Зарегистрировать тип в контейнере как синглтон для экземпляра контейнера.
        /// </summary>
        void RegisterScoped<TImp>() where TImp : class;

        /// <summary>
        /// Зарегистрировать тип в контейнере как синглтон для экземпляра контейнера.
        /// </summary>
        void RegisterScoped<TAbs, TImp>(bool rewrite = false) where TImp : class, TAbs;

        /// <summary>
        /// Зарегистрировать тип в контейнере как синглтон для экземпляра контейнера.
        /// </summary>
        void RegisterScoped(Type abs);

        /// <summary>
        /// Зарегистрировать тип в контейнере как синглтон для экземпляра контейнера.
        /// </summary>
        void RegisterScoped(Type abs, Type imp, bool rewrite = false);

        /// <summary>
        /// Зарегистрировать тип в контейнере.
        /// </summary>
        void RegisterTransient<TImp>() where TImp : class;

        /// <summary>
        /// Зарегистрировать тип в контейнере.
        /// </summary>
        void RegisterTransient<TAbs, TImp>(bool rewrite = false) where TImp : class, TAbs;

        /// <summary>
        /// Зарегистрировать тип в контейнере.
        /// </summary>
        void RegisterTransient(Type abs);

        /// <summary>
        /// Зарегистрировать тип в контейнере.
        /// </summary>
        void RegisterTransient(Type abs, Type imp, bool rewrite = false);

        /// <summary>
        /// Разрешить абстракцию со всеми зависимостями.
        /// </summary>
        TAbs Resolve<TAbs>();

        /// <summary>
        /// Разрешить абстракцию со всеми зависимостями.
        /// </summary>
        object Resolve(Type abs);
    }
}