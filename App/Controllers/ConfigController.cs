using System;
using VXEngine.Controllers;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;

public sealed class ConfigController : BasicConfigController {

    public const int TILE_SIZE = 32;

    public Random Random { get; private set; }

    public ConfigController() {
        IsPixelart = true;

        Random = new Random( );
    }

}
