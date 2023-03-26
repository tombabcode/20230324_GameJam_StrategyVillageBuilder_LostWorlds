using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using Microsoft.Xna.Framework;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map;

public abstract class TileObject {

    public string ObjectName { get; protected set; }

    public float MovementModifier { get; protected set; } = 1;
    public float ResourcesMax { get; protected set; } = 1000;
    public float ResourcesLeft { get; protected set; } = 1000;

    public TileObjectType Type { get; private set; }

    public TileObject(TileObjectType type) { 
        Type = type;
    }

    public abstract void Update(GameTime time);
    public abstract void Render(GameTime time);

}
