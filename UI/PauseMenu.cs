using Godot;
using System;

/// <summary>
/// The pause menu.
/// Will work even if paused.
/// </summary>
public class PauseMenu : Control
{
    private ResumeButton _resumeButton;

    public override void _Ready()
    {
        _resumeButton = GetNode<ResumeButton>("VBoxContainer/ResumeButton");
    }

    /// <inheritdoc/>
    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("ui_pause"))
        {
            Visible = !Visible;
            GetTree().Paused = !GetTree().Paused;
            if (Visible)
            {
                _resumeButton.GrabFocus();
            }
        }
    }
}
