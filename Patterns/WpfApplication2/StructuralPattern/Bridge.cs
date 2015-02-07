//Bridge — Мост

//Цель:
//При частом изменении класса преимущества объектно-ориентированного подхода становятся очень полезными, 
//позволяя делать изменения в программе, обладая минимальными сведениями о реализации программы. 
//Шаблон bridge является полезным там, где часто меняется не только сам класс, но и то, что он делает.

//Использование:
//Архитектура Java AWT полностью основана на этом паттерне 
//- иерархия java.awt.xxx для хэндлов и sun.awt.xxx для реализаций.

using System;
namespace Bridge
{
    // MainApp test application
    class MainApp
    {
        static void Main()
        {
            Abstraction ab = new RefinedAbstraction();

            // Set implementation and call 
            ab.Implementor = new ConcreteImplementorA();
            ab.Operation();
            // Change implementation and call 
            ab.Implementor = new ConcreteImplementorB();
            ab.Operation();
            // Wait for user 
            Console.Read();
        }
    }
    /// Abstraction - абстракция 
    /// определяем интерфейс абстракции;
    /// хранит ссылку на объект <see cref="Implementor"/>
    class Abstraction
    {
        protected Implementor implementor;

        public Implementor Implementor
        {
            get
            {
                return implementor;
            }
            set
            {
                implementor = value;
            }
        }

        public virtual void Operation()
        {
            implementor.Operation();
        }
    }
    /// Implementor - реализатор 
    /// определяет интерфейс для классов реализации. Он не обязан точно соотведствовать интерфейсу класса <see cref="Abstraction"/>. На самом деле оба 
    /// интерфейса могут быть совершенно различны. Обычно интерфейс класса
    /// <see cref="Implementor"/> представляет только примитивные операции, а класс 
    /// <see cref="Abstraction"/> определяет операции более высокого уровня, базирующиеся на этих примитивах;
    abstract class Implementor
    {
        public abstract void Operation();

        /// RefinedAbstraction - уточненная абстракция
        /// расширяет интерфейс, определенный абстракцией <see cref="Abstraction"/> 
        class RefinedAbstraction : Abstraction
        {
            public override void Operation()
            {
                implementor.Operation();
            }
        }
    }
    /// ConcreteImplementor - конкретный реализатор 
    /// содержит конкретную реализацию интерфейса <see cref="Implementor"/>
    class ConcreteImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorA Operation");
        }
    }
    // "ConcreteImplementorB"
    class ConcreteImplementorB : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorB Operation");
        }
    }
}

