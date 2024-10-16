using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Screens;

class RoomScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new() { localPosition = MyGame.ScreenCenter };
    private readonly Node myCardsNode = new() { localPosition = new(0, MyGame.ScreenHeight / 2 - 100) };
    private readonly List<Card> allCards = new Cards().cards;


    public override void Initialize()
    {
        var bg = new SpriteNode() { texture = Assets.TextureTable, WorldPosition = new(0, 0), LayerDepth = 0 };
        world.Children.Insert(0, bg);

        var cardBackAtlas = new SpriteAtlas(Assets.TextureCardsBack) { regionWidth = 88, regionHeight = 124, maxRegionCount = 2 }.GetAtlas();
        var cardBackNode = new SpriteRegionNode() { texture2DRegion = cardBackAtlas[0], scale = new(0.8f) };
        world.NodeManager.AddChild(cardBackNode);

        world.NodeManager.AddChild(new LabelNode() { tag = "CardsCount", text = $"剩余{allCards.Count}张牌", localPosition = new(0, 80) });

        // PickCard();
        var emptyNode = new Node();
        world.NodeManager.AddChild(emptyNode);
        emptyNode.ComponentManager.AddComponent(new ContinuousClockComponent()
        {
            intervalSeconds = 1,
            Tick = (object sender, EventArgs e) =>
            {
                PickCard();
            }
        });

        // world.NodeManager.AddChild(myCardsNode);

        // var singleCardNode = new SpriteRegionNode() { texture2DRegion = allCards[0].Texture2DRegion, scale = new(0.8f), localPosition = new(0, -(MyGame.ScreenHeight / 2 - 100)) };
        // singleCardNode.ComponentManager.AddComponent(new TweenerComponent()
        // {
        //     tweenerAction = (t, n) =>
        //     {
        //         t.TweenTo(n, e => e.localPosition, new(0, 0), 0.2f);
        //     }
        // });

        // myCardsNode.NodeManager.AddChild(singleCardNode);

        // var cardBlackClubAtlas = new SpriteAtlas(Assets.TextureCardsBlackClubs) { regionWidth = 88, regionHeight = 124, maxRegionCount = 13 }.GetAtlas();
        // var cardNode = new SpriteRegionNode() { texture2DRegion = cardBlackClubAtlas[cardIndex] };
        // cardNode.ComponentManager.AddComponent(new ContinuousClockComponent()
        // {
        //     intervalSeconds = 0.2f,
        //     Tick = (object sender, EventArgs e) =>
        //     {

        //         if (cardIndex >= 12)
        //         {
        //             cardIndex = 0;
        //         }
        //         else
        //         {
        //             cardIndex++;
        //         };

        //         cardNode.texture2DRegion = cardBlackClubAtlas[cardIndex];
        //     }
        // });
        // world.NodeManager.AddChild(cardNode);

        world.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        world.Update(gameTime);

        if (MyGame.KeyboardState.WasKeyPressed(Keys.Space))
        {
            var a = myCardsNode.Children[0] as SpriteRegionNode;
            a.texture2DRegion = allCards[Random.Shared.Next(13 * 4)].Texture2DRegion;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(MyGame.ThemeColor);
        MyGame.SpriteBatch.Begin();
        world.Draw();
        MyGame.SpriteBatch.End();
    }

    private void PickCard()
    {
        allCards.Shuffle(Random.Shared);
        var card = allCards[0];
        allCards.Remove(card);
        var cardsCountLabelNode = world.NodeManager.GetChildByTag<LabelNode>("CardsCount");
        cardsCountLabelNode.text = $"剩余{allCards.Count}张牌";
    }
}