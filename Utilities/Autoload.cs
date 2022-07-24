using Godot;
using System;

/// <summary>
/// The helper class to grab godot autoload.
/// </summary>
public static class Autoload
{
    private static Node _root;

    /// <summary>
    /// Setup this helper class by providing the root node.
    /// Later on, no node will be needed as this class now holds a constant reference towards the root node.
    /// </summary>
    /// <param name="root">The root node.</param>
    public static void Setup(Node root)
    {
        _root = root;
    }

    /// <summary>
    /// Smartly grabs the autoload.
    /// </summary>
    /// <typeparam name="T">The type of the autoload we're looking for. The name MUST match the type.</typeparam>
    /// <returns>The autoload.</returns>
    public static T Get<T>() where T : Node
    {
        return _root.GetNode<T>(typeof(T).Name);
    }
}
