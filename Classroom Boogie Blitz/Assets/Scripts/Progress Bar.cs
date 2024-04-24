using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float FillSpeed = 0.15f;
    private float targetProgress = 0;
    private float leftVelocity = 0;
    private GameObject leftController;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
               
        slider.value = 0;
        leftController = GameObject.Find("LeftController");
        IncrementProgress(0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value < targetProgress){
            slider.value += FillSpeed * Time.deltaTime;
        }

        //this is the velocity
        /*
        get position on this frame.
        Add it to list
        average the list with a velocity function 
        
        ballSpeed = transform.position - lastPos;
        ballSpeed /= Time.deltaTime;
        lastPos = transform.position;
        */


    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
