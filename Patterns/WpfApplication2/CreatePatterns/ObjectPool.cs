//Object pool — Объектный пул

//Переполнение:
//Если в пуле нет ни одного свободного объекта, возможна одна из трёх стратегий:
//Расширение пула.
//Отказ в создании объекта, аварийный останов.
//В случае многозадачной системы, можно подождать, пока один из объектов не освободится.

//Ловушки:
//После того, как объект возвращён, он должен вернуться в состояние, 
//пригодное для дальнейшего использования. Если объекты после возвращения в пул 
//оказываются в неправильном или неопределённом состоянии, такая конструкция 
//называется объектной клоакой (англ. object cesspool).

//Повторное использование объектов также может привести к утечке информации.
//Если в объекте есть секретные данные (например, номер кредитной карты), 
//после освобождения объекта эту информацию надо затереть.


using System;
using System.Collections;
using System.Threading;
using Digital_Patterns.Creational.Object_Pool.Soft;

// Пример 1
namespace Digital_Patterns.Creational.Object_Pool.Soft
{
    /// <summary>
    /// Интерфейс для использования шаблона "Object Pool" <see cref="Object_Pool"/> /// </summary>
    /// <typeparam name="T"></typeparam> 
    public interface ICreation<T>
    {
        /// <summary>
        /// Возвращает вновь созданный объект /// </summary>
        /// <returns></returns> 
        T Create();
    }
}

// Пример 2 using System;
namespace Digital_Patterns.Creational.Object_Pool.Soft
{
    /// <summary>
    /// Реализация пула объектов, использующий "мягкие" ссылки /// </summary>
    /// <typeparam name="T"></typeparam> 
    public class ObjectPool<T> where T : class
    {
        /// <summary>
        /// Объект синхронизации /// </summary>
        private Semaphore semaphore;

        /// <summary>
        /// Коллекция содержит управляемые объекты /// </summary>
        private ArrayList pool;

        /// <summary>
        /// Ссылка на объект, которому делегируется ответственность /// за создание объектов пула
        /// </summary>
        private ICreation<T> creator;

        /// <summary>
        /// Количество объектов, существующих в данный момент /// </summary>
        private int instanceCount;

        /// <summary>
        /// Максимальное количество управляемых пулом объектов /// </summary>
        private int maxInstances;

        /// <summary>
        /// Создание пула объектов /// </summary>
        /// <param name="creator">Объект, которому пул будет делегировать ответственность /// за создание управляемых им объектов</param>
        public ObjectPool(ICreation<T> creator) : this(creator, int.MaxValue) { }


        /// <summary>
        /// Создание пула объектов /// </summary>
        /// <param name="creator">Объект, которому пул будет делегировать ответственность /// за создание управляемых им объектов</param>
        /// <param name="maxInstances">Максимальное количество экземпляров класс, /// которым пул разрешает существовать одновременно
        /// </param>
        public ObjectPool(ICreation<T> creator, int maxInstances)
        {
            this.creator = creator;
            this.instanceCount = 0; this.maxInstances = maxInstances; this.pool = new ArrayList();
            this.semaphore = new Semaphore(0, this.maxInstances);
        }
        /// <summary>
        /// Возвращает количество объектов в пуле, ожидающих повторного /// использования. Реальное количество может быть меньше
        /// этого значения, поскольку возвращаемая
        /// величина - это количество "мягких" ссылок в пуле. /// </summary>
        public int Size
        {
            get
            {
                lock (pool)
                    return pool.Count;
            }
        }
        /// <summary>
        /// Возвращает количество управляемых пулом объектов, /// существующих в данный момент
        /// </summary>
        public int InstanceCount { get { return instanceCount; } }

        /// <summary>
        /// Получить или задать максимальное количество управляемых пулом /// объектов, которым пул разрешает существовать одновременно. /// </summary>
        public int MaxInstances
        {
            get { return maxInstances; }
            set { maxInstances = value; }
        }

