using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

static class Assets
{
    // textures
    public static Texture2D TextureLogo => AssetsLoader.LoadTexture2D("/Assets/Images/Logo.png");
    public static Texture2D TextureCardsBlackClubs => AssetsLoader.LoadTexture2D("/Assets/Images/Clubs-88x124.png");
    public static Texture2D TextureCardsRedDiamonds => AssetsLoader.LoadTexture2D("/Assets/Images/Diamonds-88x124.png");
    public static Texture2D TextureCardsRedHearts => AssetsLoader.LoadTexture2D("/Assets/Images/Hearts-88x124.png");
    public static Texture2D TextureCardsBlackSpades => AssetsLoader.LoadTexture2D("/Assets/Images/Spades-88x124.png");
    public static Texture2D TextureChips => AssetsLoader.LoadTexture2D("/Assets/Images/ChipsB_Flat-64x72.png");


    // sounds
    public static SoundEffect SoundButtonClick => AssetsLoader.LoadSoundEffect("/Assets/Sounds/ButtonClick.wav");
    public static SoundEffect SoundButtonHover => AssetsLoader.LoadSoundEffect("/Assets/Sounds/ButtonHover.wav");
    public static SoundEffect SoundBGM => AssetsLoader.LoadSoundEffect("/Assets/Sounds/BGM.wav");

    // fonts
    public static FontSystem FontYuanTi => AssetsLoader.LoadFont("/Assets/Fonts/YuanTi.otf");
    public static FontSystem FontHeiTi => AssetsLoader.LoadFont("/Assets/Fonts/HeiTi.otf");
}