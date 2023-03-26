using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using Microsoft.Xna.Framework;
using VXEngine.Objects.Primitives;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Environment;

public sealed class Forest : TileObject {

    private Sprite _sprite;

    public Forest(ContentController content, InputController input, int x, int y) : base(TileObjectType.Forest) {
        ObjectName = "Forest";
        MovementModifier = .5f;

        _sprite = new Sprite(content, input, content.TileObjectForest, x * ConfigController.TILE_SIZE, (y + 1) * ConfigController.TILE_SIZE);
        _sprite.SetOrigin(0, 1);
    }

    public override void Update(GameTime time) {
        _sprite.Update(time);
    }

    public override void Render(GameTime time) {
        _sprite.Render(time);
    }

}
