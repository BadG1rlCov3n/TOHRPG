using Godot;
using System;
using System.Collections.Generic;

public class SimpleDialog : Resource
{
    [Export]
    public List<DialogFragment> Fragments { get; set; }
}
