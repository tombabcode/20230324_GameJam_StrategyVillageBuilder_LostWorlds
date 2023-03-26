using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using VXEngine.Objects.Primitives;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Units;

public sealed class Unit {

    public Tile TileSource { get; private set; }
    public Tile TileTarget { get; private set; }
    public Tile TileCurrent { get; private set; }

    public List<Tile> Path { get; private set; }

    private Sprite _sprite;

    public Unit(ContentController content, InputController input, Tile tileSource) {
        _sprite = new Sprite(content, input, content.UnitBannerBase, tileSource.DisplayX, tileSource.DisplayY + ConfigController.TILE_SIZE);
        _sprite.SetOrigin(0, 1);

        TileSource = tileSource;
        TileTarget = tileSource;
        TileCurrent = tileSource;
        Path = new List<Tile>( );
    }

    /// <summary>
    /// Range 0-1 where 0 is start of the path and 1 is the end
    /// </summary>
    public float DistanceCovered { get; private set; }

    public void MoveTo(MapManager map, int targetX, int targetY) {
        if (TileTarget.X == targetX && TileTarget.Y == targetY)
            return;

        TileTarget = map.GetTile(targetX, targetY);
        Path = UnitPathFinder.GetPath(map, TileSource, TileTarget);
    }

    public void MoveTo(MapManager map, Tile tileTarget) {
        if (TileTarget.X == tileTarget.X && TileTarget.Y == tileTarget.Y)
            return;

        TileTarget = tileTarget;
        Path = UnitPathFinder.GetPath(map, TileSource, TileTarget);
    }

    public void Update(GameTime time) {

    }

    public void Render(GameTime time) {
        _sprite.Render(time);
    }

}
