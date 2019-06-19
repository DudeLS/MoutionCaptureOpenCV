using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Менеджер цветоискателя
/// </summary>
public class ColorFinderManager : MonoBehaviour
{
    /// <summary>
    /// Экземпляр
    /// </summary>
    public static ColorFinderManager Instance = null;
    
    /// <summary>
    /// Группа чекбоксов со цветом
    /// </summary>
    public ToggleGroup ColorToggleGroup;

    /// <summary>
    /// Список точек
    /// </summary>
    [HideInInspector]
    public List<CircleF> Circles = new List<CircleF>();

    /// <summary>
    /// Менеджер захвата видео с камеры
    /// </summary>
    [HideInInspector]
    public VideoCaptureManager VideoCaptureManager = null ;

    private int frame = 0;

    /// <summary>
    /// Метод, выполняемый при старте игры
    /// </summary>
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

    /// <summary>
    /// Метод, выполняемый при обновлении кадра
    /// </summary>
    void Update()
    {
        frame++;
        if(frame % 2 != 0)
        {
            return;
        }
        frame = 0;

        Circles.Clear();

        VideoCaptureManager = VideoCaptureManager.Instance;

        if (!VideoCaptureManager.IsActive)
        {
            ColorToggleGroup.DisableChilds();
            return;
        }
        ColorToggleGroup.EnableChilds();
        var videoCapture = VideoCaptureManager.VideoCapture;
        if(videoCapture == null)
        {
            return;
        }
        var queryFrame = videoCapture.QueryFrame();
        if (queryFrame == null)
        {
            return;
        }
        var currentFrame = queryFrame.ToImage<Bgr, byte>();
        if (currentFrame == null)
        {
            return;
        }
        var toggle = GetActiveToggle();
        if(toggle == null)
        {
            return;
        }
        Image<Gray, byte> imageProcessed = null;
        var color = toggle.name;
        var setColor = false;
        switch(color)
        {
            case "BlueColorToggle":
                imageProcessed = currentFrame.InRange(new Bgr(100, 0, 0), new Bgr(256, 100, 100));
                setColor = true;
                break;
            case "GreenColorToggle":
                setColor = false;
                break;
            case "RedColorToggle":
                setColor = true;
                imageProcessed = currentFrame.InRange(new Bgr(0, 0, 175), new Bgr(100, 100, 256));
                break;
            default:
                setColor = false;
                break;
        }
        if (!setColor)
        {
            return;
        }
        imageProcessed = imageProcessed.SmoothGaussian(9);
        Circles = imageProcessed.HoughCircles(new Gray(100), new Gray(50), 2, imageProcessed.Height / 4, 10, 400)[0].ToList();
    }

    private Toggle GetActiveToggle()
    {
        return ColorToggleGroup.ActiveToggles().FirstOrDefault();
    }

    /// <summary>
    /// Инициализация менеджера
    /// </summary>
    private void Initialize()
    {
        
    }
}
