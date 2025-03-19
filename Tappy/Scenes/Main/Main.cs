using Godot;
using System;

public partial class Main : Control
{
    [Export] private Label _highScoreValue;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        _highScoreValue.Text = $"{ScoreManager.GetHighScore():0000}";

        // change to new scene
        if (Input.IsActionJustPressed("fly")) {
            GameManager.LoadGame();
        }
	}
}
