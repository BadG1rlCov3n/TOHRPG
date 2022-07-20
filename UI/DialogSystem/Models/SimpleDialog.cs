using Godot;
using System;
using System.Collections.Generic;

public class SimpleDialog : Resource
{
    [Export]
    public List<DialogFragment> Fragments { get; set; }
}

public class DialogFragment : Resource
{
    [Export]
    public string Text { get; set; }
    public string Actor { get; set; }
}
