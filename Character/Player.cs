using Godot;
using System;

public class Player : Area2D
{
    [Export]
    public int Speed = 15;

    public override void _Ready()
    {
    }

    public override void _PhysicsProcess(float delta)
    {
        var direction = Vector2.Zero;
        direction.x += Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        direction.y += Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        direction *= Speed;

        GlobalPosition += direction;
    }
}
