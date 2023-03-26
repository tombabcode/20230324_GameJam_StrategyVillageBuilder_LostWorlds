using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Units;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map;

public sealed class MapManager {

    private Tile[] _tiles;
    private List<Unit> _units;

    public int MapDiameter { get; private set; } = 16;

    public float MapCenterDisplay { get; private set; } = 0;
    public float MapMaximumDistance { get; private set; } = 0;

    public Tile TileVillage { get; private set; }

    public MapManager(ContentController content, ConfigController config, InputController input) {
        Tile tileVillage;
        _tiles = MapGenerator.Generate(content, config, input, MapDiameter, out tileVillage);
        TileVillage = tileVillage;

        MapCenterDisplay = MapDiameter * .5f * ConfigController.TILE_SIZE;
        MapMaximumDistance = MapDiameter * .5f * ConfigController.TILE_SIZE;

        _units = new List<Unit>( );
    }

    public void AddUnit(Unit unit) {
        _units.Add(unit);
        unit.MoveTo(this, GetTile(8, 2));
    }

    public Tile GetTile(int x, int y) {
        int id = y * MapDiameter + x;
        if (id < 0 || id >= _tiles.Length)
            return null;
        return _tiles[id];
    }

    public void Update(GameTime time) {
        foreach (Tile tile in _tiles)
            tile?.Update(time);
    }

    public void Render(GameTime time) {
        for (int y = 0; y < MapDiameter; y++)
            for (int x = 0; x < MapDiameter; x++)
                _tiles[y * MapDiameter + x]?.Render(time);

        foreach (Unit unit in _units)
            unit.Render(time);
    }

}
