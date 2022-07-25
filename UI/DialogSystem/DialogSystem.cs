using Godot;
using System.Collections.Generic;
using System.Linq;

public class DialogSystem : Control
{
    [Export]
    private float letterInterval = 0.05f;
    private RichTextLabel _textLabel;
    private Queue<string> _dialogQueue = new Queue<string>();
    private Timer _dialogTimer = new Timer();
    private float _percentStep = 0;

    private const float MaxPercent = 1.0f;

    public override void _Ready()
    {
        _textLabel = GetNode<RichTextLabel>("TextContainer/MainText");
        _dialogTimer.Connect("timeout", this, nameof(OnDialogTimerTimeout));
        AddChild(_dialogTimer);
    }

    public void Play(List<string> messages)
    {
        messages.ForEach(_ => _dialogQueue.Enqueue(_));
        Show();
    }

    public void Skip()
    {
        if (!Visible)
        {
            return;
        }

        if (_textLabel.PercentVisible == MaxPercent)
        {
            _textLabel.Text = string.Empty;
            if (!_dialogQueue.Any())
            {
                Hide();
            }
        }

        if (_textLabel.PercentVisible < MaxPercent)
        {
            _textLabel.PercentVisible = MaxPercent;
        }
    }

    public bool CurrentlyTalking()
    {
        return Visible;
    }

    public override void _Process(float delta)
    {
        if (_dialogQueue.Any() && _textLabel.Text.Empty())
        {
            _textLabel.Text = _dialogQueue.Dequeue();
            _textLabel.PercentVisible = 0;
            _percentStep = 1.0f / (float)_textLabel.Text.Length;
            _dialogTimer.Start(letterInterval);
        }
    }

    private void OnDialogTimerTimeout()
    {
        _textLabel.PercentVisible += _percentStep;

        if (_textLabel.PercentVisible == MaxPercent)
        {
            _dialogTimer.Stop();
        }
    }
}
