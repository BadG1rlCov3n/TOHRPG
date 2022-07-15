using Godot;
using System;

/// <summary>
/// The ground.
/// </summary>
public class Ground : TileMap
{
    /// <inheritdoc/>
    public override void _Ready()
    {
        // NB : i'm only filling this tilemap real quick, to have a test ground to check for hud movement.
        for (int x = -100; x < 100; x++)
        {
            for (int y = -100; y < 100; y++)
            {
                if (x % 2 == 0 && y % 2 == 0)
                {
                    SetCell(x, y, 0);
                }
            }
        }
    }
}
