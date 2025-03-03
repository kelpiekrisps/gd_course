using System;

class Hobbit : Character {
    // constructor invokes parent constructor with 
    // hardcoded race of "Hobbit"
    public Hobbit(string name) : base(name, "Hobbit") {}

    public void SneakAround() {
        Console.WriteLine($"{Name} is sneaking around.");
    }

    // below does not work because the parent has a PRIVATE setter
     public void ChangeName(string newName) {
        Console.WriteLine($"{Name} is now {newName}.");
         Name = newName;
     }
}