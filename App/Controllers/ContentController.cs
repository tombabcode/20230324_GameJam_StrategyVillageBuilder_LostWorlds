using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using VXEngine.Audio;
using VXEngine.Controllers;
using VXEngine.Textures;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;

public sealed class ContentController : BasicContentController {

    // Audio credits
    // https://pixabay.com/pl/music/gowny-tytu-where-the-brave-may-live-forever-viking-background-music-109867/
    public SoundEffect MusicGameplay1 { get; private set; }

    // Audio credits
    // https://pixabay.com/pl/music/budowac-sceny-heilir-sir-norse-viking-background-music-114582/
    public SoundEffect MusicGameplay2 { get; private set; }

    // Graphics
    public TextureStatic TileObjectForest { get; private set; }
    public TextureStatic TileObjectMeadow { get; private set; }
    public TextureStatic TileObjectMonolith { get; private set; }
    public TextureStatic TileObjectVillage { get; private set; }

    public TextureStatic UnitBannerBase { get; private set; }

    public ContentController(SpriteBatch canvas, GraphicsDevice device, ContentManager content, GraphicsDeviceManager manager) : base(canvas, device, content, manager) { }

    public override void LoadAssets( ) {
        base.LoadAssets( );

        MusicGameplay1 = _content.Load<SoundEffect>(Path.Combine("Audio", "Music", "music_gameplay_1"));
        MusicGameplay2 = _content.Load<SoundEffect>(Path.Combine("Audio", "Music", "music_gameplay_2"));

        TileObjectForest = new TextureStatic(_content.Load<Texture2D>(Path.Combine("Graphics", "Objects", "Env", "forest_tileset")));
        TileObjectMeadow = new TextureStatic(_content.Load<Texture2D>(Path.Combine("Graphics", "Objects", "Env", "meadow")));
        TileObjectMonolith = new TextureStatic(_content.Load<Texture2D>(Path.Combine("Graphics", "Objects", "Env", "monolith")));
        TileObjectVillage = new TextureStatic(_content.Load<Texture2D>(Path.Combine("Graphics", "Objects", "Buildings", "village")));
        
        UnitBannerBase = new TextureStatic(_content.Load<Texture2D>(Path.Combine("Graphics", "Units", "unit_banner_base")));
    }

}
