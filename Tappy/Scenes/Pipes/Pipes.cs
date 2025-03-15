using Godot;
using System;

public partial class Pipes : Node2D
{
    [Export] float _speed = 120.0f;
    [Export] private VisibleOnScreenNotifier2D _visibleOnScreenNotifier;
    [Export] private Area2D _upperPipe;
    [Export] private Area2D _lowerPipe;
    [Export] private Area2D _laser;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // when pipes leave screen call fn to delete
        _visibleOnScreenNotifier.ScreenExited += OnScreenExited;  // attach fn to signal emission
        _lowerPipe.BodyEntered += OnPipeBodyEntered;
        _upperPipe.BodyEntered += OnPipeBodyEntered;
        _laser.BodyEntered += OnLaserEntered;

        // when plane crashes, have the signal react here by stopping the instance of a pipe
        SignalManager.Instance.OnPlaneCrash += OnPlaneCrash;
    }

    // necessary as SignalManager invoked
    public override void _ExitTree()
    {
        SignalManager.Instance.OnPlaneCrash -= OnPlaneCrash;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        Position -= new Vector2(_speed * (float)delta, 0);
	}

    // once left screen, remove pipes from scene
    private void OnScreenExited() {
        QueueFree();
    }

    private void OnPipeBodyEntered(Node2D body) {
        if (body is Plane) {
            (body as Plane).Die();  // cast body as plane type to call Die
        }
    }

    private void OnLaserEntered(Node2D body) {
        if (body is Plane) {
            GD.Print(ScoreManager.GetScore());
            ScoreManager.IncrementScore();
        }
    }

    private void OnPlaneCrash() {
        SetProcess(false);
    }
}
