using Godot;
using System;

public partial class ScoreManager : Node
{
    public static ScoreManager Instance { get; private set;}

    private int _score;
    private int _highScore;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Instance = this;
	}

    public static void ResetScore() {
        SetScore(0);
    }

    public static void SetScore(int value) {
        Instance._score = value;

        if (Instance._score > Instance._highScore) {
            Instance.newHighScore(Instance._score);
        }
        
        SignalManager.EmitOnScored();  // envoked whenever score changed
    }

    public static void IncrementScore() {
        SetScore(GetScore() + 1);
    }

    public static int GetScore() {
        return Instance._score;
    }

    public static int GetHighScore() {
        return Instance._highScore;
    }

    private void newHighScore(int value) {
        _highScore = value;
    }
}
