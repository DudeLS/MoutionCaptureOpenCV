using Emgu.CV.Structure;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CamVisualizator : MonoBehaviour
{         
    /// <summary>
    /// Материал
    /// </summary>
    public Material material;

    /// <summary>
    /// 
    /// </summary>
    public Toggle canRender;

    private Texture defaultTexture;

    private bool isDefaultTexture;

    /// <summary>
    /// Метод, выполняемый при старте игры
    /// </summary>
    void Start()
    {
        if(material != null)
        {
            defaultTexture = material.mainTexture;
        }
        isDefaultTexture = true;
    }

    /// <summary>
    /// Метод, выполняемый при обновлении кадра
    /// </summary>
    void Update()
    {
        var colorFinderManager = ColorFinderManager.Instance;        
        if(colorFinderManager == null)
        {
            return;
        }
        var videoCaptureManager = colorFinderManager.VideoCaptureManager;
        if(videoCaptureManager == null)
        {
            return;
        }        
        if (canRender == null)
        {
            return;
        }

        canRender.interactable = colorFinderManager.VideoCaptureManager.IsActive;

        if (!colorFinderManager.VideoCaptureManager.IsActive)
        {
            return;
        }
        var videoCapture = videoCaptureManager.VideoCapture;
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
        //foreach (var circle in colorFinderManager.Circles)
        //{
        //    currentFrame.Draw(circle, new Bgr(System.Drawing.Color.Red), 3);
        //}
        if (colorFinderManager.Circles.Any())
        {
            currentFrame.Draw(colorFinderManager.Circles.First(), new Bgr(System.Drawing.Color.Red), 3);
        }

        if(canRender.isOn)
        {
            var texture2D = new Texture2D(videoCaptureManager.FrameWidth, videoCaptureManager.FrameHeight);
            currentFrame.ToBitmap();
            var memoryStream = new MemoryStream();
            currentFrame.Bitmap.Save(memoryStream, currentFrame.Bitmap.RawFormat);

            texture2D.LoadImage(memoryStream.ToArray());
            material.mainTexture = texture2D;
            isDefaultTexture = false;
        }
        else
        {
            if (!isDefaultTexture)
            {
                material.mainTexture = defaultTexture;
                isDefaultTexture = true;
            }            
        }
    }

    //private int frameWidth;
    //private int frameHeight;
    //private VideoCapture videoCapture;
    //private Image<Bgr, byte> currentFrame;

    //public Material material;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    try
    //    {
    //        videoCapture = new VideoCapture();
    //        frameWidth = (int)videoCapture.GetCaptureProperty(CapProp.FrameWidth);
    //        frameHeight = (int)videoCapture.GetCaptureProperty(CapProp.FrameHeight);
    //        videoCapture.Start();
    //    }
    //    catch(Exception exception)
    //    {
    //        Debug.LogException(exception);
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    var queryFrame = videoCapture.QueryFrame();
    //    if(queryFrame == null)
    //    {
    //        return;
    //    }
    //    currentFrame = queryFrame.ToImage<Bgr, byte>();
    //    if(currentFrame == null)
    //    {
    //        return;
    //    }

    //    var imageProcessed = currentFrame.InRange(new Bgr(100, 0, 0), new Bgr(256, 100, 100));
    //    imageProcessed = imageProcessed.SmoothGaussian(9);
    //    var circles = imageProcessed.HoughCircles(new Gray(100), new Gray(50), 2, imageProcessed.Height / 4, 10, 400)[0];
    //    foreach(var circle in circles)
    //    {
    //        //CvInvoke.Circle(currentFrame, new System.Drawing.Point(circle.Center.X, circle.Center.Y))
    //        currentFrame.Draw(circle, new Bgr(System.Drawing.Color.Red), 3);
    //    }


    //    Texture2D texture2D = new Texture2D(640, 480);
    //    currentFrame.ToBitmap();
    //    var memoryStream = new MemoryStream();
    //    currentFrame.Bitmap.Save(memoryStream, currentFrame.Bitmap.RawFormat);

    //    texture2D.LoadImage(memoryStream.ToArray());
    //    material.mainTexture = texture2D;
    //}

    //private void OnDestroy()
    //{
    //    videoCapture.Dispose();
    //    videoCapture.Stop();
    //}
}
