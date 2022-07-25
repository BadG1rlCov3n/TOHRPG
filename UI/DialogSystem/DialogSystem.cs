using Godot;
using System.Collections.Generic;
using System.Linq;

public class DialogSystem : Control
{
    private RichTextLabel _textLabel;
    private IEnumerator<float> _coRoutine;
    private Queue<string> _dialogQueue = new Queue<string>();
    private float _coDelay = 0f;
    private float _requestedCoDelay = 0f;

    private Timer _dialogTimer = new Timer();

    [Export]
    public float letterInterval = 0.05f;

    public override void _Ready()
    {
        _textLabel = GetNode<RichTextLabel>("TextContainer/MainText");
        _dialogTimer.Connect("timeout", this, nameof(OnDialogTimerTimeout));
    }

    public void PlayDialog(string message)
    {
        _dialogQueue.Enqueue(message);
        Show();
    }

    public void SkipDialog()
    {
        if (!Visible)
        {
            return;
        }

        if (_textLabel.PercentVisible == 1.0f)
        {
            _textLabel.Text = string.Empty;
        }

        if (_textLabel.PercentVisible < 1.0f)
        {
            _textLabel.PercentVisible = 1.0f;
        }
    }

    public override void _Process(float delta)
    {
        if (_dialogQueue.Any() && _textLabel.Text.Empty())
        {
            _textLabel.Text = _dialogQueue.Dequeue();
            _textLabel.PercentVisible = 0;
            _dialogTimer.Start(letterInterval);
        }

        if (Visible && !_dialogQueue.Any())
        {
            Hide();
        }
    }

    private void OnDialogTimerTimeout()
    {
        _textLabel.PercentVisible += 0.1f;
    }
}
