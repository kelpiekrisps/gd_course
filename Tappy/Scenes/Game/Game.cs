using Godot;
using System;

public partial class Game : Node2D
{
    [Export] private Marker2D _spawnUpper;
    [Export] private Marker2D _spawnLower;
    [Export] private Timer _spawnTimer;
    [Export] private PackedScene _pipeScene;
    [Export] private Node2D _pipesHolder;
    [Export] private Plane _plane;

    private bool GAME_OVER = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{   
        // spawn initial pipe
        SpawnPipe();

        // spawn pipes on timer
        _spawnTimer.Timeout += OnSpawnTimer;
        _plane.OnPlaneCrash += GameOver; // custom signal handling
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        // check if user wants to end the game/return to main scene
        bool quit_game = Input.IsActionJustPressed("quit") || (GAME_OVER && Input.IsActionJustPressed("fly"));
        
        if (quit_game) {
            GameManager.LoadMain();
        }
	}

    public float GetSpawnY() {
        // recall Y value increases as go down screen
        return (float)GD.RandRange(_spawnUpper.Position.Y, _spawnLower.Position.Y);
    }

    private void SpawnPipe() {
        Pipes pipe = (Pipes)_pipeScene.Instantiate();
        _pipesHolder.AddChild(pipe);  // note all pipes positioning relative to _pipesHolder

        float YCoord = GetSpawnY();
        pipe.Position = new Vector2(_spawnLower.Position.X, YCoord);
    }

    private void StopPipes() {
        _spawnTimer.Stop();
        foreach (Node node in _pipesHolder.GetChildren()) {
            node.SetProcess(false);  // stop all movement, user input, etc.
        }
    }

    private void OnSpawnTimer() {
        SpawnPipe();
    }

    private void GameOver() {
        GD.Print("Plane crashed!");
        StopPipes();
        GAME_OVER = true;
    }
}
