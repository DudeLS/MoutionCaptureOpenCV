using System.Linq;
using UnityEngine.UI;

public class MouthController : Controller
{
    /// <summary>
    /// Экземпляр
    /// </summary>
    public static MouthController Instance = null;

    /// <summary>
    /// Рот Y
    /// </summary>
    public Slider MouthY;

    /// <summary>
    /// Текущий чекбокс
    /// </summary>
    public Toggle Toggle;

    /// <summary>
    /// Менеджер цветоискателя
    /// </summary>
    private ColorFinderManager _colorFinderManager;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (/*_toggle*/Toggle.isOn)
        {
            MouthY.Enable(0f);
            _colorFinderManager = ColorFinderManager.Instance;
            if (!_colorFinderManager.Circles.Any())
            {
                return;
            }
            var point = _colorFinderManager.Circles.First();
            var height = _colorFinderManager.VideoCaptureManager.FrameHeight;
            MouthY.value = point.Center.Y / height;
        }
        else
        {
            MouthY.Disable(0f);
        }
    }

    public override void Disable()
    {
        
    }

    public override void Enable()
    {
        
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Initialize()
    {
        
    }    
}
