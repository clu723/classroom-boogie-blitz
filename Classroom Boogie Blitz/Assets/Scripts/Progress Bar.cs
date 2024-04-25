using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float fillSpeed = 1f; // This will be dynamically adjusted by controller speed
    [SerializeField] private float decreaseRate = 0.1f; // Rate at which the progress decreases when not moving

    private TeacherBehavior teacherBehavior;
    private InputData _inputData;
    private float currentVelocity = 0f;

    private void Start()
    {
        teacherBehavior = FindObjectOfType<TeacherBehavior>(); // Ensure the TeacherBehavior script is on the teacher GameObject
        _inputData = GetComponent<InputData>(); // Assuming the InputData is attached to the same GameObject as this script
        ResetProgress();
    }

    private void Update()
    {
        if (teacherBehavior && !teacherBehavior.isFacingStudents)
        {
            UpdateProgressBasedOnMovement();
        }
        else
        {
            DecreaseProgress();
        }

        UpdateProgressBar();
    }

    private void UpdateProgressBasedOnMovement()
    {
        float leftVelocity = 0f, rightVelocity = 0f;
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftControllerVelocity))
        {
            leftVelocity = leftControllerVelocity.magnitude;
        }
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightControllerVelocity))
        {
            rightVelocity = rightControllerVelocity.magnitude;
        }

        currentVelocity = (leftVelocity + rightVelocity) / 2; // Average velocity of both controllers
        slider.value += currentVelocity * fillSpeed * Time.deltaTime; // Increase progress bar according to the velocity
    }

    private void DecreaseProgress()
    {
        if (slider.value > 0)
        {
            slider.value -= decreaseRate * Time.deltaTime; // Slowly decrease the progress bar when not moving
        }
    }

    private void UpdateProgressBar()
    {
        slider.value = Mathf.Clamp(slider.value, 0, slider.maxValue); // Ensure the progress bar value stays within valid bounds
    }

    public void ResetProgress()
    {
        slider.value = 0; // Reset the progress bar at the start of the game
    }
}
