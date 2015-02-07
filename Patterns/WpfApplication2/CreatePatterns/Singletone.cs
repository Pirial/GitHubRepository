//Singleton — Одиночка

//Плюс:
//контролируемый доступ к единственному экземпляру;
//уменьшение числа имён;
//допускает уточнение операций и представления;
//допускает переменное число экземпляров;
//большая гибкость, чем у операций класса.

//Минусы:
//глобальные объекты могут быть вредны для объектного программирования, 
//в некоторых случаях приводя к созданию немасштабируемого проекта.
//усложняет написание модульных тестов и следование TDD


/// generic Singleton<T> (потокобезопасный с использованием generic-класса и с отложенной инициализацией)
/// <typeparam name="T">Singleton class</typeparam> 
using System.Reflection;

namespace First
{
    public class Singleton<T> where T : class
    {
        /// Защищённый конструктор необходим для того, чтобы предотвратить создание экземпляра класса Singleton.
        /// Он будет вызван из закрытого конструктора наследственного класса. 
        protected Singleton() { }

        public static T Instance
        {
            get { return SingletonCreator<T>.CreatorInstance; }
        }

        /// Фабрика используется для отложенной инициализации экземпляра класса     
        private sealed class SingletonCreator<S> where S : class
        {
            //Используется Reflection для создания экземпляра класса без публичного конструктора 
            private static readonly S instance = (S)typeof(S).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new System.Type[0], new ParameterModifier[0]).Invoke(null);
            public static S CreatorInstance
            {
                get { return instance; }
            }
        }
    }
}

/// Использование Singleton
public class TestClass : First.Singleton<TestClass>
{
    /// Вызовет защищенный конструктор класса Singleton 
    private TestClass() { }

    public string TestProc() { return "Hello World"; }
}

//Также можно использовать стандартный вариант потокобезопасной реализации Singleton с отложенной инициализацией:
namespace Second
{
    public class Singleton
    {
        /// Защищенный конструктор нужен, чтобы предотвратить создание экземпляра класса Singleton 
        protected Singleton() { }

        private sealed class SingletonCreator
        {
            private static readonly Singleton instance = new Second.Singleton();
            public static Singleton Instance
            {
                get { return instance; }
            }
        }
        public static Singleton Instance
        {
            get { return SingletonCreator.Instance; }
        }
    }
}

//Если нет необходимости в каких-либо публичных статических методах или свойствах (кроме свойства Instance), то можно использовать упрощенный вариант:
namespace Third
{
    public class Singleton
    {
        private static readonly Singleton instance = new Singleton();
        public static Singleton Instance
        {
            get { return instance; }
        }
        /// Защищенный конструктор нужен, чтобы предотвратить создание экземпляра класса Singleton 
        protected Singleton() { }
    }
}