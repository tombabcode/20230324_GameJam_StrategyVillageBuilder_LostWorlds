using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map;
using Microsoft.Xna.Framework;
using System;
using VXEngine.Objects;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic;

public sealed class GameplayCamera : Camera {

    // References
    private InputController _input;

    // Camera press position (to smoothly move camera around)
    private int _cameraPressX = -1;
    private int _cameraPressY = -1;

    // Constructor
    public GameplayCamera(ConfigController config, InputController input, MapManager map) : base(config.ViewWidth * .5f, config.ViewHeight * .5f) {
        _input = input;

        IsZoomSmooth = true;
        ZoomMax = 4.0f;
        ZoomInertia = 10;
        IsMoveSmooth = true;
        MoveInertia = 5;
        SetZoom(2.0f, true);
        LookAt(map.TileVillage.DisplayX + ConfigController.TILE_SIZE * .5f, map.TileVillage.DisplayY + ConfigController.TILE_SIZE + .5f, true);

        // On camera's smooth movement update - check if camera's view is not outside the map
        OnCameraMovementUpdate = ( ) => {
            float cameraDistanceFromTheCenter = (float)Math.Sqrt(Math.Pow(Target.X - map.MapCenterDisplay, 2) + Math.Pow(Target.Y - map.MapCenterDisplay, 2));
            if (cameraDistanceFromTheCenter > map.MapMaximumDistance) {
                float cameraAngleFromTheCenter = (float)Math.Atan2(Target.Y - map.MapCenterDisplay, Target.X - map.MapCenterDisplay);
                LookAt(
                    (float)(map.MapCenterDisplay + map.MapDiameter * .5f * ConfigController.TILE_SIZE * Math.Cos(cameraAngleFromTheCenter)),
                    (float)(map.MapCenterDisplay + map.MapDiameter * .5f * ConfigController.TILE_SIZE * Math.Sin(cameraAngleFromTheCenter)),
                    true
                );
            }
        };
    }

    public override void Update(GameTime time) {
        base.Update(time);

        // Control camera
        if (_input.HasScrolledDown( )) ZoomOut( );
        if (_input.HasScrolledUp( )) ZoomIn( );
        if (_input.IsMMBPressed( )) {
            LookBy(_input.MouseDiffX / Zoom, _input.MouseDiffY / Zoom);
        } else {
            if (_input.IsRMBPressedOnce( )) {
                _cameraPressX = _input.MouseWindowX;
                _cameraPressY = _input.MouseWindowY;
            }

            if (_input.IsRMBPressed( ))
                LookBy((_cameraPressX - _input.MouseWindowX) / 10f, (_cameraPressY - _input.MouseWindowY) / 10f);
        }
    }

}
