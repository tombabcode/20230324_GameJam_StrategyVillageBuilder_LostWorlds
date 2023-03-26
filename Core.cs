using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Scenes;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using VXEngine.Models;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds;

public class Core : Game {

    private ConfigController _config;
    private ContentController _content;
    private InputController _input;
    private SceneController _scene;
    private AudioController _audio;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _canvas;

    public Core( ) {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize( ) {
        base.Initialize( );

        _config = new ConfigController( );
        _config.Load( );
        _config.ApplyWindowChanges(_graphics);

        _canvas = new SpriteBatch(GraphicsDevice);

        _content = new ContentController(_canvas, GraphicsDevice, Content, _graphics);
        _content.LoadAssets( );
        _content.LoadFont((int)FontType.Basic, new FontData(Content.Load<SpriteFont>(Path.Combine("Fonts", "font_basic")), 32));
        _content.LoadFont((int)FontType.BasicBold, new FontData(Content.Load<SpriteFont>(Path.Combine("Fonts", "font_basic_bold")), 32));

        _input = new InputController(_config);

        _audio = new AudioController( );

        _scene = new SceneController( );
        _scene.AddScene((int)SceneType.Intro, new IntroScene(_content, _config, _input));
        _scene.AddScene((int)SceneType.MainMenu, new MainMenuScene(_content, _config, _input));
        _scene.AddScene((int)SceneType.Gameplay, new GameplayScene(_content, _config, _input, _audio));
        _scene.ChangeScene((int)SceneType.Gameplay);

        ((GameplayScene)_scene.CurrentScene).NewGame( );
    }

    protected override void Update(GameTime gameTime) {
        base.Update(gameTime);
        _scene.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        base.Draw(gameTime);
        _scene.Display(gameTime);
    }
}
