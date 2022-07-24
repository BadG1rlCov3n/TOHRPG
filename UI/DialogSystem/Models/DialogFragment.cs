using Godot;

public class DialogFragment : Resource
{
    [Export]
    public string Text { get; set; }
    
    [Export]
    public string Actor { get; set; }
    
    [Export]
    public string Recipient { get; set; }
}
