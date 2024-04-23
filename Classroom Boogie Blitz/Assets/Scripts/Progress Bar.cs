using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float FillSpeed = 0.15f;
    private float targetProgress = 0;
    private LeftController leftController;
    private RightController rightController;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        leftController = gameObject.gameObject.GetComponent<LeftController>();
        rightController = gameObject.gameObject.GetComponent<RightController>();        
        slider.value = 0;
        IncrementProgress(0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value < targetProgress){
            slider.value += FillSpeed * Time.deltaTime;
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
