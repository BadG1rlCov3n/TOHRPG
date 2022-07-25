using Godot;
using System;

public class DialogSystemTestScene : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var dialog = GetNode<DialogSystem>("DialogSystem");
        dialog.PlayDialog("This is the Boiling Isles. Every myth you humans have is caused by a little of our world leaking into yours.");
        dialog.PlayDialog("And goodbye");
    }
}
