using System;

class Utils {
    static int numCalls = 0;

    // static functions: you do not need an instance to call
    public static void SayStuff() {
        numCalls++;
        Console.WriteLine($"Utils.SayStuff: Stuff {numCalls} times");
    }
}