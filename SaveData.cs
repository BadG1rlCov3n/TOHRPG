using Godot;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The save data. It MUSTS inherit node to be an autoload.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class SaveData : Node
{
    private const string FileName = "save_file";

    private const int EventCompleted = -1;

    /// <summary>
    /// The events. 
    /// The idea would be to have a list of keys corresponding to the events happening in the game, and a value to indicate how far we are in completing it.
    /// For example tutorial could have the key "tutorial", and the events would be "beginning = 0, school = 1, chose palisman = 2, chosen palisman = 3, DONE = -1 (EventCompleted)". 
    /// </summary>
    /// <typeparam name="string">The keys to the events.</typeparam>
    /// <typeparam name="int">The event's progression.</typeparam>
    /// <returns>The events that have been started so far.</returns>
    [JsonProperty]
    public Dictionary<string, int> Events { get; private set; } = new Dictionary<string, int>();

    /// <inheritdoc/>
    public override void _Ready()
    {
        // We need to remove "auto quitting" to have save on quit.
        GetTree().SetAutoAcceptQuit(false);

        var save = JsonManager.Get<SaveData>(FileName) ?? new SaveData();

        // Map every property here.
        Events = save.Events;
    }

    /// <inheritdoc/>
    public override void _Notification(int what)
    {
        // If we asked to safely quit, save, THEN quit.
        if (what == MainLoop.NotificationWmQuitRequest)
        {
            JsonManager.Save(this, FileName);
            GetTree().Quit();
        }
    }

    /// <summary>
    /// Has the given event been started?
    /// </summary>
    /// <param name="eventName">The even we're looking for.</param>
    /// <returns>True if the event has been started, false otherwise.</returns>
    public bool HasEventStarted(string eventName)
    {
        return Events.Keys.Any(_ => _ == eventName);
    }

    /// <summary>
    /// Is the given event fully finished?
    /// </summary>
    /// <param name="eventName">The event we wants to know the completion.</param>
    /// <returns>True if the event has been completed, false otherwise.</returns>
    public bool IsEventCompleted(string eventName)
    {
        return HasEventStarted(eventName) && Events[eventName] == EventCompleted;
    }
}
