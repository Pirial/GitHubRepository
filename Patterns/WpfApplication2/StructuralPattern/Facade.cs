//Facade — Фасад

//Шаблон Facade (Фасад) — Шаблон проектирования, 
//позволяющий скрыть сложность системы путем сведения всех возможных 
//внешних вызовов к одному объекту, делегирующему их соответствующим объектам системы


using System;

namespace Library
{
    ///реализует функциональность подсистемы;
    ///выполняет работу, порученную объектом Facade
    ///ничего не "знает" о существовании фасада, то есть не хранит ссылок на него;
    internal class SubsystemA
    {
        internal string A1()
        {
            return "Subsystem A, Method A1\n";
        }
        internal string A2()
        {
            return "Subsystem A, Method A2\n";
        }
    }
    internal class SubsystemB
    {
        internal string B1()
        {
            return "Subsystem B, Method B1\n";
        }
    }
    internal class SubsystemC
    {
        internal string C1()
        {
            return "Subsystem C, Method C1\n";
        }
    }
    /// Facade - фасад 
    /// "знает", каким классами подсистемы адресовать запрос;
    /// делегирует запросы клиентов подходящим объектам внутри подсистемы;
    public static class Facade
    {
        static Library.SubsystemA a = new Library.SubsystemA();
        static Library.SubsystemB b = new Library.SubsystemB();
        static Library.SubsystemC c = new Library.SubsystemC();

        public static void Operation1()
        {
            Console.WriteLine("Operation 1\n" + a.A1() + a.A2() + b.B1());
        }
        public static void Operation2()
        {
            Console.WriteLine("Operation 2\n" + b.B1() + c.C1());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Facade.Operation1();
            Facade.Operation2();
            // Wait for user 
            Console.Read();
        }
    }
}