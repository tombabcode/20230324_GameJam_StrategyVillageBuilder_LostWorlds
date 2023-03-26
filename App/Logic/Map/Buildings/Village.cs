using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using Microsoft.Xna.Framework;
using VXEngine.Objects.Primitives;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Buildings;

public sealed class Village : TileObject {

    private Sprite _sprite;

    public Village(ContentController content, InputController input, int x, int y) : base(TileObjectType.Village) {
        ObjectName = "Village";
        MovementModifier = 2;

        _sprite = new Sprite(content, input, content.TileObjectVillage, x * ConfigController.TILE_SIZE, (y + 1) * ConfigController.TILE_SIZE);
        _sprite.SetOrigin(0, 1);
    }

    public override void Update(GameTime time) {
        _sprite.Update(time);
    }

    public override void Render(GameTime time) {
        _sprite.Render(time);
    }

}
