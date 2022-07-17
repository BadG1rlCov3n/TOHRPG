using System;
using System.IO;
using Newtonsoft.Json;
using static Godot.GD;

/// <summary>
/// The json manager.
/// </summary>
public static class JsonManager
{
    /// <summary>
    /// Gets and deserialize a json file into an object.
    /// </summary>
    /// <typeparam name="T">The type to desserialize into.</typeparam>
    /// <param name="fileName">The file name to deserialize from.</param>
    public static T Get<T>(string fileName) where T : class
    {
        try
        {
            var jsonName = $"{fileName}.json";
            if (!File.Exists(jsonName))
            {
                File.Create(jsonName);
            }

            var file = File.ReadAllText(jsonName);
            var deserialized = JsonConvert.DeserializeObject<T>(file);
            return deserialized;
        }
        catch (Exception exception)
        {
            Print($"A problem occured while fetching {fileName} : {exception.Message}");
            throw;
        }
    }

    /// <summary>
    /// Saves an object into a file, json format.
    /// </summary>
    /// <param name="data">The data to save.</param>
    /// <param name="fileName">The file name to save into.</param>
    /// <typeparam name="T">The type of the data to save and serialize.</typeparam>
    public static void Save<T>(T data, string fileName) where T : class
    {
        try
        {
            var serialized = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText($"{fileName}.json", serialized);
        }
        catch (Exception exception)
        {
            Print($"A problem occured while saving data of type {typeof(T)} at location {fileName}: {exception.Message}");
        }
    }
}
