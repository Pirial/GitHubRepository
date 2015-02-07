//Adapter — Адаптер

//Задача:
//Система поддерживает требуемые данные и поведение, но имеет неподходящий интерфейс. 
//Чаще всего шаблон Адаптер применяется, если необходимо создать класс, 
//производный от вновь определяемого или уже существующего абстрактного класса.

//Участники:
//Класс Adapter приводит интерфейс класса Adaptee в соответствие с интерфейсом класса Target (наследником которого является Adapter).
//Это позволяет объекту Client использовать объект Adaptee (посредством адаптера Adapter) так, словно он является экземпляром класса Target.

//Таким образом Client обращается к интерфейсу Target, реализованному в наследнике Adapter, который перенаправляет обращение к Adaptee.


using System;

namespace Adapter
{
    class MainApp
    {
        static void Main()
        {
            // Create adapter and place a request 
            Target target = new Adapter();
            target.Request();
            // Wait for  user 
            Console.Read();
        }
        // "Target"
        class Target
        {
            public virtual void Request()
            {
                Console.WriteLine("Called Target Request()");
            }
        }
        // "Adapter"
        class Adapter : Target
        {
            private Adaptee adaptee = new Adaptee();

            public override void Request() { }
        }
        // Possibly do some other work // and then call SpecificRequest adaptee.SpecificRequest();
        // "Adaptee"
        class Adaptee
        {
            public void SpecificRequest()
            {
                Console.WriteLine("Called SpecificRequest()");
            }
        }
    }
}