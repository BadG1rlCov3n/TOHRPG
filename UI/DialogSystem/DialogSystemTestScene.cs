using Godot;
using System;

public class DialogSystemTestScene : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var dialog = GetNode<DialogSystem>("DialogSystem");
    }
}
