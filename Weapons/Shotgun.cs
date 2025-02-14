namespace Simple2DShooter.Weapons;
internal class Shotgun : Weapon
{
    private void CreateRandomBullets(Vector2 position, float angle, int count, float minAngleOffset, float maxAngleOffset, float frequency)
    {
        for (int i = 0; i < count; i++)
        {
            float randomAngle = MathUtilities.random.NextSingle(angle + minAngleOffset, angle + maxAngleOffset);
            if (MathUtilities.random.NextSingle(0f, 1f) < frequency)
                randomAngle = MathUtilities.random.NextSingle(angle + minAngleOffset, angle + minAngleOffset / 2);

            new Bullet(position, randomAngle.ToDirection(), this);
        }
    }

    protected override void OnShoot(Vector2 position)
    {
        var angle = (Input.MousePosition.ToVector() - position).ToAngle();

        CreateRandomBullets(position, angle, 4, -2.5f, 2.5f, 0.7f);
        CreateRandomBullets(position, angle, 3, -15f, 15f, 0.5f);
        CreateRandomBullets(position, angle, 2, -35f, 35f, 0.3f);
    }
}
