//Decorator — Декоратор

//Задача:
//Объект, который предполагается использовать, выполняет основные функции. 
//Однако может потребоваться добавить к нему некоторую дополнительную функциональность, 
//которая будет выполняться до, после или даже вместо основной функциональности объекта.

//Способ решения:
//Декоратор предусматривает расширение функциональности объекта без определения подклассов.

//Участники:
//Класс ConcreteComponent — класс, в который с помощью шаблона Декоратор добавляется новая функциональность. 
//В некоторых случаях базовая функциональность предоставляется классами, производными от класса ConcreteComponent. 
//В подобных случаях класс ConcreteComponent является уже не конкретным, а абстрактным. 
//Абстрактный класс Component определяет интерфейс для использования всех этих классов.

//Следствия:
//1. Добавляемая функциональность реализуется в небольших объектах. 
//Преимущество состоит в возможности динамически добавлять эту функциональность до или после основной функциональности объекта ConcreteComponent.
//2. Позволяет избегать перегрузки функциональными классами на верхних уровнях иерархии
//3. Декоратор и его компоненты не являются идентичными


using System;

namespace Decorator
{
    class MainApp
    {
        static void Main()
        {
            // Create ConcreteComponent and two Decorators 
            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();
            // Link decorators 
            d1.SetComponent(c);
            d2.SetComponent(d1);
            d2.Operation();
            // Wait for user 
            Console.Read();
        }
    }
    /// определяем интерфейс для объектов, на которые могут быть динамически возложены дополнительные обязанности;
    abstract class Component
    {
        public abstract void Operation();
    }

    /// определяет объект, на который возлагается дополнительные обязанности
    class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteComponent.Operation()");
        }
    }
    ///хранит ссылку на объект Component и определяет интерфейс, соответствующий интерфейсу Component
    abstract class Decorator : Component
    {
        protected Component component;

        public void SetComponent(Component component)
        {
            this.component = component;
        }
        public override void Operation()
        {
            if (component != null)
                component.Operation();
        }
    }
    /// возглагает дополнительные обязанности на компонент.
    class ConcreteDecoratorA : Decorator
    {
        private string addedState;

        public override void Operation()
        {
            base.Operation(); addedState = "New State";
            Console.WriteLine("ConcreteDecoratorA.Operation()");
        }
    }
    class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
            Console.WriteLine("ConcreteDecoratorB.Operation()");
        }
        void AddedBehavior() { }
    }
}