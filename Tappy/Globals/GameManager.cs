using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set;}

    private PackedScene _gameScene = GD.Load<PackedScene>("res://Scenes/Game/Game.tscn");
    private PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/Main/main.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Instance = this;  // singleton, one instance
	}

    public static  void LoadMain() {
        // by making static (class level)
        // need to have an instance to call GetTree,
        // to which we use Instance
        Instance.GetTree().ChangeSceneToPacked(Instance._mainScene);
    }

    public static void LoadGame() {
        Instance.GetTree().ChangeSceneToPacked(Instance._gameScene);
    }
}
