using System.Drawing.Printing;

namespace Simple2DShooter.Weapons;

internal class Knife : Weapon
{
    private class KnifeEffect : GameComponent
    {
        private readonly Vector2 _start, _end, _dir;
        private readonly Weapon _weapon;
        private readonly bool _perfectKill;

        private Vector2 _current;
        private float _alpha = 1f;
        private bool _isHitted = false;

        public KnifeEffect(Vector2 start, Vector2 end, Weapon weapon)
        {
            _start = _current = start;
            _end = end;
            _dir = Vector2.Normalize(end - start);
            _weapon = weapon;
            _perfectKill = MathUtilities.random.Next(1, 100) <= 25;
        }

        public override void Update()
        {
            if (!_isHitted)
                CheckHit();

            if (Vector2.DistanceSquared(_current, _start) < Vector2.DistanceSquared(_start, _end))
                _current += _dir * 250f * IResources.frameTime;

            _alpha -= IResources.frameTime;
            if (_alpha <= 0f)
            {
                Destroy();
                return;
            }
        }

        private void CheckHit()
        {
            foreach (var comp in IResources.gameComponents.ToList())
            {
                if (comp is Enemy enemy && enemy.position.Intersects(enemy.radius, _start, _current))
                {
                    if (_perfectKill)
                        while (!enemy.TakeDamage(_weapon)) ;
                    else
                        enemy.TakeDamage(_weapon);

                    _isHitted = true;
                }
            }
        }

        public override void Draw(Graphics graphics)
        {
            if (_alpha < 0)
                _alpha = 0;

            graphics.DrawLine(new(Color.FromArgb((int)(_alpha * 255f), Color.White), _perfectKill ? 3f : 1f), _start.ToPointF(), _current.ToPointF());
        }
    }

    protected override void OnShoot(Vector2 position)
    {
        var pos = Input.MousePosition.ToVector();
        var dir = MathUtilities.random.NextSingle(0f, 360f).ToDirection() * 15f;
        new KnifeEffect(pos + dir, pos - dir, this);
    }
}