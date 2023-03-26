using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.UI;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Resources;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic;
using Microsoft.Xna.Framework;
using System;
using VXEngine.Objects;
using VXEngine.Scenes;
using VXEngine.Utility;
using Microsoft.Xna.Framework.Input;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Units;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Scenes;

public sealed class GameplayScene : SceneBase {

    // References
    private InputController _input;
    private AudioController _audio;

    // Camera
    private Camera _camera;

    // Gameplay's logic managers
    private MapManager _map;
    private UIManager _ui;
    private ResourceManager _resources;

    // Mouse selection
    private int _selectionX = -1;
    private int _selectionY = -1;
    
    // Currently hovered and currently selected tiles
    private Tile _tileHovered = null;
    private Tile _tileSelected = null;

    public GameplayScene(ContentController content, ConfigController config, InputController input, AudioController audio) : base(content, config, (int)SceneType.Gameplay) {
        _input = input;
        _audio = audio;
        _ui = new UIManager(content, config, input);
        _resources = new ResourceManager( );
    }

    public void NewGame( ) {
        _map = new MapManager((ContentController)_content, (ConfigController)_config, _input);
        _camera = new GameplayCamera((ConfigController)_config, _input, _map);
    }

    public override void OnShow( ) {
        base.OnShow( );
        _audio.LoadGameplayQueue((ContentController)_content, _config);
        _audio.PlayRandomGameplaySong((ConfigController)_config);
    }

    public override void Update(GameTime time) {
        // Basic updates
        _audio.Update(time);
        _input.Update(time);
        _ui.Update(time);
        _map.Update(time);
        _camera.Update(time);

        // Tile selection
        _selectionX = (int)Math.Floor((_input.MouseX / _camera.Zoom - _camera.Offset.X / _camera.Zoom + _camera.Target.X) / ConfigController.TILE_SIZE);
        _selectionY = (int)Math.Floor((_input.MouseY / _camera.Zoom - _camera.Offset.Y / _camera.Zoom + _camera.Target.Y) / ConfigController.TILE_SIZE);
        
        // Hover tile
        _tileHovered = null;
        _tileHovered = _map.GetTile(_selectionX, _selectionY);

        // Select tile
        if (_input.IsLMBPressedOnce() && _tileHovered != null) {
            // Selected new tile
            if (_tileHovered != _tileSelected) {
                _tileSelected = _tileHovered;
                _ui.ShowTileInfo(_tileSelected);
                _camera.LookAt(_tileSelected.DisplayX + ConfigController.TILE_SIZE * .5f + View.Width * 0.075f, _tileSelected.DisplayY + ConfigController.TILE_SIZE * .5f);

            // Selected old tile (deselect)
            } else {
                _tileSelected = null;
                _ui.HideTileInfo( );
            }
        }

        // Temp
        if (_input.IsKeyPressedOnce(Keys.Space)) {
            _map.AddUnit(new Unit((ContentController)_content, _input, _map.TileVillage));
        }

        // Update UI
        _ui.SetDebugSelection(_selectionX, _selectionY);
        _ui.SetDebugCamera(_camera);
    }

    public override void Render(GameTime time) {
        RenderUtility.RenderScene(_content, _config, View, null, _camera, ( ) => {
            _map.Render(time);
        });

        _ui.Render(time);
    }

    public override void Display( ) {
        RenderUtility.RenderFinalScene(_content, _config, View, ( ) => {
            RenderUtility.DisplayScene(_content, _config, View);
            _ui.Display( );
        });
    }

}
