using System;

// example singleton

class Mordor {
    private static Mordor instance;
    public int NumOrcs{get; set;} = 0;

    private Mordor() {
        Console.WriteLine("Mordor instance created.");
    }

    public static Mordor Instance {
        get {
            if (instance == null) {
                instance = new Mordor();
            }
            return instance;
        }
    }

    public void sayHello() {
        Console.WriteLine($"Hello from the {NumOrcs} orcs.");
    }

    public void AddOrc() {
        NumOrcs++;
    }
}