//Lazy initialization — Ленивая инициализация

//Достоинства:
//Инициализация выполняется только в тех случаях, когда она действительно необходима; ускоряется начальная инициализация.

//Недостатки:
//Невозможно явным образом задать порядок инициализации объектов; возникает задержка при первом обращении к объекту.


using System;
namespace FirstRealization
{
    public class LazyInitialization<T> where T : new()
    {
        protected LazyInitialization() { }
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    lock (typeof(T))
                        if (_instance == null) _instance = new T();
                return _instance;
            }
        }
    }
    public sealed class BigObject : LazyInitialization<BigObject>
    {
        public BigObject()
        {
            //Большая работа 
            System.Threading.Thread.Sleep(3000);
            System.Console.WriteLine("Экземпляр BigObject создан");
        }
        class Program
        {
            static void MainM(string[] args)
            {
                System.Console.WriteLine("Первое обращение к экземпляру  BigObject...");

                //создание объекта происходит только при этом обращении к объекту 
                System.Console.WriteLine(BigObject.Instance);

                System.Console.WriteLine("Второе обращение к экземпляру BigObject...");
                System.Console.WriteLine(BigObject.Instance);

                //окончание программы 
                System.Console.ReadLine();
            }
        }
    }
}

namespace SecondRealization
{
    public class BigObject
    {
        private static BigObject instance;

        private BigObject()
        {
            //Большая работа 
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Экземпляр BigObject создан");
        }
        public override string ToString()
        {
            return "Обращение к BigObject";
        }
        // Метод возвращает экземпляр BigObject, при этом он 
        // создаёт его, если тот ещё не существует
        public static BigObject GetInstance()
        {
            // для исключения возможности создания двух объектов 
            // при многопоточном приложении
            if (instance == null)
                lock (typeof(BigObject))
                    if (instance == null)
                        instance = new BigObject();
            return instance;
        }

        public static void MainM()
        {
            Console.WriteLine("Первое обращение к экземпляру BigObject...");

            //создание объекта происходит только при этом обращении к объекту
            Console.WriteLine(BigObject.GetInstance());
            Console.WriteLine("Второе обращение к экземпляру BigObject...");
            Console.WriteLine(BigObject.GetInstance());
            //окончание программы 
            Console.ReadLine();
        }
    }
}