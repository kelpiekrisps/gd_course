using Godot;
using System;
using System.ComponentModel;

public partial class Game : Node2D
{
    const double GEM_MARGIN = 50.0;
    private static readonly AudioStream _explodeSound = GD.Load<AudioStream>("res://assets/explode.wav");

    [Export] private PackedScene _gemScene;  // PackedScene == .tscn
    
    [Export] private Timer _spawnTimer;
    [Export] private Label _scoreLabel;
    [Export] private AudioStreamPlayer _bgMusic;
    [Export] private AudioStreamPlayer2D _effects;

    private int _score = 0;
    
	public override void _Ready()
	{
        SpawnGem();
        _scoreLabel.Text = $"{_score:0000}";
        _spawnTimer.Timeout += OnSpawnTimer;
    }

	public override void _Process(double delta)
	{
	}

    private void GameOver() {
        GD.Print("Game Over!");
        foreach (Node node in GetChildren()) {
            node.SetProcess(false);  // stop all movement, user input, etc.
        }
        _spawnTimer.Stop();
        _bgMusic.Stop();

        _effects.Stop();  // any other sound it may be playing
        _effects.Stream = _explodeSound;
        _effects.Play();
    }

    private void SpawnGem() {
        Rect2 vpr = GetViewportRect();

        Gem gem = (Gem)_gemScene.Instantiate();
        AddChild(gem);

        // set position after adding child to avoid funky positioning offset
        float XCoord = (float)GD.RandRange(vpr.Position.X + GEM_MARGIN, vpr.End.X - GEM_MARGIN);
        gem.Position = new Vector2(XCoord, -100);

        // each instantiated gem will send signal to our OnGemScored fn
        gem.OnScored += OnGemScored;  
        gem.OnGemOffScreen += OnGemOffScreen;
    }


// events
    private void OnGemScored() {  // behavior upon receiving signal
        //GD.Print("OnScored Received");
        _effects.Play();
        _score += 1;

        // keep score display in "0000" type formatting
        /*
        int numDigits = _score / 10;
        string scoreDisplay = "";
        for (int scoreIdx = 0; scoreIdx < (3-numDigits); scoreIdx++) {
            scoreDisplay += "0";
        }
        scoreDisplay += _score.ToString();

        // update label
        _scoreLabel.Text = scoreDisplay; */
        _scoreLabel.Text = $"{_score:0000}";

    }

    private void OnGemOffScreen() {
        GameOver();
    }

    private void OnSpawnTimer() {
        SpawnGem();
    }
}


/*
REFERENCE NOTES

    //[Export] private Gem _gem;  // avoid hardcoding - assign var in Godot
    non-hardcoded version of gem = GetNode.
        [Export] private NodePath _gemPath;
        private Gem _gem; 


    	public override void _Ready()
	{
        //Gem gem = GetNode<Gem>("Gem");  // type varname = GetNode<Type>("NodeNameInGodotScene")
        // non-hardcoded version of above.
        //    _gem = GetNode<Gem>(_gemPath); 
        //
        
        // set signal to this scene's handling?
        //_gem.OnScored += OnGemScored;  // Teacher prefers both named 'OnScored', diff here for understanding
        SpawnGem();
    }
*/