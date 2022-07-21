using Godot;
using System;


/// <summary>
/// TEST COMMIT
/// </summary>

public class Action_XyMovement : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    float
        m_fMoveForce = 10;

    [Export]
    Vector2
        m_v2VeloLimits = new Vector2(10,10);
    
    [Export]
    NodePath
        m_pathRigidbody2D;


    RigidBody2D
        m_Body;

    Vector2
        m_v2ForceApplied = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        m_Body = GetNode<RigidBody2D>(m_pathRigidbody2D);
       // m_Body.PhysicsMaterialOverride.Friction = 0.75f;
        m_Body.GravityScale = 0;
        SetProcess(true);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (IsProcessing() == false)
            return;

        Vector2 v2MoveDir = Vector2.Zero;
        Vector2 v2Velo = m_Body.LinearVelocity;

        v2MoveDir.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        v2MoveDir.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        

        v2MoveDir *= m_fMoveForce;

        m_Body.LinearVelocity = v2MoveDir;
    }

    public void _on_DashStarted() {
        SetProcess(false);
    }

    public void _on_DashEnded() {
        SetProcess(true);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
