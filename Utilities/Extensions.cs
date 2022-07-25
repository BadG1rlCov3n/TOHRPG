using Godot;
using System;
using System.Linq;
using Godot.Collections;
using System.Collections.Generic;

public static class Extensions
{
    /// <summary>
    /// Load a scene smartly. Takes a path to a node to instanciate.
    /// </summary>
    /// <typeparam name="T">The type to return. T must inherit node.</typeparam>
    /// <param name="path">The path to load.</param>
    /// <returns>The instanciated node.</returns>
    public static T SmartLoader<T>(string path) where T : Node
    {
        try
        {
            var packedScene = ResourceLoader.Load<PackedScene>(path);
            var instance = (T)packedScene.Instance();
            return instance;
        }
        catch (Exception exception)
        {
            GD.Print($"Exception while smart loading a scene : {exception.Message}");
            throw;
        }
    }

    /// <summary>
    /// Search for children of Type T and return them in a new Array.
    /// </summary>
    /// <typeparam name="T">The type to return. T must inherit node.</typeparam>
    /// <param name="node">The node from which we will start searching.</param>
    /// <returns>A list containing all children of type {T}.</returns>
    public static List<T> FindChildrenOfType<T>(this Node node) where T : Node
    {
        var children = node.GetChildren();
        var nodes = new Array<Node>(children);
        var uncasted = nodes.Where(_ => _ is T);

        var result = new List<T>();
        foreach (var toCast in uncasted)
        {
            result.Add((T)toCast);
        }

        return result;
    }

    /// <summary>
    /// Linearly interpolates between two Vector3 by a normalized value.
    /// </summary>
    /// <param name="start">The starting value.</param>
    /// <param name="end">The end value.</param>
    /// <param name="weight">The weight, or step to interpolate by.</param>
    /// <returns>The interpolated vector.</returnq>
    public static Vector3 Lerp(Vector3 start, Vector3 end, float weight)
    {
        return new Vector3(
            Mathf.Lerp(start.x, end.x, weight),
            Mathf.Lerp(start.y, end.y, weight),
            Mathf.Lerp(start.z, end.z, weight)
        );
    }

    /// <summary>
    /// Free all children from the node. 
    /// </summary>
    /// <param name="node">The node to remove the children from.</param>
    public static void FreeAllChildren(this Node node)
    {
        var children = node.GetChildren();
        foreach (Node child in children)
        {
            child.QueueFree();
        }
    }

    /// <summary>
    /// Transform any C# collections to a Godot Array.
    /// </summary>
    /// <param name="collection">Any C# collection implementing ICollection.</param>
    /// <typeparam name="T">An element inheriting Node.</typeparam>
    /// <returns>An array filled with the same values as the given collection.</returns>
    public static Array<T> ToGodotArray<T>(this IEnumerable<T> collection) where T : Node
    {
        var array = new Array<T>();

        foreach (var element in collection)
        {
            array.Add(element);
        }

        return array;
    }

    /// <summary>
    /// Transform a godot Array in a C# enumerable.
    /// </summary>
    /// <typeparam name="T">The type we're converting to.</typeparam>
    /// <param name="array">A <see cref="Godot.Collections.Array"/>.</param>
    /// <returns>A C# enumerable filled with the same values as the given array.</returns>
    public static IEnumerable<T> ToEnumerable<T>(this Godot.Collections.Array array)
    {
        var list = new List<T>();

        foreach (var item in array)
        {
            list.Add((T)item);
        }

        return list;
    }

    /// <summary>
    /// Returns the first children of type T.
    /// </summary>
    /// <typeparam name="T">The type we're looking for. Must inherit node.</typeparam>
    /// <param name="node">The node we're starting to look from.</param>
    /// <returns>The first children or null.</returns>
    public static T FindFirstChildOfType<T>(this Node node) where T : Node
    {
        var children = node.FindChildrenOfType<T>();
        var child = children.FirstOrDefault();
        return child;
    }
}
