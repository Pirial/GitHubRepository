﻿//Builder — Строитель

//Цель:
//Отделяет конструирование сложного объекта от его представления,
//так что в результате одного и того же процесса конструирования могут получаться разные представления.

//Плюсы:
//позволяет изменять внутреннее представление продукта;
//изолирует код, реализующий конструирование и представление;
//дает более тонкий контроль над процессом конструирования.


using System;
using System.Collections.Generic;

namespace Builder
{
    public class MainApp
    {
        public static void MainM()
        {
            // Create director and builders 
            Director director = new Director();
            Builder b1 = new ConcreteBuilder1();
            Builder b2 = new ConcreteBuilder2();
            // Construct two products 
            director.Construct(b1);
            Product p1 = b1.GetResult(); p1.Show();
            director.Construct(b2);
            Product p2 = b2.GetResult(); p2.Show();
        }
        // Wait for user Console.Read();
        // "Director"
        class Director
        {
            // Builder uses a complex series of steps 
            public void Construct(Builder builder)
            {
                builder.BuildPartA(); builder.BuildPartB();
            }
        }
        // "Builder"
        abstract class Builder
        {
            public virtual void BuildPartA(){}
            public virtual void BuildPartB(){}
            public abstract Product GetResult();
        }

        // "ConcreteBuilder1"
        class ConcreteBuilder1 : Builder
        {
            private readonly Product product = new Product();

            public override void BuildPartA()
            {
                product.Add("PartA");
            }

            public override void BuildPartB()
            {
                product.Add("PartB");
            }

            public override Product GetResult()
            {
                return product;
            }
        }
        // "ConcreteBuilder2"
        class ConcreteBuilder2 : Builder
        {
            private readonly Product product = new Product();

            public override void BuildPartA()
            {
                product.Add("PartX");
            }
            public override void BuildPartB()
            {
                product.Add("PartY");
            }
            public override Product GetResult()
            {
                return product;
            }
        }
        // "Product"
        class Product
        {
            private readonly List<string> parts = new List<string>();

            public void Add(string part)
            {
                parts.Add(part);
            }

            public void Show()
            {
                Console.WriteLine("\nProduct Parts -------"); foreach (string part in parts)
                    Console.WriteLine(part);
            }
        }
    }
}