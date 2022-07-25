using Godot;
using System.Collections.Generic;

public class Npc : Area2D
{
    private Sprite _button;

    public override void _Ready()
    {
        _button = GetNode<Sprite>("ButtonIcon");
        Connect("area_entered", this, nameof(OnAreaEntered));
        Connect("area_exited", this, nameof(OnAreaExited));
    }

    public List<string> GetDialogs()
    {
        return new List<string>{
            "This is the Boiling Isles. Every myth you humans have is caused by a little of our world leaking into yours.",
            "And goodbye"
        };
    }

    public void OnAreaEntered(Area2D area)
    {
        _button.Show();
    }

    public void OnAreaExited(Area2D area)
    {
        _button.Hide();
    }
}
