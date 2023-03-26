using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VXEngine.Objects;
using VXEngine.Objects.Primitives;
using VXEngine.Utility;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.UI;

public sealed class UIManager {

    private ContentController _content;
    private ConfigController _config;
    private InputController _input;

    private RenderTarget2D _view;

    private UITileInfo _tileInfo;

    private Text _debugSelection;
    private Text _debugCamera;
    private Text _debugTime;

    public UIManager(ContentController content, ConfigController config, InputController input) {
        _content = content;
        _config = config;
        _input = input;

        _view = new RenderTarget2D(content.Device, (int)config.ViewWidth, (int)config.ViewHeight);

        _tileInfo = new UITileInfo(_content, _config, _input);

        _debugSelection = new Text(_content.GetFont((int)FontType.Basic), _content, input, "-", size: 10);
        _debugSelection.SetPosition(5, 4);

        _debugCamera = new Text(_content.GetFont((int)FontType.Basic), _content, input, "-", size: 10);
        _debugCamera.SetPosition(5, 18);

        _debugTime = new Text(_content.GetFont((int)FontType.Basic), _content, input, "-", size: 10);
        _debugTime.SetPosition(5, 32);
    }

    public void SetDebugSelection(int x, int y) => _debugSelection.SetText($"Selection ({x}, {y})");
    public void SetDebugCamera(Camera camera) => _debugCamera.SetText($"Camera ({camera.Target.X:0}, {camera.Target.Y:0}, x{camera.Zoom:0.00})");
    public void SetDebugTime(TimeManager time) => _debugTime.SetText($"Time (Tick {time.CurrentTick}, Hour {time.GameHour}, Day {time.GameDay}, Year {time.GameYear})");
    public void ShowTileInfo(Tile tile) => _tileInfo.Show(tile);
    public void HideTileInfo( ) => _tileInfo.Hide( );

    public void Update(GameTime time) {
        _tileInfo.Update(time);
    }

    public void Render(GameTime time) {
        RenderUtility.RenderScene(_content, _config, _view, SamplerState.AnisotropicClamp, null, ( ) => {
            _tileInfo.Render(time);
            _debugSelection.Render(time);
            _debugCamera.Render(time);
            _debugTime.Render(time);
        });
    }

    public void Display( ) => RenderUtility.DisplayScene(_content, _config, _view);

}
