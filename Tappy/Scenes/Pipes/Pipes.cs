using Godot;
using System;

public partial class Pipes : Node2D
{
    [Export] float _speed = 120.0f;
    [Export] private VisibleOnScreenNotifier2D _visibleOnScreenNotifier;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _visibleOnScreenNotifier.ScreenExited += OnScreenExited;  // attach fn to signal emission
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
}
