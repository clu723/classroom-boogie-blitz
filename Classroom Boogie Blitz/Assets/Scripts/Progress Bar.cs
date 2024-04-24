using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float FillSpeed = 0.15f;
    private float targetProgress = 0;
    private float leftVelocity = 0;
    private GameObject leftController;
    private InputData _inputData;

    public TextMeshProUGUI leftScoreDisplay;
    public TextMeshProUGUI rightScoreDisplay;
    private float _leftMaxScore = 0f;
    private float _rightMaxScore = 0f;


    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
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

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        {
            _leftMaxScore = Mathf.Max(leftVelocity.magnitude, _leftMaxScore);
            leftScoreDisplay.text = _leftMaxScore.ToString("F2");
        }
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        {
            _rightMaxScore = Mathf.Max(rightVelocity.magnitude, _rightMaxScore);
            rightScoreDisplay.text = _rightMaxScore.ToString("F2");
        }
        float average_velocity = (_leftMaxScore + _rightMaxScore) / 2;
        IncrementProgress(average_velocity);
        
        
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
