using Godot;
using System;

/// <summary>
/// The pause menu.
/// Will work even if paused.
/// </summary>
public class PauseMenu : Control
{
    /// <inheritdoc/>
    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            Visible = !Visible;
            GetTree().Paused = !GetTree().Paused;
        }
    }
}
