using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;

class RoomScreen(MyGame game) : GameScreen(game)
{

    private Texture2DAtlas atlas;
    private int cardIndex = 0;
    private readonly Node world = new()
    {
        localPosition = MyGame.ScreenCenter,
    };

    public override void Initialize()
    {

        var bg = new SpriteNode() { texture = Assets.TextureTable, WorldPosition = new(0, 0), LayerDepth = 0, scale = new(0.5f) };

        world.Children.Insert(0, bg);

        atlas = new SpriteAtlas(Assets.TextureCardsBlackClubs) { regionWidth = 88, regionHeight = 124, maxRegionCount = 13 }.GetAtlas(); ;

        var card = new SpriteRegionNode() { texture2DRegion = atlas[cardIndex] };

        card.ComponentManager.AddComponent(new ContinuousClockComponent()
        {
            intervalSeconds = 0.2f,
            Tick = (object sender, EventArgs e) =>
            {

                if (cardIndex >= 12)
                {
                    cardIndex = 0;
                }
                else
                {
                    cardIndex++;
                };

                card.texture2DRegion = atlas[cardIndex];
            }
        });

        world.NodeManager.AddChild(card);

        world.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(new Color(0, 128, 128));
        MyGame.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        world.Draw();
        MyGame.SpriteBatch.End();
    }
}