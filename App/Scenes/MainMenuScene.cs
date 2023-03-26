using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using Microsoft.Xna.Framework;
using VXEngine.Scenes;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Scenes;

public sealed class MainMenuScene : SceneBase {

    private InputController _input;

    public MainMenuScene(ContentController content, ConfigController config, InputController input) : base(content, config, (int)SceneType.MainMenu) {
        _input = input;
    }

    public override void Update(GameTime time) {
    }

    public override void Render(GameTime time) {
    }

}
