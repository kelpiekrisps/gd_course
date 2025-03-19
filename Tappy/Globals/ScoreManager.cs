using Godot;
using System;

public partial class ScoreManager : Node
{
    public static ScoreManager Instance { get; private set;}

    private uint _score = 0;
    private uint _highScore = 0;

    private const string SCORE_FILE = "user://tappy.save";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Instance = this;
        LoadScoreFromFile();
	}

    private void SaveScoreToFile() {
        // open file for writing, will not append
        using FileAccess file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Write);

        if (file != null) {  // open succesful
            file.Store32(_highScore);
        }
    }

    private void LoadScoreFromFile() {
        // open file for writing, will not append
        using FileAccess file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Read);

        if (file != null) {  // open succesful
            _highScore = file.Get32();
        }
    }

    public static void ResetScore() {
        SetScore(0);
    }

    public static void SetScore(uint value) {
        Instance._score = value;

        if (Instance._score > Instance._highScore) {
            Instance.newHighScore(Instance._score);
        }

        SignalManager.EmitOnScored();  // envoked whenever score changed
    }

    public static void IncrementScore() {
        SetScore(GetScore() + 1);
    }

    public static uint GetScore() {
        return Instance._score;
    }

    public static uint GetHighScore() {
        return Instance._highScore;
    }

    private void newHighScore(uint value) {
        GD.Print("New High Score!");
        _highScore = value;

        SaveScoreToFile();
    }
}
