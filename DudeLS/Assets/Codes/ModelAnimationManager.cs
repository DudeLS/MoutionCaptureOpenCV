using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModelAnimationManager : MonoBehaviour
{
    public enum Horisontal
    {
        Left,
        Center,
        Right
    }

    public enum Vertical
    {
        Top,
        Bottom
    }

    /// <summary>
    /// Аниматор модели
    /// </summary>
    public Animator ModelAnimator;

    /// <summary>
    /// Положение по умолчанию
    /// </summary>
    private Vector3 defaultPosition;

    /// <summary>
    /// Положение по умолчанию
    /// </summary>
    private Vector3 defaultRotation;

    ///// <summary>
    ///// 
    ///// </summary>
    //private float headOldValueX;

    /// <summary>
    /// 
    /// </summary>
    private Horisontal headPrevios;

    /// <summary>
    /// 
    /// </summary>
    private Vertical mouthPrevios;

    private Vector3 rotationPrevios;

    private Vector3 defaultEulerAngles;

    void Start()
    {
        //defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //defaultRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        headPrevios = Horisontal.Center;

        mouthPrevios = Vertical.Top;

        defaultEulerAngles = transform.GetChild(0).eulerAngles;
    }

    void Update()
    {
        Head();
        Mouth();
        Eyes();
    }

    private Horisontal? GetHorisontal(float value)
    {

        if (value <= Constants.SliderMaxValue &&
            value > Constants.SliderMaxValue - Constants.SliderOffset)
        {
            //left (0.9, 1]
            return Horisontal.Left;
        }
        else if (value >= Constants.SliderMinValue &&
            value < Constants.SliderMinValue + Constants.SliderOffset)
        {
            //right (0.1, 0]
            return Horisontal.Right;
        }
        else if (value >= Constants.SliderDefaultValue - Constants.SliderOffset &&
            value <= Constants.SliderDefaultValue + Constants.SliderOffset)
        {
            //center [0.4, 0.6]
            return Horisontal.Center;
        }
        else
        {
            return null;
        }
    }

    private Vertical? GetVertical(float value)
    {
        if (value <= Constants.SliderMaxValue &&
            value > Constants.SliderMaxValue - Constants.SliderOffset)
        {
            //left (0.9, 1]
            return Vertical.Bottom;
        }
        else if (value >= Constants.SliderMinValue &&
            value < Constants.SliderMinValue + Constants.SliderOffset)
        {
            //right (0.1, 0]
            return Vertical.Top;
        }
        else
        {
            return null;
        }
    }

    private void Head()
    {
        var headController = HeadController.Instance;
        if (headController == null)
        {
            return;
        }

        if (headController.Toggle == null ||
            headController.HeadX == null)
        {
            return;
        }

        if (headController.Toggle.isOn)
        {
            var headValueX = headController.HeadX.normalizedValue;
            var headHorisontal = GetHorisontal(headValueX);

            switch (headHorisontal)
            {
                case Horisontal.Left:
                    if (headPrevios == Horisontal.Center)
                    {
                        ModelAnimator.Play("Left", -1, 0f);
                    }
                    break;
                case Horisontal.Center:
                    if (headPrevios == Horisontal.Left)
                    {
                        ModelAnimator.Play("Right", -1, 0f);
                    }
                    else if (headPrevios == Horisontal.Right)
                    {
                        ModelAnimator.Play("Left", -1, 0f);
                    }
                    break;
                case Horisontal.Right:
                    if (headPrevios == Horisontal.Center)
                    {
                        ModelAnimator.Play("Right", -1, 0f);
                    }
                    break;
            }

            if (headHorisontal.HasValue)
            {
                headPrevios = headHorisontal.Value;
            }            
        }
        else
        {
            if(headPrevios == Horisontal.Left)
            {
                ModelAnimator.Play("Right", -1, 0f);
                headPrevios = Horisontal.Center;
            }
            else if(headPrevios == Horisontal.Right)
            {
                ModelAnimator.Play("Left", -1, 0f);
                headPrevios = Horisontal.Center;
            }            
        }
    }

    private void Mouth()
    {
        var mouthController = MouthController.Instance;
        if (mouthController == null)
        {
            return;
        }

        if (mouthController.Toggle == null ||
            mouthController.MouthY == null)
        {
            return;
        }

        if (mouthController.Toggle.isOn)
        {
            var mouthValueY = mouthController.MouthY.normalizedValue;
            var mouthVertical = GetVertical(mouthValueY);

            switch (mouthVertical)
            {
                case Vertical.Top:
                    if (mouthPrevios == Vertical.Bottom)
                    {
                        ModelAnimator.Play("Top", -1, 0f);
                    }
                    break;
                case Vertical.Bottom:
                    if (mouthPrevios == Vertical.Top)
                    {
                        ModelAnimator.Play("Bottom", -1, 0f);
                    }
                    break;
            }

            if (mouthVertical.HasValue)
            {
                mouthPrevios = mouthVertical.Value;
            }
        }
        else
        {
            if (mouthPrevios == Vertical.Bottom)
            {
                ModelAnimator.Play("Top", -1, 0f);
                mouthPrevios = Vertical.Top;
            }
        }
    }

    private void Eyes()
    {
        var eyesController = EyesController.Instance;
        if (eyesController == null)
        {
            return;
        }

        if (eyesController.Toggle == null ||
            eyesController.EyasX == null ||
            eyesController.EyasY == null)
        {
            return;
        }
        
        var eyeTransform = transform.GetChild(0);
        var eye2Transform = transform.GetChild(1);

        if (eyeTransform == null)
        {
            return;
        }

        if (eyesController.Toggle.isOn)
        {
            var x = (eyesController.EyasX.value - Constants.SliderDefaultValue) * 20f;
            //var y = (eyesController.EyasY.value - Constants.SliderDefaultValue) * 20f;

            var currentRotation = eyeTransform.eulerAngles;

            rotationPrevios = currentRotation;

            var newRotation = new Vector3(
                defaultEulerAngles.x,
                defaultEulerAngles.y + x,
                defaultEulerAngles.z /*+ y*/);

            if (newRotation != rotationPrevios)
            {
                eyeTransform.eulerAngles = newRotation;
                eye2Transform.eulerAngles = newRotation;
                rotationPrevios = newRotation;
            }

            //eyeRightTransform.eulerAngles = new Vector3(eyesController.EyasX.value * 100f, eyesController.EyasY.value * 100f, 0f);
        }
        else
        {
            eyeTransform.eulerAngles = defaultEulerAngles;
            eye2Transform.eulerAngles = defaultEulerAngles;
        }
    }

    //private void SetDefaultPosition()
    //{
    //    transform.eulerAngles = defaultRotation;
    //    transform.position = defaultPosition;
    //}
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;

//public class ModelAnimationManager : MonoBehaviour
//{
//    public enum Horisontal
//    {
//        Left,
//        Center,
//        Right
//    }

//    public enum Vertical
//    {
//        Top,
//        Center,
//        Bottom
//    }

//    /// <summary>
//    /// Аниматор модели
//    /// </summary>
//    public Animator ModelAnimator;

//    /// <summary>
//    /// Положение по умолчанию
//    /// </summary>
//    private Vector3 defaultPosition;

//    /// <summary>
//    /// Положение по умолчанию
//    /// </summary>
//    private Vector3 defaultRotation;

//    /// <summary>
//    /// 
//    /// </summary>
//    private float headOldValueX;

//    /// <summary>
//    /// 
//    /// </summary>
//    private Horisontal headPrevios;

//    void Start()
//    {
//        defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
//        defaultRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

//        headOldValueX = Constants.SliderDefaultValue;

//        headPrevios = Horisontal.Center;
//    }

//    void Update()
//    {
//        Head();
//    }

//    private Horisontal? GetHorisontal(float value)
//    {

//        if (value <= Constants.SliderMaxValue && 
//            value > Constants.SliderMaxValue - Constants.SliderOffset)
//        {
//            //left (0.9, 1]
//            return Horisontal.Left;
//        }
//        else if (value >= Constants.SliderMinValue && 
//            value < Constants.SliderMinValue + Constants.SliderOffset)
//        {
//            //right (0.1, 0]
//            return Horisontal.Right;
//        }
//        else if(value >= Constants.SliderDefaultValue - Constants.SliderOffset && 
//            value <= Constants.SliderDefaultValue + Constants.SliderOffset)
//        {
//            //center [0.4, 0.6]
//            return Horisontal.Center;
//        }
//        else
//        {
//            return null;
//        }
//    }

//    private void Head()
//    {
//        var headController = HeadController.Instance;
//        if (headController == null)
//        {
//            return;
//        }

//        if (headController.Toggle == null ||
//            headController.HeadX == null)
//        {
//            return;
//        }

//        if (headController.Toggle.isOn)
//        {
//            var headValueX = headController.HeadX.normalizedValue;
//            var headHorisontal = GetHorisontal(headValueX);

//            switch (headHorisontal)
//            {
//                case Horisontal.Left:
//                    if(headPrevios == Horisontal.Center)
//                    {
//                        ModelAnimator.Play("Left", -1, 0f);
//                    }
//                    else if (headPrevios == Horisontal.Right)
//                    {
//                        ModelAnimator.Play("Left", -1, 0f);
//                    }
//                    else
//                    {
//                        //ModelAnimator.Play("None");
//                    }
//                    break;
//                case Horisontal.Center:
//                    if(headPrevios == Horisontal.Left)
//                    {
//                        ModelAnimator.Play("Right", -1, 0f);
//                    }
//                    else if (headPrevios == Horisontal.Right)
//                    {
//                        ModelAnimator.Play("Left", -1, 0f);
//                    }
//                    else
//                    {
//                        //ModelAnimator.Play("None");
//                    }
//                    break;
//                case Horisontal.Right:
//                    if (headPrevios == Horisontal.Center)
//                    {
//                        ModelAnimator.Play("Right", -1, 0f);
//                    }
//                    else if (headPrevios == Horisontal.Left)
//                    {
//                        ModelAnimator.Play("Right", -1, 0f);
//                    }
//                    else
//                    {
//                        //ModelAnimator.Play("None");
//                    }
//                    break;
//                default:
//                    //ModelAnimator.Play("None");
//                    break;
//            }

//            if (headHorisontal.HasValue)
//            {
//                headPrevios = headHorisontal.Value;
//            }

//            #region comment

//            //if (headValueX > Constants.SliderDefaultValue)
//            //{
//            //    if (headValueX > headOldValueX)
//            //    {
//            //        SetDefaultPosition();
//            //        ModelAnimator.speed = 1f;
//            //        ModelAnimator.enabled = true;
//            //        float animationPosition = (1f/20f) * (headValueX * 10f);// headValueX - Constants.SliderDefaultValue;
//            //        ModelAnimator.Play("Left", -1, animationPosition);
//            //        //ModelAnimator.CrossFade("Left", 0f, -1, 0f, 0f);

//            //        //ModelAnimator.speed = 0f;
//            //    }
//            //    else if (headValueX < headOldValueX)
//            //    {
//            //        SetDefaultPosition();

//            //        ModelAnimator.enabled = true;
//            //        var animationPosition = headValueX - Constants.SliderDefaultValue;
//            //        ModelAnimator.Play("Left", -1, animationPosition);
//            //    }
//            //    else
//            //    {
//            //        //ModelAnimator.Play("None");
//            //        //ModelAnimator.speed = 0f;
//            //        //ModelAnimator.enabled = false;
//            //    }
//            //}
//            //else if (headValueX < Constants.SliderDefaultValue)
//            //{
//            //    if (headValueX < headOldValueX)
//            //    {
//            //        SetDefaultPosition();

//            //        ModelAnimator.enabled = true;
//            //        var animationPosition = Constants.SliderDefaultValue - headValueX;
//            //        ModelAnimator.Play("Right", -1, animationPosition);
//            //    }
//            //    else if (headValueX > headOldValueX)
//            //    {
//            //        SetDefaultPosition();

//            //        ModelAnimator.enabled = true;
//            //        var animationPosition = Constants.SliderDefaultValue - headValueX;
//            //        ModelAnimator.Play("Right", -1, animationPosition);
//            //    }
//            //    else
//            //    {
//            //        //ModelAnimator.Play("None");
//            //        ModelAnimator.enabled = false;
//            //    }
//            //}
//            //else
//            //{
//            //    //ModelAnimator.Play("None");


//            //    //ModelAnimator.enabled = false;
//            //}
//            //headOldValueX = headValueX;

//            #endregion
//        }
//        else
//        {
//            //ModelAnimator.enabled = false;

//            SetDefaultPosition();

//            headPrevios = Horisontal.Center;            

//            //ModelAnimator.Play("None");
//        }
//    }

//    private void Mouth()
//    {

//    }

//    private void Eyes()
//    {

//    }

//    private void SetDefaultPosition()
//    {
//        transform.eulerAngles = defaultRotation;
//        transform.position = defaultPosition;
//    }
//}
