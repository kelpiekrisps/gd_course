
using System;

class Monster : Character {
    public Monster(string name) : base(name, "Monster"){}

    public override void Introduce() {  // keyword override explicitly signals overriding parent fn
        Console.WriteLine($"I am a fearsome monster! My name is {Name}");
    }
}