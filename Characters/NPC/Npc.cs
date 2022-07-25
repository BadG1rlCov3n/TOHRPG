using Godot;
using System;

public class Npc : Area2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("on_area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D body)
    {

    }
}
