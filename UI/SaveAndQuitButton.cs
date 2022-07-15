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
        GetTree().Quit();
    }
}
