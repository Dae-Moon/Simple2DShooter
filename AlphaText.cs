
namespace Simple2DShooter;

internal class AlphaText : GameComponent
{
    public float alpha = 1f;
    public float speed;
    public string text;
    public Vector2 position;
    public Color color;
    public Vector2 direction;

    public AlphaText(float speed, string text, Vector2 position, Color color)
    {
        this.speed = speed;
        this.text = text;
        this.position = position;
        this.color = color;

        direction = MathUtilities.random.NextSingle(0f, 360f).ToDirection();
        this.position += direction * (Window.Instance?.Font.Size ?? 0 * 2f);
    }

    public override void Update()
    {
        alpha -= IResources.frameTime;

        if (alpha <= 0f)
        {
            Destroy();
            return;
        }

        position += direction * speed * IResources.frameTime;
    }

    public override void Draw(Graphics graphics)
    {
        if (alpha <= 0f)
            return;

        graphics.DrawString(text, Window.Instance!.Font, new SolidBrush(Color.FromArgb(Convert.ToInt32(alpha * 255f), color)), position.ToPointF());
    }
}
