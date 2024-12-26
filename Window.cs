namespace Simple2DShooter;

public partial class Window : Form, IInputable
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public static Window Instance { get; private set; } = null;

    private readonly Thread _updateThread;
    private DateTime _lastUpdateTime = DateTime.Now;
    private DateTime _lastScreenTime = DateTime.Now;
    private bool isRunning = false;

    EnemySpawner _spawner = new();
    Player _player = new()
    {
        position = new(20f, 20f),
        speed = 250f,
        radius = 10f
    };

    public Window()
    {
        Instance = this;

        InitializeComponent();

        _updateThread = new(Update);
    }

    private void Update()
    {
        while (isRunning)
        {
            Thread.Sleep(1);

            var now = DateTime.Now;
            IResources.frameTime = (float)(now - _lastUpdateTime).TotalSeconds;
            _lastUpdateTime = now;

            foreach (var comp in IResources.gameComponents.ToList())
                comp?.Update();
            
            //Invoke(() => { IInputable.mousePosition = PointToClient(Cursor.Position); });
            Input.Update();
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        Input.Initialize();

        foreach (var comp in IResources.gameComponents.ToList())
            comp?.Initialize();

        isRunning = true;
        _updateThread.Start();
        base.OnLoad(e);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        isRunning = false;
        base.OnClosing(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var now = DateTime.Now;
        var frameTime = (now - _lastScreenTime).TotalSeconds;
        _lastScreenTime = now;

        e.Graphics.Clear(Color.Black);

        foreach (var comp in IResources.gameComponents.ToList())
            comp?.Draw(e.Graphics);
        
        e.Graphics.DrawString($"{(int)(1f / frameTime)} FPS", Font, Brushes.White, 10, 10);
        e.Graphics.DrawString($"Frame time: {IResources.frameTime}", Font, Brushes.White, 10f, 10f + Font.Size * 1.5f);
        
        IInputable.mousePosition = PointToClient(Cursor.Position);

        Invalidate();
        base.OnPaint(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        IInputable.mouse[e.Button] = true;
        base.OnMouseDown(e);
    }


    protected override void OnMouseUp(MouseEventArgs e)
    {
        IInputable.mouse[e.Button] = false;
        base.OnMouseUp(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        IInputable.keys[e.KeyCode] = true;
        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        IInputable.keys[e.KeyCode] = false;
        base.OnKeyUp(e);
    }
}