namespace Simple2DShooter;

internal interface IInputable
{
    protected static Point mousePosition = new(0, 0);
    protected static Dictionary<MouseButtons, bool> mouse;
    protected static Dictionary<Keys, bool> keys;
}

internal class Input : IInputable
{
    public static Point MousePosition => IInputable.mousePosition;
    protected static Dictionary<MouseButtons, bool> _lastMouse = new();
    protected static Dictionary<Keys, bool> _lastKeys = new();

    public static void Initialize()
    {
        foreach (var mouse in Enum.GetValues<MouseButtons>())
            if (!_lastMouse.ContainsKey(mouse))
                _lastMouse.Add(mouse, false);

        foreach (var key in Enum.GetValues<Keys>())
            if (!_lastKeys.ContainsKey(key))
                _lastKeys.Add(key, false);

        IInputable.mouse = new(_lastMouse);
        IInputable.keys = new(_lastKeys);
    }

    public static void Update()
    {
        _lastMouse = new(IInputable.mouse);
        _lastKeys = new(IInputable.keys);
    }

    public static bool IsKeyDown(Keys key)
    {
        return IInputable.keys[key] && !_lastKeys[key];
    }

    public static bool IsKey(Keys key)
    {
        return IInputable.keys[key];
    }

    public static bool IsKeyUp(Keys key)
    {
        return !IInputable.keys[key] && _lastKeys[key];
    }

    public static bool IsMouseDown(MouseButtons mouse)
    {
        return IInputable.mouse[mouse] && !_lastMouse[mouse];
    }

    public static bool IsMouse(MouseButtons mouse)
    {
        return IInputable.mouse[mouse];
    }

    public static bool IsMouseUp(MouseButtons mouse)
    {
        return !IInputable.mouse[mouse] && _lastMouse[mouse];
    }
}
