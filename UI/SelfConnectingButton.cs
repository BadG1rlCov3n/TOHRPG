using Godot;

/// <summary>
/// The class for the self connecting button.
/// It allows us to only have one class with the connection logic, but multiple instanciation of it.
/// </summary>
public class SelfConnectingButton : Button
{
    /// <inheritdoc/>
    public override void _Ready()
    {
        Connect("pressed", this, nameof(OnButtonPressed));
    }

    /// <summary>
    /// What needs to be done when this button is pressed.
    /// </summary>
    protected virtual void OnButtonPressed()
    {
        // Do not instanciate anything here.
        // Instead, inherit this class, then override this method. 
        // The button will automatically connect this signal, and no more code or UI management is needed.
    }
}