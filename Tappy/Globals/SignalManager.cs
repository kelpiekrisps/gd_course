using Godot;
using System;

public partial class SignalManager : Node
{
    public static SignalManager Instance {get; private set;}

    // signals
    [Signal] public delegate void OnPlaneCrashEventHandler();
    [Signal] public delegate void OnScoredEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public static void EmitOnScored() {
        Instance.EmitSignal(SignalName.OnScored);
    }

    public static void EmitOnPlaneDied() {
        Instance.EmitSignal(SignalName.OnPlaneCrash);
    }
}
