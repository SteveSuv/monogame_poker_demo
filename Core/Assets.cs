using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

static class Assets
{
    // textures
    public static Texture2D TextureCardsBlackClubs = AssetsLoader.loadTexture2D("/Assets/textures/cards/Clubs-88x124.png");
    public static Texture2D TextureCardsRedDiamonds = AssetsLoader.loadTexture2D("/Assets/textures/cards/Diamonds-88x124.png");
    public static Texture2D TextureCardsRedHearts = AssetsLoader.loadTexture2D("/Assets/textures/cards/Hearts-88x124.png");
    public static Texture2D TextureCardsBlackSpades = AssetsLoader.loadTexture2D("/Assets/textures/cards/Spades-88x124.png");
    public static Texture2D TextureChips = AssetsLoader.loadTexture2D("/Assets/textures/chips/ChipsA_Outline-64x72.png");


    // sounds
    public static SoundEffect SoundMouseClick = AssetsLoader.loadSoundEffect("/Assets/sounds/btn_click.wav");
    public static SoundEffect SoundBGM = AssetsLoader.loadSoundEffect("/Assets/sounds/menu.wav");

    // fonts
    public static FontSystem FontYouquti = AssetsLoader.loadFont("/Assets/fonts/youquti.ttf");
}