using System.Drawing.Printing;

namespace Simple2DShooter;

internal class KnifeEffect : GameComponent
{
    private readonly Vector2 _start, _end;
    private readonly Weapon _weapon;
    private readonly bool _perfectKill;

    private float _alpha = 1f;

    public KnifeEffect(Vector2 start, Vector2 end, Weapon weapon)
    {
        _start = start;
        _end = end;
        _weapon = weapon;
        _perfectKill = Utils.random.Next(1, 100) <= 25;

        CheckHit();
    }

    public override void Update()
    {
        _alpha -= IResources.frameTime;

        if (_alpha <= 0f)
        {
            Destroy();
            return;
        }
    }

    private void CheckHit()
    {
        var pos = (_start + _end) / 2;

        foreach (var comp in IResources.gameComponents.ToList())
        {
            if (comp != null && comp is Enemy)
            {
                var enemy = comp as Enemy;

                bool Intersects(Vector2 position, float radius, Vector2 start, Vector2 end)
                {
                    float dx, dy, A, B, C, det, t;

                    dx = end.X - start.X;
                    dy = end.Y - start.Y;

                    A = dx * dx + dy * dy;
                    B = 2 * (dx * (start.X - position.X) + dy * (start.Y - position.Y));
                    C = (start.X - position.X) * (start.X - position.X) + (start.Y - position.Y) * (start.Y - position.Y) - radius * radius;

                    det = B * B - 4 * A * C;
                    if ((A <= 0.0000001) || (det < 0))
                    {
                        // No real solutions.
                        return false;
                    }
                    else if (det == 0)
                    {
                        // One solution.
                        return true;
                    }
                    else
                    {
                        // Two solutions.
                        return true;
                    }
                };

                if (Intersects(enemy.position, enemy.radius, _start, _end))
                {
                    if (_perfectKill)
                        while (!enemy.TakeDamage(_weapon));
                    else
                        enemy.TakeDamage(_weapon);
                }
            }
        }
    }

    public override void Draw(Graphics graphics)
    {
        if (_alpha < 0)
            _alpha = 0;

        graphics.DrawLine(new(Color.FromArgb((int)(_alpha * 255f), Color.White), _perfectKill ? 3f : 1f), _start.ToPointF(), _end.ToPointF());
    }
}
