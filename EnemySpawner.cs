﻿

namespace Simple2DShooter;

internal class EnemySpawner : GameComponent
{
    private DateTime _lastSpawnTime;
    private float _randomTime = 0;
    private int _enemyCount = 0;

    public EnemySpawner()
    {
        _randomTime = MathUtilities.random.NextSingle(0f, 5f);
    }

    public override void Update()
    {
        _enemyCount = GetEnemyCount();
        if (_enemyCount >= 10)
            return;

        var now = DateTime.Now;
        var left = (float)(now - _lastSpawnTime).TotalSeconds;

        if (left > _randomTime)
        {
            var sp = MathUtilities.random.NextSingle(25, 250);
            var pos = MathUtilities.RandomVector(Vector2.Zero, Window.Instance!.Bounds.Size.ToVector());
            var rad = MathUtilities.random.NextSingle(5f, 15f);

            if ((pos - Player.LocalPlayer.position).Length() > Player.LocalPlayer.radius + rad + sp * 5f)
            {
                new Enemy(MathUtilities.random.Next(35, 100))
                {
                    speed = sp,
                    position = pos,
                    radius = rad,
                    direction = MathUtilities.random.NextSingle(0, 360).ToDirection(),
                };
            }
            _lastSpawnTime = now;
            _randomTime = MathUtilities.random.NextSingle(0f, 5f);
        }
    }

    public override void Draw(Graphics graphics)
    {
        graphics.DrawString(_enemyCount.ToString() + (_enemyCount > 1 ? " Enemies" : " Enemy"), Window.Instance!.Font, Brushes.White, Window.Instance.Bounds.Width - 100, 10);
    }

    private int GetEnemyCount()
    {
        int result = 0;
        foreach (var comp in IResources.gameComponents)
            if (comp != null && comp is Enemy)
                result++;

        return result;
    }
}