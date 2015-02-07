//Factory method — Фабричный метод

//Цель:
//Определяет интерфейс для создания объекта, но оставляет подклассам решение о том,
//какой класс инстанциировать. Фабричный метод позволяет классу делегировать создание подклассов. 
//Используется, когда:
//- классу заранее неизвестно, объекты каких подклассов ему нужно создавать.
//- класс спроектирован так, чтобы объекты, которые он создаёт, специфицировались подклассами.

//Класс делегирует свои обязанности одному из нескольких вспомогательных подклассов, 
//и планируется локализовать знание о том, какой класс принимает эти обязанности на себя.

//Плюсы:
//Позволяет сделать код создания объектов более универсальным, 
//не привязываясь к конкретным классам (ConcreteProduct), а оперируя лишь общим интерфейсом (Product);
//позволяет установить связь между параллельными иерархиями классов.

//Минусы:
//Необходимость создавать наследника Creator для каждого нового типа продукта (ConcreteProduct).

//Product — продукт определяет интерфейс объектов, создаваемых абстрактным методом;
//ConcreteProduct — конкретный продукт реализует интерфейс Product;
//Creator — создатель объявляет фабричный метод, который возвращает объект типа Product. 
//Может также содержать реализацию этого метода «по умолчанию»;
//может вызывать фабричный метод для создания объекта типа Product;
//ConcreteCreator — конкретный создатель переопределяет фабричный метод таким образом, 
//чтобы он создавал и возвращал объект класса ConcreteProduct.


using System;
using System.Collections.Generic;

namespace Factory
{
    public class MainApp
    {
        public static void MainM()
        {
            // an array of creators
            Creator[] creators = { new ConcreteCreatorA(), new ConcreteCreatorB() };

            // iterate over creators and create products 
            foreach (Creator creator in creators)
            {
                Product product = creator.FactoryMethod();
                Console.WriteLine("Created 0", product.GetType());
            }
            // Wait for user 
            Console.Read();
        }
        // Product
        abstract class Product { }

        // "ConcreteProductA"
        class ConcreteProductA : Product { }

        // "ConcreteProductB"
        class ConcreteProductB : Product { }

        // "Creator"
        abstract class Creator
        {
            public abstract Product FactoryMethod();
        }

        // "ConcreteCreatorA"
        class ConcreteCreatorA : Creator
        {
            public override Product FactoryMethod()
            {
                return new ConcreteProductA();
            }
        }

        // "ConcreteCreatorB"
        class ConcreteCreatorB : Creator
        {
            public override Product FactoryMethod()
            {
                return new ConcreteProductB();
            }
        }
    }
}