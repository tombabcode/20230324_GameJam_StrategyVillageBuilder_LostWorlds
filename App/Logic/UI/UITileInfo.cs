using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using Microsoft.Xna.Framework;
using VXEngine.Objects.Primitives;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.UI;

public sealed class UITileInfo {

    private ContentController _content;
    private ConfigController _config;
    private InputController _input;

    private Tile _data;

    private Container _view;
    private Box _background;

    private Text _labelObject;
    private Text _labelGround;
    private Text _labelMovementSpeed;

    public bool IsVisible { get; private set; }

    public UITileInfo(ContentController content, ConfigController config, InputController input) {
        _content = content;
        _config = config;
        _input = input;

        _view = new Container(_input);
        _view.SetPosition(_config.ViewWidth - 256 - 8, _config.ViewHeight - 512 - 8);

        _background = new Box(_content, _input, 0, 0, 256, 512, Color.Black * 0.9f);
        _background.SetParent(_view);
        _background.SetInteractive(true);
        _background.OnClick.Add(_ => {
            _input.StopPropagation( );
        });
        _background.OnClickCondition = _input.IsLMBPressedOnce;

        _labelObject = new Text(_content.GetFont((int)FontType.BasicBold), _content, _input, "Forest", size: 16);
        _labelObject.SetParent(_view);
        _labelObject.SetPosition(14, 85);

        _labelGround = new Text(_content.GetFont((int)FontType.Basic), _content, _input, "Grass", size: 10);
        _labelGround.SetParent(_view);
        _labelGround.SetPosition(14, 108);

        _labelMovementSpeed = new Text(_content.GetFont((int)FontType.Basic), _content, _input, "Movement speed: 100%", size: 10);
        _labelMovementSpeed.SetParent(_view);
        _labelMovementSpeed.SetPosition(14, 138);
    }

    public void Show(Tile data) {
        if (data == null || _data == data || !data.HasTileObject())
            return;

        IsVisible = true;
        _data = data;

        _labelObject.SetText($"{_data.GetTileObjectName()} ({_data.X}, {_data.Y})");
        _labelMovementSpeed.SetText($"Movement speed: {_data.GetMovementModifier( ) * 100:0}%");
    }

    public void Hide( ) {
        IsVisible = false;
        _data = null;
    }

    public void Update(GameTime time) {
        if (!IsVisible)
            return;

        _background.Update(time);
        _labelObject.Update(time);
        _labelGround.Update(time);
        _labelMovementSpeed.Update(time);
    }

    public void Render(GameTime time) {
        if (!IsVisible)
            return;

        _background.Render(time);
        _labelObject.Render(time);
        _labelGround.Render(time);
        _labelMovementSpeed.Render(time);
    }

}
