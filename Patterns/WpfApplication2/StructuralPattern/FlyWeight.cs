﻿//Flyweight — Приспособленец

//Цель:
//Оптимизация работы с памятью, путем предотвращения создания экземпляров элементов, имеющих общую сущность.

//Описание:
//Flyweight используется для уменьшения затрат при работе с большим количеством мелких объектов.
//При проектировании приспособленца необходимо разделить его свойства на внешние и внутренние. 
//Внутренние свойства всегда неизменны, тогда как внешние могут отличаться в зависимости 
//от места и контекста применения и должны быть вынесены за пределы приспособленца.

//Flyweight дополняет паттерн Factory таким образом, 
//что Factory при обращении к ней клиента для создания нового объекта ищет 
//уже созданный объект с такими же параметрами, что и у требуемого, и возвращает его клиенту.
//Если такого объекта нет, то фабрика создаст новый.

using System;
using System.Collections;

namespace Flyweight
{
    class MainApp
    {
        static void Main()
        {
            // Build a document with text 
            string document = "AAZZBBZB";
            char[] chars = document.ToCharArray();
            CharacterFactory f = new CharacterFactory();

            // extrinsic state 
            int pointSize = 10;
            // For each character use a flyweight object 
            foreach (char c in chars)
            {
                pointSize++;
                Character character = f.GetCharacter(c);
                character.Display(pointSize);
            }
            // Wait for user 
            Console.Read();
        }
    }
    // "FlyweightFactory
    class CharacterFactory
    {
        private Hashtable characters = new Hashtable();

        public Character GetCharacter(char key)
        {
            // Uses "lazy initialization"
            Character character = characters[key] as Character;
            if (character == null)

                switch (key)
                {
                    case 'A':
                        character = new CharacterA(); break;
                    case 'B':
                        character = new CharacterB(); break;
                    case 'Z':
                        character = new CharacterZ(); break;
                }
            characters.Add(key, character);
            return character;
        }
    }
    // "Flyweight"
    abstract class Character
    {
        protected char symbol;
        protected int width;
        protected int height;
        protected int ascent;
        protected int descent;
        protected int pointSize;

        public abstract void Display(int pointSize);
    }
    // "ConcreteFlyweight"
    class CharacterA : Character
    {
        // Constructor 
        public CharacterA()
        {
            this.symbol = 'A';
            this.height = 100;
            this.width = 120;
            this.ascent = 70;
            this.descent = 0;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize; Console.WriteLine(this.symbol + " (pointsize " + this.pointSize + ")");
        }
    }
    // "ConcreteFlyweight"
    class CharacterB : Character
    {
        // Constructor 
        public CharacterB()
        {
            this.symbol = 'B';
            this.height = 100;
            this.width = 140;
            this.ascent = 72;
            this.descent = 0;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize; Console.WriteLine(this.symbol + " (pointsize " + this.pointSize + ")");
        }
    }
    // ... C, D, E, etc.
    // "ConcreteFlyweight"
    class CharacterZ : Character
    {
        // Constructor 
        public CharacterZ()
        {
            this.symbol = 'Z';
            this.height = 100;
            this.width = 100;
            this.ascent = 68;
            this.descent = 0;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize; Console.WriteLine(this.symbol + " (pointsize " + this.pointSize + ")");
        }
    }
}
