using Godot;
using System;
using System.Threading;

public partial class GameOver : Control
{
    [Export] private Label _gameOverLabel;
    [Export] private AnimationPlayer _anim;
    [Export] private Label _pressSpaceLabel;
    [Export] private Godot.Timer _instrTimer;
    [Export] private AudioStreamPlayer _gameOverSound;

	public override void _Ready()
	{
        SignalManager.Instance.OnPlaneCrash += OnGameOver;
        _gameOverLabel.Visible = false;
        _pressSpaceLabel.Visible = false;

        _instrTimer.Timeout += OnTimerTimeout;
	}

    public override void _ExitTree()
    {
        SignalManager.Instance.OnPlaneCrash -= OnGameOver;
    }

    public override void _Process(double delta)
	{
        bool quit_game = _pressSpaceLabel.Visible && Input.IsActionJustPressed("fly");
        
        if (quit_game) {
            GameManager.LoadMain();
        }
        
	}

    private void OnTimerTimeout() {
        _gameOverLabel.Visible = false;
        _pressSpaceLabel.Visible = true;
        _anim.Play("pressSpace");
    }

    private void OnGameOver() {

        _instrTimer.Start();
        _gameOverLabel.Visible = true;
        _gameOverSound.Play();
    }
}
