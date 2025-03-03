
using System;

class Character {
    public static int Count = 0;
    public string Name {get; protected set;}
    public string Race {get; private set;}

    // constructor
    public Character(string name, string race) {
        Name = name;
        Race = race;
        Count++;
    }

    public virtual void Introduce() {  // virtual: "I can be overriden"
        Console.WriteLine($"Hello! I am {Name}, a {Race}.");
    }

    public static void CountCharacters() {
        Console.WriteLine($"There are {Count} characters.");
    }

}