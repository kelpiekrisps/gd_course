using Godot;
using System;

public partial class Gem : Area2D
{
    [Export] float _speed = 100.0f;
    
    // custom signal
    [Signal] public delegate void OnScoredEventHandler();  // GD reqs: <SigName>EventHandler(info)
    [Signal] public delegate void OnGemOffScreenEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        // lightning means "event"
        AreaEntered += OnAreaEntered;  // add fn as event
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Position += new Vector2(0, _speed * (float)delta);  // fall
        if (CheckHitBottom() == true) {  // oops
            EmitSignal(SignalName.OnGemOffScreen);
            QueueFree();
        }
	}

    private bool CheckHitBottom() {
        Rect2 vpr = GetViewportRect();
        return (float)Position.Y >= (float)vpr.End.Y;  // if true, gem has hit bottom
    }

    private void OnAreaEntered(Area2D area) {
        GD.Print("Scored");
        EmitSignal(SignalName.OnScored);  // emits this signal for all receivers
        QueueFree();  // removes node from tree at end of frame
    }

}
