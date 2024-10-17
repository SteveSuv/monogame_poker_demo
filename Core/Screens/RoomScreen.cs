using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Screens;

class RoomScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new() { localPosition = MyGame.ScreenCenter };
    private readonly Node myCardsNode = new() { localPosition = new(0, MyGame.ScreenHeight / 2 - 60) };
    private readonly List<Card> allCards = new Cards().cards;
    private readonly List<Card> myCards = [];

    public override void Initialize()
    {
        var bg = new SpriteNode() { texture = Assets.TextureTable, WorldPosition = new(0, 0), LayerDepth = 0 };
        world.Children.Insert(0, bg);

        var cardBackAtlas = new SpriteAtlas(Assets.TextureCardsBack) { regionWidth = 88, regionHeight = 124, maxRegionCount = 2 }.GetAtlas();
        var cardBackNode = new SpriteRegionNode() { tag = "CardBackNode", texture2DRegion = cardBackAtlas[0], scale = new(0.8f) };
        world.NodeManager.AddChild(cardBackNode);

        world.NodeManager.AddChild(new LabelNode() { tag = "CardsCountLabel", text = $"剩余{allCards.Count}张牌", localPosition = new(0, 80) });

        world.ComponentManager.AddComponent(new ContinuousClockComponent()
        {
            intervalSeconds = 0.2f,
            Tick = (object sender, EventArgs e) =>
            {
                if (myCards.Count < 5)
                {
                    PickCard();
                }
            }
        });

        world.NodeManager.AddChild(myCardsNode);

        var labelNode = new LabelNode() { tag = "CardLabel", color = Color.Transparent, fontSize = 24 };
        world.NodeManager.AddChild(labelNode);


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
        if (allCards.Count <= 0)
        {
            var cardBackNode = world.NodeManager.GetChildByTag<SpriteRegionNode>("CardBackNode");
            cardBackNode?.RemoveFromParent();
            return;
        }

        allCards.Shuffle(Random.Shared);
        var card = allCards[0];
        allCards.Remove(card);


        // singleCardNode.ComponentManager.AddComponent(new TweenerComponent()
        // {
        //     tweenerAction = (t, n) =>
        //     {
        //         t.TweenTo(n, e => e.localPosition, new(0, 0), 0.2f);
        //     }
        // });

        myCards.Add(card);

        var cardsCountLabelNode = world.NodeManager.GetChildByTag<LabelNode>("CardsCountLabel");
        cardsCountLabelNode.text = $"剩余{allCards.Count}张牌";

        myCardsNode.NodeManager.RemoveAllChildren();

        var cardLabel = world.NodeManager.GetChildByTag<LabelNode>("CardLabel");

        var len = myCards.Count;
        for (int i = 0; i < len; i++)
        {
            var myCard = myCards[i];
            var x = i - (len - 1) / 2;
            var singleCardNode = new SpriteRegionNode() { texture2DRegion = myCard.Texture2DRegion, scale = new(0.8f), localPosition = new(x * 40, 0) };

            singleCardNode.ComponentManager.AddComponent(new MouseEventComponent()
            {
                OnMouseMove = (object sender, Vector2 mousePos) =>
                {
                    cardLabel.color = Color.Black;
                    cardLabel.text = myCard.Label;
                    cardLabel.WorldPosition = mousePos + new Vector2(0, -20);
                },
                OnMouseLeave = (object sender, Vector2 mousePos) =>
                {
                    cardLabel.color = Color.Transparent;
                },
                OnClick = (object sender, Vector2 mousePos) =>
                {
                    if (singleCardNode.localPosition.Y == 0)
                    {
                        singleCardNode.localPosition.Y = -20;
                    }
                    else
                    {
                        singleCardNode.localPosition.Y = 0;
                    }

                },
            });
            myCardsNode.NodeManager.AddChild(singleCardNode);
        }
    }
}