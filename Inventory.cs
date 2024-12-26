
namespace Simple2DShooter;

internal class Inventory : GameComponent
{
    private List<Weapon> _weapons = new();
    private Weapon _oldWeapon;

    public Weapon CurrentWeapon { get; private set; }

    public Inventory()
    {
        _weapons.Add(new()
        {
            weaponType = WeaponType.Gun,
            cooldown = 0.1f,
            knockback = 75f,
            bulletSpeed = 2000f,
            bulletSize = 1f,
            damageDistance = 250f,
            damage = 15
        });

        _weapons.Add(new()
        {
            weaponType = WeaponType.Shotgun,
            cooldown = 1f,
            knockback = 250f,
            bulletSpeed = 2000f,
            bulletSize = 2.5f,
            damageDistance = 50f,
            damage = 75
        });

        _weapons.Add(new()
        {
            weaponType = WeaponType.Knife,
            cooldown = 0.5f,
            knockback = 0f,
            bulletSpeed = 1000f,
            bulletSize = 0f,
            damageDistance = 0f,
            damage = 50
        });

        CurrentWeapon = _weapons[0];
        _oldWeapon = _weapons[2];
    }

    public override void Update()
    {
        if (CurrentWeapon.weaponType != WeaponType.Gun && Input.IsKeyDown(Keys.D1))
        {
            _oldWeapon = CurrentWeapon;
            CurrentWeapon = _weapons[0];
        }
        if (CurrentWeapon.weaponType != WeaponType.Shotgun && Input.IsKeyDown(Keys.D2))
        {
            _oldWeapon = CurrentWeapon;
            CurrentWeapon = _weapons[1];
        }
        if (CurrentWeapon.weaponType != WeaponType.Knife && Input.IsKeyDown(Keys.D3))
        {
            _oldWeapon = CurrentWeapon;
            CurrentWeapon = _weapons[2];
        }

        if (Input.IsKeyDown(Keys.Q))
        {
            var saveWeapon = CurrentWeapon;
            CurrentWeapon = _oldWeapon;
            _oldWeapon = saveWeapon;
        }
    }

    public override void Draw(Graphics graphics)
    {
        var y = Window.Instance.Bounds.Height - 65f;
        graphics.DrawString("[3] KNIFE", Window.Instance.Font, CurrentWeapon.weaponType == WeaponType.Knife ? Brushes.White : Brushes.DarkGray, 10f, y);
        y -= Window.Instance.Font.Size * 1.5f;
        graphics.DrawString("[2] SHOTGUN", Window.Instance.Font, CurrentWeapon.weaponType == WeaponType.Shotgun ? Brushes.White : Brushes.DarkGray, 10f, y);
        y -= Window.Instance.Font.Size * 1.5f;
        graphics.DrawString("[1] GUN", Window.Instance.Font, CurrentWeapon.weaponType == WeaponType.Gun ? Brushes.White : Brushes.DarkGray, 10f, y);
        y -= Window.Instance.Font.Size * 1.5f;
        graphics.DrawString("[Q] Switch", Window.Instance.Font, Brushes.White, 10f, y);

        graphics.DrawString(CurrentWeapon.killCount.ToString() + (CurrentWeapon.killCount > 1 || CurrentWeapon.killCount < 1 ? " kills" : " kill"), Window.Instance.Font, Brushes.White, Window.Instance.Bounds.Width - 100f, 10f + Window.Instance.Font.Size * 1.5f);
    }
}
