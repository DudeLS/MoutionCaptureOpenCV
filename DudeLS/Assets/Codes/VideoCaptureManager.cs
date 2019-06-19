using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Менеджер захвата видео с камеры
/// </summary>
public class VideoCaptureManager : MonoBehaviour
{
    public Toggle ActivateToggle;

    /// <summary>
    /// Экземпляр
    /// </summary>
    public static VideoCaptureManager Instance = null;

    /// <summary>
    /// Видео
    /// </summary>
    [HideInInspector]
    public VideoCapture VideoCapture = null;

    /// <summary>
    /// Активен
    /// </summary>
    [HideInInspector]
    public bool IsActive = false;

    /// <summary>
    /// Ширина кадра
    /// </summary>
    [HideInInspector]
    public int FrameWidth = 0;

    /// <summary>
    /// Высота кадра
    /// </summary>
    [HideInInspector]
    public int FrameHeight = 0;

    /// <summary>
    /// Метод, выполняемый при старте игры
    /// </summary>
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if (ActivateToggle != null && ActivateToggle.isOn)
        {
            Activate();
        }
    }

    void Update()
    {
        if(ActivateToggle != null && ActivateToggle.isOn)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    /// <summary>
    /// Метод, выполняемый при выходе из игры
    /// </summary>
    private void OnDestroy()
    {
        if (IsActive)
        {
            Deactivate();
        }        
    }

    /// <summary>
    /// Инициализация менеджера
    /// </summary>
    private void Activate()
    {
        if (IsActive)
        {
            return;
        }

        try
        {
            VideoCapture = new VideoCapture();
            FrameWidth = (int)VideoCapture.GetCaptureProperty(CapProp.FrameWidth);
            FrameHeight = (int)VideoCapture.GetCaptureProperty(CapProp.FrameHeight);
            VideoCapture.Start();
            IsActive = true;
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private void Deactivate()
    {
        if (!IsActive)
        {
            return;
        }

        VideoCapture.Dispose();
        VideoCapture.Stop();
        IsActive = false;
    }
}