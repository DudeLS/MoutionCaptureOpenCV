using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    /// <summary>
    /// Аниматор модели
    /// </summary>
    public Animator ModelAnimator;

    private float headOld;

    private Vector3 defaultPosition;
    private Vector3 defaultRotation;

    void Start()
    {
        headOld = Constants.SliderDefaultValue;

        defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        defaultRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void Update()
    {
        var headController = HeadController.Instance;
        if(headController == null)
        {
            return;
        }

        if (headController.Toggle != null && 
            headController.HeadX != null && 
            headController.Toggle.isOn)
        {
            var head = headController.HeadX.value;
            if(head > 0.5f)
            {
                if (head > headOld)
                {
                    ModelAnimator.enabled = true;
                    ModelAnimator.Play("Left", -1, head - 0.5f);
                }
                else if(head < headOld)
                {
                    ModelAnimator.enabled = true;
                    ModelAnimator.Play("Right", -1, head - 0.5f);
                }
                else
                {
                    //ModelAnimator.Play("None");
                    ModelAnimator.enabled = false;
                }
            }
            else if (head < 0.5f)
            {
                if(head < headOld)
                {
                    ModelAnimator.enabled = true;
                    ModelAnimator.Play("Right", -1, 0.5f - head);
                }
                else if(head > headOld)
                {
                    ModelAnimator.enabled = true;
                    ModelAnimator.Play("Left", -1, 0.5f - head);
                }
                else
                {
                    //ModelAnimator.Play("None");
                    ModelAnimator.enabled = false;
                }                
            }
            else
            {
                //ModelAnimator.Play("None");
                ModelAnimator.enabled = false;
            }
            headOld = head;
        }
        else
        {
            //ModelAnimator.enabled = false;
            transform.eulerAngles = defaultRotation;
            transform.position = defaultPosition;
            //ModelAnimator.Play("None");
        }
    }
}
