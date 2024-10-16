
using MonoGame.Extended.Graphics;


enum CardColor
{
    Black,
    Red
}

enum CardType
{
    BlackClubs,
    BlackSpade,
    RedDiamond,
    RedHeart,
}

class Card
{
    public string Label;
    public int Num;
    public CardType Type;

    public CardColor Color;

    public Texture2DRegion Texture2DRegion;
}

class Cards
{
    private readonly List<Texture2DRegion> atlas;
    public readonly List<Card> cards;

    public Cards()
    {
        var cardBlackClubsAtlas = new SpriteAtlas(Assets.TextureCardsBlackClubs) { regionWidth = 88, regionHeight = 124, maxRegionCount = 13 }.GetAtlas();
        var cardBlackSpadesAtlas = new SpriteAtlas(Assets.TextureCardsBlackSpades) { regionWidth = 88, regionHeight = 124, maxRegionCount = 13 }.GetAtlas();
        var cardRedDiamondsAtlas = new SpriteAtlas(Assets.TextureCardsRedDiamonds) { regionWidth = 88, regionHeight = 124, maxRegionCount = 13 }.GetAtlas();
        var cardRedHeartsAtlas = new SpriteAtlas(Assets.TextureCardsRedHearts) { regionWidth = 88, regionHeight = 124, maxRegionCount = 13 }.GetAtlas();

        atlas = [.. cardBlackClubsAtlas, .. cardBlackSpadesAtlas, .. cardRedDiamondsAtlas, .. cardRedHeartsAtlas];

        cards = [
        .. cardBlackClubsAtlas.Select((x, i) => new Card()
        {
            Texture2DRegion = x,
            Label = $"黑方{i + 1}",
            Num = i + 1,
            Type = CardType.BlackClubs,
            Color = CardColor.Black,

        }),
            .. cardBlackSpadesAtlas.Select((x, i) => new Card()
            {
                Texture2DRegion = x,
                Label = $"黑桃{i + 1}",
                Num = i + 1,
                Type = CardType.BlackSpade,
                Color = CardColor.Black
            }),
            .. cardRedDiamondsAtlas.Select((x, i) => new Card()
            {
                Texture2DRegion = x,
                Label = $"红钻{i + 1}",
                Num = i + 1,
                Type = CardType.RedDiamond,
                Color = CardColor.Red
            }),
            .. cardRedHeartsAtlas.Select((x, i) => new Card()
            {
                Texture2DRegion = x,
                Label = $"红心{i + 1}",
                Num = i + 1,
                Type = CardType.RedHeart,
                Color = CardColor.Red
            })
        ];
    }
}