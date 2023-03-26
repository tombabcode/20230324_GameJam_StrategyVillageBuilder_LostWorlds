using System;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Units;

public sealed class PathNode {

    // Data of the tile
    public Tile Data { get; private set; }

    public PathNode PreviousNode { get; set; }

    /// <summary>
    /// Distance from start node
    /// </summary>
    public float GCost { get; set; }

    /// <summary>
    /// Distance to target node
    /// </summary>
    public float HCost { get; set; }

    /// <summary>
    /// Total value of <see cref="GCost"/> + <see cref="HCost"/>
    /// </summary>
    public float FCost => GCost + HCost;

    public PathNode(Tile data) {
        Data = data;
        PreviousNode = null;
    }

    public float GetDistanceBetweenTiles(Tile tileTarget) {
        if (Math.Abs(Data.X - tileTarget.X) > Math.Abs(Data.Y - tileTarget.Y)) {
            return 14 * Math.Abs(Data.Y - tileTarget.Y) + 10 * (Math.Abs(Data.X - tileTarget.X) - Math.Abs(Data.Y - tileTarget.Y));
        } else {
            return 14 * Math.Abs(Data.X - tileTarget.X) + 10 * (Math.Abs(Data.Y - tileTarget.Y) - Math.Abs(Data.X - tileTarget.X));
        }
    }

}
