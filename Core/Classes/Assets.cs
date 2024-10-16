using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

static class Assets
{
    // textures
    public static readonly Texture2D TextureLogo = AssetsLoader.LoadTexture2D("/Assets/Images/Logo.png");
    public static readonly Texture2D TextureCardsBack = AssetsLoader.LoadTexture2D("/Assets/Images/Card_Back-88x124.png");
    public static readonly Texture2D TextureCardsBlackClubs = AssetsLoader.LoadTexture2D("/Assets/Images/Clubs-88x124.png");
    public static readonly Texture2D TextureCardsRedDiamonds = AssetsLoader.LoadTexture2D("/Assets/Images/Diamonds-88x124.png");
    public static readonly Texture2D TextureCardsRedHearts = AssetsLoader.LoadTexture2D("/Assets/Images/Hearts-88x124.png");
    public static readonly Texture2D TextureCardsBlackSpades = AssetsLoader.LoadTexture2D("/Assets/Images/Spades-88x124.png");
    public static readonly Texture2D TextureChips = AssetsLoader.LoadTexture2D("/Assets/Images/ChipsB_Flat-64x72.png");
    public static readonly Texture2D TextureBackground = AssetsLoader.LoadTexture2D("/Assets/Images/Background.png");
    public static readonly Texture2D TextureTable = AssetsLoader.LoadTexture2D("/Assets/Images/Table.png");

    // sounds
    public static readonly SoundEffect SoundButtOnClick = AssetsLoader.LoadSoundEffect("/Assets/Sounds/ButtonClick.wav");
    public static readonly SoundEffect SoundButtOnHover = AssetsLoader.LoadSoundEffect("/Assets/Sounds/ButtonHover.wav");
    public static readonly SoundEffect SoundBGM = AssetsLoader.LoadSoundEffect("/Assets/Sounds/BGM.wav");

    // fonts
    public static readonly FontSystem FontYuanTi = AssetsLoader.LoadFont("/Assets/Fonts/YuanTi.otf");
    public static readonly FontSystem FontHeiTi = AssetsLoader.LoadFont("/Assets/Fonts/HeiTi.otf");

    // shaders
    // public static readonly Effect EffectGaussianBlur = AssetsLoader.LoadShader("/Assets/Shaders/GaussianBlur.fx");
}