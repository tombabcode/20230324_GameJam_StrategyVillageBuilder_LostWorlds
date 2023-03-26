using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using Microsoft.Xna.Framework;
using VXEngine.Objects.Primitives;

using CFG = _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers.ConfigController;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map;

public class Tile {

    private ContentController _content;
    private InputController _input;

    public int X { get; protected set; }
    public int Y { get; protected set; }

    public int DisplayX => X * CFG.TILE_SIZE;
    public int DisplayY => Y * CFG.TILE_SIZE;

    private Box _ground;
    private TileObject _object;

    public Tile(ContentController content, InputController input, int x, int y) {
        X = x;
        Y = y;

        _content = content;
        _input = input;

        _ground = new Box(_content, _input, x * CFG.TILE_SIZE, y * CFG.TILE_SIZE, CFG.TILE_SIZE, CFG.TILE_SIZE, x % 2 == 0 ? Color.Red : Color.White);
    }

    public bool HasTileObject( ) => _object != null;
    public string GetTileObjectName( ) => _object?.ObjectName ?? string.Empty;
    public void SetTileObject(TileObject obj) => _object = obj;
    public float GetMovementModifier( ) => _object?.MovementModifier ?? 1;

    public void Update(GameTime time) {

    }

    public void Render(GameTime time) {
        _ground.Render(time);
        _object?.Render(time);
    }

}
