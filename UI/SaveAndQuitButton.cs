using Godot;
using System;

/// <summary>
/// The save and quit button.
/// </summary>
public class SaveAndQuitButton : SelfConnectingButton
{
    /// <inheritdoc/>
    protected override void OnButtonPressed()
    {
        // Safely request quitting the game (this notification will be caught by the SaveData.cs)
        GetTree().Notification(MainLoop.NotificationWmQuitRequest);
    }
}
