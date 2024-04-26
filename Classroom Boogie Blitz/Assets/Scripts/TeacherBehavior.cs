using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;


public class TeacherBehavior : MonoBehaviour
{
    public float rotationSpeed;
    public float timeToRotate = 2.0f;
    public float minTimeWriting = 3.0f;
    public float maxTimeWriting = 7.0f;

    private Animator animator;
    public bool isFacingStudents = true;
    private bool isRotating = false;
    private float rotationTimeElapsed = 0;
    private float currentWritingTime = 0;
    private XRController leftController;
    private XRController rightController;

    void Start()
    {
        animator = GetComponent<Animator>();
        rotationSpeed = 180.0f / timeToRotate;
        GameObject leftControllerObject = GameObject.Find("Left Controller");
        GameObject rightControllerObject = GameObject.Find("Right Controller");
        leftController = leftControllerObject.GetComponent<XRController>();
        rightController = rightControllerObject.GetComponent<XRController>();
        StartRotationSequence();
    }

    void Update()
    {
        if (isRotating)
        {
            RotateTeacher();
        }
        if (isFacingStudents && IsPlayerMoving())
        {
            EndGame();
        }
    }

    public void StartRotationSequence()
    {
        StartCoroutine(RotateSequence());
    }

    private IEnumerator RotateSequence()
    {
        while (true)
        {
            currentWritingTime = Random.Range(minTimeWriting, maxTimeWriting);
            isRotating = true;
            yield return new WaitForSeconds(timeToRotate);
            isRotating = false;

            yield return new WaitForSeconds(currentWritingTime);

            isRotating = true;
            yield return new WaitForSeconds(timeToRotate);
            isRotating = false;

            // Trigger the animation when facing the students
            if (isFacingStudents)
            {
                animator.SetTrigger("FaceStudents");

            }
        }
    }

    private void RotateTeacher()
    {
        if (rotationTimeElapsed < timeToRotate)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationStep, 0);
            rotationTimeElapsed += Time.deltaTime;
        }
        else
        {
            isRotating = false;
            rotationTimeElapsed = 0;
            isFacingStudents = !isFacingStudents;
            transform.rotation = Quaternion.Euler(0, isFacingStudents ? 0 : 180, 0);
        }
    }

    private bool IsPlayerMoving()
    {
        bool leftMoving = false;
        bool rightMoving = false;

        if (leftController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftAxis) && leftAxis.magnitude > 0.1f)
        {
            leftMoving = true;
        }

        if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightAxis) && rightAxis.magnitude > 0.1f)
        {
            rightMoving = true;
        }

        return leftMoving || rightMoving;
    }

    private void EndGame()
    {
        SceneManager.LoadScene("Lose");
    }
}
