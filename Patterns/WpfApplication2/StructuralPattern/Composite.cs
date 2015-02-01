//Composite — Компоновщик

//Цель:Паттерн определяет иерархию классов, которые одновременно 
//могут состоять из примитивных и сложных объектов, упрощает 
//архитектуру клиента, делает процесс добавления новых видов объекта более простым.



    using System;
using System.Collections;
class MainApp
{
    static void Main()
    {
        // Create a tree structure
        Component root = new Composite("root");
        root.Add(new Leaf("Leaf A")); root.Add(new Leaf("Leaf B"));
        Component comp = new Composite("Composite X");
        comp.Add(new Leaf("Leaf XA")); comp.Add(new Leaf("Leaf XB")); root.Add(comp);
        root.Add(new Leaf("Leaf C"));
        // Add and remove a leaf
        Leaf leaf = new Leaf("Leaf D"); root.Add(leaf); root.Remove(leaf);
        // Recursively display tree 
        root.Display(1);
        // Wait for user 
        Console.Read();
    }
}

/// объявляет интерфейс для компонуемых объектов;
/// предоставляет подходящую реализацию операций по умолчанию, общую для всех классов;
/// объявляет интерфейс для доступа к потомкам и управлению ими;
/// определяет интерфейс доступа к родителю компонента в рекурсивной структуре и при необходимости реализует его. Описанная возможность необязательна; 
abstract class Component
{
    protected string name;

    public Component(string name)
    {
        this.name = name;
    }

    public abstract void Add(Component c);
    public abstract void Remove(Component c);
    public abstract void Display(int depth);
}

/// Composite - составной объект 
///определяет поведеление компонентов, у которых есть потомки;  
///хранит компоненты-потомоки;
/// реализует относящиеся к управлению потомками 
/// операции и интерфейсе  класса Component
class Composite : Component
{
    private ArrayList children = new ArrayList();
    public Composite(string name) : base(name) { }
    public override void Add(Component component)
    {
        children.Add(component);
    }
    public override void Remove(Component component)
    {
        children.Remove(component);
    }
    public override void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + name);
        // Recursively display child nodes 
        foreach (Component component in children)
            component.Display(depth + 2);
    }
}
/// представляет листовой узел композиции и не имеет потомков;  
/// определяет поведение примитивных объектов в композиции;
class Leaf : Component
{
    public Leaf(string name) : base(name) { }
    public override void Add(Component c)
    {
        Console.WriteLine("Cannot add to a leaf");
    }
    public override void Remove(Component c)
    {
        Console.WriteLine("Cannot remove from a leaf");
    }
    public override void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + name);
    }
}