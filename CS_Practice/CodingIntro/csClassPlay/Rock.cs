class Rock {
    private string _name;
    private int _weight;

    public Rock(string name, int weight)  // ctor + tab shortcut
    {
        _name = name;
        _weight = weight;
    }

    // everything inherets from System.Object
    // overriding one of these functions
    public override string ToString() {
        return $"Name: {_name}, Weight: {_weight}";
    } 
// ToString override means printing an instance of the class outputs that
// the default output string is namespace.className
}