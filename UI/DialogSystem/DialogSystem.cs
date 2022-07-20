using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DialogSystem : CanvasLayer
{
    private RichTextLabel _textLabel;
    private IEnumerator<float> _coRoutine;
    private Queue<string> _dialogQueue = new Queue<string>();

    
    
    private float _coDelay = 0f;
    private float _requestedCoDelay = 0f;
    
    
    

    [Export]
    public float letterIntervalS = 0.05f;
    


    public override void _Ready()
    {
        _textLabel = GetNode<RichTextLabel>("BackgroundPanel/MainText");
        _coRoutine = InnerProcess().GetEnumerator();
    }

    public void PlayDialog(string message)
    {    
        _dialogQueue.Enqueue(message);
    }

    // The actual UI Co-routine, return value indicates desired delay between actions in seconds
    private IEnumerable<float> InnerProcess()
    {
        while(true) {
            if (_dialogQueue.Count != 0)
            {
                var nextMessage = _dialogQueue.Dequeue();
                var currentMessage = "";

                for(int i = 0; i <= nextMessage.Length; i++) {
                    currentMessage = nextMessage.Substr(0,i);
                    _textLabel.Text = currentMessage;
                    yield return letterIntervalS;
                }

                yield return 1;

            }
            yield return 0; // If nothing else is ready, we just yield and reset
        }
    } 


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // Basically just drives the co-routine
    public override void _Process(float delta)
    {
        _coDelay += delta;

        if (_coDelay >= _requestedCoDelay){
            _coRoutine.MoveNext();
            _requestedCoDelay = _coRoutine.Current;
            _coDelay = 0;
        }
    }
}
