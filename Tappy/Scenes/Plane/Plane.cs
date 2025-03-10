using Godot;
using System;

public partial class Plane : CharacterBody2D
{
    // consts
    const float GRAVITY = 800.0f;
    const float POWER = -450.0f;
    
    // exports
    [Export] private NodePath _animplayPath;
    private AnimationPlayer _animplay;
    [Export] private AnimatedSprite2D _planeSprite;

    // signals
    [Signal] public delegate void OnPlaneCrashEventHandler();


    // functions ********************************************************************************
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _animplay = GetNode<AnimationPlayer>(_animplayPath);
    }

	// move_and_slide etc. should be called from physics process, not process
	public override void _PhysicsProcess(double delta)
	{
        Vector2 velocityLocal = Velocity;

        // falling
        velocityLocal.Y += GRAVITY * (float)delta;

        if (Input.IsActionJustPressed("fly")) {
            velocityLocal.Y = POWER;  // set constant instead of modifying based on time passage
            _animplay.Play("power");
        } 

        Velocity = velocityLocal;
        MoveAndSlide();

        if (IsOnFloor()) {
            Die();  // only called once since setphysicsprocess(false)
        }
	}

    public void Die() {
        SetPhysicsProcess(false);  //no longer have physics process, no flying
        _planeSprite.Stop();  // halt animations
        EmitSignal(SignalName.OnPlaneCrash);
    }
}
