using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

static class Assets
{
    // textures
    public static Texture2D TextureCardsBlackClubs => AssetsLoader.LoadTexture2D("/Assets/images/Clubs-88x124.png");
    public static Texture2D TextureCardsRedDiamonds => AssetsLoader.LoadTexture2D("/Assets/images/Diamonds-88x124.png");
    public static Texture2D TextureCardsRedHearts => AssetsLoader.LoadTexture2D("/Assets/images/Hearts-88x124.png");
    public static Texture2D TextureCardsBlackSpades => AssetsLoader.LoadTexture2D("/Assets/images/Spades-88x124.png");
    public static Texture2D TextureChips => AssetsLoader.LoadTexture2D("/Assets/images/ChipsB_Flat-64x72.png");


    // sounds
    public static SoundEffect SoundMouseClick => AssetsLoader.LoadSoundEffect("/Assets/sounds/btn_click.wav");
    public static SoundEffect SoundBGM => AssetsLoader.LoadSoundEffect("/Assets/sounds/menu.wav");

    // fonts
    public static FontSystem FontPuhuiti => AssetsLoader.LoadFont("/Assets/fonts/puhuiti.ttf");
}