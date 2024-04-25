using System.Collections;
using UnityEngine;

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

    void Start()
    {
        animator = GetComponent<Animator>();
        rotationSpeed = 180.0f / timeToRotate;
        StartRotationSequence();
    }

    void Update()
    {
        if (isRotating)
        {
            RotateTeacher();
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
}
