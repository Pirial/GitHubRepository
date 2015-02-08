//Proxy — Заместитель

//Проблема:
//Необходимо управлять доступом к объекту так, чтобы создавать громоздкие объекты «по требованию».

//Решение:
//Создать суррогат громоздкого объекта. «Заместитель» хранит ссылку, 
//которая позволяет заместителю обратиться к реальному субъекту 
//(объект класса «Заместитель» может обращаться к объекту класса «Субъект», 
//если интерфейсы «Реального Субъекта» и «Субъекта» одинаковы). 
//Поскольку интерфейс «Реального Субъекта» идентичен интерфейсу «Субъекта», так,
//что «Заместителя» можно подставить вместо «Реального Субъекта», 
//контролирует доступ к «Реальному Субъекту», может отвечать за создание или 
//удаление «Реального Субъекта». «Субъект» определяет общий для «Реального Субъекта»
//и «Заместителя» интерфейс, так, что «Заместитель» может быть использован везде, 
//где ожидается «Реальный Субъект». При необходимости запросы могут
//быть переадресованы «Заместителем» «Реальному Субъекту».


using System;
using System.Threading;

class MainApp
{
    static void Main()
    {
        // Create math proxy
        IMath p = new MathProxy();
        // Do the math
        Console.WriteLine("4 + 2 = " + p.Add(4, 2)); 
        Console.WriteLine("4 - 2 = " + p.Sub(4, 2)); 
        Console.WriteLine("4 * 2 = " + p.Mul(4, 2)); 
        Console.WriteLine("4 / 2 = " + p.Div(4, 2));
        // Wait for user 
        Console.Read();
    }
    /// Subject - субъект 
    ///определяет общий для Math и Proxy интерфейс, так что класс Proxy можно использовать везде, где ожидается Math
    public interface IMath
    {
        double Add(double x, double y); double Sub(double x, double y); double Mul(double x, double y); double Div(double x, double y);
    }
    /// RealSubject - реальный объект 
    ///определяет реальный объект, представленный заместителем
    class Math : IMath
    {
        public Math()
        {
            Console.WriteLine("Create object Math. Wait..."); Thread.Sleep(1000);
        }
        public double Add(double x, double y)
        {
            return x + y;
        }
        public double Sub(double x, double y)
        {
            return x - y;
        }
        public double Mul(double x, double y)
        {
            return x * y;
        }
        public double Div(double x, double y)
        {
            return x / y;
        }
    }
    /// Proxy - заместитель 
    /// хранит ссылку, которая позволяет заместителю обратиться к реальному субъекту.
    /// Объект класса MathProxy" может обращаться к объекту класса IMath, 
    /// если интерфейсы классов Math и IMath одинаковы; 
    /// предоставляет интерфейс, идентичный интерфейсу IMath, 
    /// так что заместитель всегда может быть предоставлен вместо реального субъекта; 
    /// контролирует доступ к реальному субъекту и может отвечать за его создание  и удаление;
    class MathProxy : IMath
    {
        Math math;
        public MathProxy()
        {
            math = null;
        }
        /// Быстрая операция - не требует реального субъекта 
        public double Add(double x, double y)
        {
            return x + y;
        }
        public double Sub(double x, double y)
        {
            return x - y;
        }

        /// Медленная операция - требует создания реального субъекта 
        public double Mul(double x, double y)
        {
            if (math == null)
                math = new Math();
            return math.Mul(x, y);
        }
        public double Div(double x, double y)
        {
            if (math == null)
                math = new Math();
            return math.Div(x, y);
        }
    }
}