        /// <summary>
        /// Возвращает из пула объект. При пустом пуле будет создан /// объект, если количество управляемых пулом объектов не /// больше или равно значению, возвращаемому методом
        /// <see cref="ObjectPoolT.MaxInstances"/>. Если количество управляемых пулом /// объектов превышает это значение, то данный метод возварщает null
        /// </summary>
        /// <returns></returns> 
        public T GetObject()
        {
            lock (pool)
            {
                T thisObject = RemoveObject();
                if (thisObject != null)
                    return thisObject;
            }
            if (InstanceCount < MaxInstances)
                return CreateObject();

            return null;
        }
        /// <summary>
        /// Возвращает из пула объект. При пустом пуле будет создан /// объект, если количество управляемых пулом объектов не

        /// больше или равно значению, возвращаемому методом
        /// <see cref="ObjectPoolT.MaxInstances"/>. Если количество управляемых пулом /// объектов превышает это значение, то данный метод будет ждать до тех
        /// пор, пока какой-нибудь объект не станет доступным для /// повторного использования.
        /// </summary>
        /// <returns></returns> 
        public T WaitForObject()
        {
            lock (pool)
            {
                T thisObject = RemoveObject();
                if (thisObject != null)
                    return thisObject;
            }

            if (InstanceCount < MaxInstances)
                return CreateObject();
            semaphore.WaitOne();
            return WaitForObject();
        }
        /// <summary>
        /// Удаляет объект из коллекции пула и возвращает его /// </summary>
        /// <returns></returns> 
        private T RemoveObject()
        {
            while (pool.Count > 0)
            {
                var refThis = (WeakReference)pool[pool.Count - 1];
                pool.RemoveAt(pool.Count - 1);
                var thisObject = (T)refThis.Target;
                if (thisObject != null)
                    return thisObject; instanceCount--;
            }
            return null;
        }
        /// <summary>
        /// Создать объект, управляемый этим пулом /// </summary>
        /// <returns></returns> 
        private T CreateObject()
        {
            T newObject = creator.Create();
            instanceCount++;
            return newObject;
        }
        /// <summary>
        /// Освобождает объект, помещая его в пул для /// повторного использования
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NullReferenceException"></exception> 
        public void Release(T obj)
        {
            if (obj == null)
                throw new NullReferenceException();
            lock (pool)
            {
                var refThis = new WeakReference(obj);
                pool.Add(refThis);
                semaphore.Release();
            }
        }
    }
}

// Пример 3
namespace Digital_Patterns.Creational.Object_Pool.Soft
{
    public class Reusable
    {
        public Object[] Objs { get; protected set; }

        public Reusable(params Object[] objs)
        {
            this.Objs = objs;
        }
    }

    public class Creator : ICreation<Reusable>
    {
        private static int iD = 0;

        public Reusable Create()
        {
            ++iD;
            return new Reusable(iD);
        }
    }

    public class ReusablePool : ObjectPool<Reusable>
    {
        public ReusablePool() : base(new Creator(), 2) { }
    }
}

// Пример 4 using System;
namespace Digital_Patterns
{
    class Program
    {
        static void MainM(string[] args)
        {
            Console.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name); var reusablePool = new ReusablePool();

            var thrd1 = new Thread(Run); var thrd2 = new Thread(Run);
            var thisObject1 = reusablePool.GetObject(); var thisObject2 = reusablePool.GetObject(); thrd1.Start(reusablePool); thrd2.Start(reusablePool); ViewObject(thisObject1); ViewObject(thisObject2); Thread.Sleep(2000);

            reusablePool.Release(thisObject1); Thread.Sleep(2000); reusablePool.Release(thisObject2);

            Console.ReadKey();
        }
        private static void Run(Object obj)
        {
            Console.WriteLine("\t" + System.Reflection.MethodInfo.GetCurrentMethod().Name); var reusablePool = (ReusablePool)obj;
            Console.WriteLine("\tstart wait");
            var thisObject1 = reusablePool.WaitForObject(); ViewObject(thisObject1); Console.WriteLine("\tend wait"); reusablePool.Release(thisObject1);
        }
        private static void ViewObject(Reusable thisObject)
        {
            foreach (var obj in thisObject.Objs)
                Console.Write(obj.ToString() + @" ");
            Console.WriteLine();
        }
    }
}