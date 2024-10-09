using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;

class InputNode : Node
{
    public string text = "";
    private bool isFocused = false;
    public EventHandler<string> OnInput = (object sender, string text) => { };
    public int maxLength = 5;
    public string placeholder = "仅可输入字母a-z";
    public new Vector2 Size = new(200, 50);
    private readonly LabelNode _labelNode = new() { color = new(150, 150, 150) };

    public InputNode()
    {
        AddChild(_labelNode);

        OnClick += (object sender, Vector2 mousePos) =>
        {
            isFocused = true;
        };

        OnOutSideClick += (object sender, Vector2 mousePos) =>
        {
            isFocused = false;
        };
    }

    public override void Update(GameTime gameTime)
    {
        if (isFocused)
        {
            foreach (var key in MyGame.KeyboardState.GetPressedKeys())
            {
                if (MyGame.KeyboardState.WasKeyPressed(key))
                {

                    if (key == Keys.Back && text.Length > 0)
                    {
                        text = text[..^1];
                    }

                    if (IsValidKey(key) && text.Length < maxLength)
                    {
                        text += key.ToString().ToLower().Trim();
                        OnInput.Invoke(this, text);
                    }
                }
            }
        }

        if (text == "" && !isFocused)
        {
            _labelNode.text = placeholder;
            _labelNode.color = new(150, 150, 150);
        }
        else
        {
            _labelNode.text = text;
            _labelNode.color = Color.Black;
        }

        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.FillRectangle(rectangle: Rectangle, color: isFocused ? color : color * 0.8f, layerDepth: layerDepth);
        MyGame.SpriteBatch.DrawRectangle(rectangle: Rectangle, color: isFocused ? Color.Green : Color.Black, thickness: 2, layerDepth: layerDepth);
        base.Draw();
    }

    private static bool IsValidKey(Keys key)
    {
        return key >= Keys.A && key <= Keys.Z;
    }

    public override Vector2 GetSize()
    {
        return Size;
    }
}
