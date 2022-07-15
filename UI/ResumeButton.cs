using Godot;
using System;

/// <summary>
/// The resume game button.
/// </summary>
public class ResumeButton : SelfConnectingButton
{
    /// <inheritdoc/>
    protected override void OnButtonPressed()
    {
        // Taking the grandparent here is faster and less cumbersome to read than doing some signals shenanigans...
        GetParent<VBoxContainer>().GetParent<PauseMenu>().Hide();
        GetTree().Paused = false;
    }
}
