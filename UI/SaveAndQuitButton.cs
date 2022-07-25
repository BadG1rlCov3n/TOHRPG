using Godot;

/// <summary>
/// The save and quit button.
/// </summary>
public class SaveAndQuitButton : SelfConnectingButton
{
    /// <inheritdoc/>
    public override void _Ready()
    {
        var signalHandler = Autoload.Get<SignalHandler>();
        signalHandler.Connect(nameof(SignalHandler.OnSaveSignal), this, nameof(OnSaveCompleted));
        base._Ready(); //forced to leave that here, we must override _ready to keep node behaviour, but we also need to call our parent's ready because it connects the signal.
    }

    /// <inheritdoc/>
    protected override void OnButtonPressed()
    {
        // Safely request quitting the game (this notification will be caught by the SaveData.cs)
        GetTree().Notification(MainLoop.NotificationWmQuitRequest);
    }

    private void OnSaveCompleted()
    {
        GetTree().Quit();
    }
}
