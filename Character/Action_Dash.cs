using Godot;
using System;

public class Action_Dash : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    NodePath
        m_Path_Rigidbody2D;

    [Signal]
    delegate void sig_DashStarted();

    [Signal]
    delegate void sig_DashEnded();


    [Export]
    int
        m_iDashSpeed = 1000;


    [Export]
    ulong
        m_fDashTimeEnd,
        m_fDashDuration = 250;

    RigidBody2D
        m_Body;

    bool
        m_bIsdashing = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        m_Body = GetNode<RigidBody2D>(m_Path_Rigidbody2D);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if (m_bIsdashing == true && OS.GetTicksMsec() > m_fDashTimeEnd)
        {
            m_bIsdashing = false;
            EmitSignal(nameof(sig_DashEnded));
            return;
        }

        if (Input.IsActionJustPressed("ui_dash") == false)
            return;

        if (OS.GetTicksMsec() < m_fDashTimeEnd)
            return;

       
        Vector2 v2Dir = Vector2.Zero;

        if (Input.IsActionPressed("ui_left"))
            v2Dir.x -= 1;
        if (Input.IsActionPressed("ui_right"))
            v2Dir.x += 1;
        if (Input.IsActionPressed("ui_up"))
            v2Dir.y -= 1;
        if (Input.IsActionPressed("ui_down"))
            v2Dir.y += 1;

        if (v2Dir == Vector2.Zero)
            return;

        EmitSignal(nameof(sig_DashStarted));
        m_bIsdashing = true;
        m_fDashTimeEnd = OS.GetTicksMsec() + m_fDashDuration;
        m_Body.LinearVelocity = v2Dir * m_iDashSpeed;

        GD.Print("Dashing!");

    }
}
