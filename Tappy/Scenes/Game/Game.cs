using Godot;
using System;

public partial class Game : Node2D
{
    [Export] private Marker2D _spawnUpper;
    [Export] private Marker2D _spawnLower;
    [Export] private Timer _spawnTimer;
    [Export] private PackedScene _pipeScene;
    [Export] private Node2D _pipesHolder;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{   
        // spawn initial pipe
        SpawnPipe();

        // spawn pipes on timer
        _spawnTimer.Timeout += OnSpawnTimer;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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

    private void OnSpawnTimer() {
        SpawnPipe();
    }
}
