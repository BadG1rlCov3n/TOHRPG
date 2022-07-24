using Godot;
using System;

/// <summary>
/// The signal handler.
/// Should only have notification logic and signal declaration.
/// All signals declaration must be done here.
/// </summary>
public class SignalHandler : Node
{
    [Signal]
    public delegate void OnSaveSignal();

    private SaveData _saveData;

    /// <inheritdoc/>
    public override void _Ready()
    {
        Autoload.Setup(GetTree().Root);
        _saveData = Autoload.Get<SaveData>();
    }

    /// <inheritdoc/>
    public override void _Notification(int what)
    {
        // If we asked to safely quit, save, THEN quit.
        if (what == MainLoop.NotificationWmQuitRequest)
        {
            _saveData.Save();
            EmitSignal(nameof(SignalHandler.OnSaveSignal));
        }
    }
}
