using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VXEngine.Scenes;
using VXEngine.Utility;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Scenes;

public sealed class IntroScene : SceneBase {

    private InputController _input;

    public IntroScene(ContentController content, ConfigController config, InputController input) : base(content, config, (int)SceneType.Intro) {
        _input = input;
    }

    public override void Update(GameTime time) {
        _input.Update(time);
    }

    public override void Render(GameTime time) {
        RenderUtility.RenderScene(_content, _config, View, SamplerState.AnisotropicClamp, null, ( ) => {

        });
    }

}
