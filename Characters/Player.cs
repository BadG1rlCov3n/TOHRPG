using Godot;
using System;

public class Player : Area2D
{
    [Export]
    private int _speed = 15;

    private DialogSystem _dialogSystem;
    private Npc _npcToTalkTo;

    public override void _Ready()
    {
        _dialogSystem = GetNode<DialogSystem>("Camera2D/CanvasLayer/DialogSystem");
        Connect("area_entered", this, nameof(OnAreaEntered));
        Connect("area_exited", this, nameof(OnAreaExited));
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_dialogSystem.CurrentlyTalking())
        {
            return;
        }

        var direction = Vector2.Zero;
        direction.x += Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        direction.y += Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        direction *= _speed;

        GlobalPosition += direction;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            if (_npcToTalkTo != null)
            {
                HandleDialog();
                return;
            }
        }
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is Npc npc)
        {
            _npcToTalkTo = npc;
        }
    }

    public void OnAreaExited(Area2D area)
    {
        if (area is Npc npc)
        {
            _npcToTalkTo = null;
        }
    }

    private void HandleDialog()
    {
        if (_dialogSystem.CurrentlyTalking())
        {
            _dialogSystem.Skip();
        }
        else
        {
            _dialogSystem.Play(_npcToTalkTo.GetDialogs());
        }
    }
}
