using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float rotationSpeed = 10.0f;

    public bool isFirstPerson = true;
    private bool isGrounded;
    private Rigidbody rb;

    [Header("Camera Setting")]
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float mouseSenesivity = 2.0f;

    public float radius = 5.0f;
    public float minRadius = 1.0f;
    public float maxRadius = 10.0f;

    public float yMinLimit = -90;
    public float yMaxLimit = 90;

    private float theta = 0.0f;
    private float phi = 0.0f;
    private float targetVerticalRotation = 0;
    private float verticalRotationSpeed = 240f;

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement;

        if (!isFirstPerson)
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();

            Vector3 cameraRight = thirdPersonCamera.transform.right;
            cameraRight.y = 0.0f;
            cameraRight.Normalize();

            movement = cameraRight * moveHorizontal + cameraForward * moveVertical;
        }
        else
        {
            movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        }

        if (movement.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        rb.MovePosition(rb.position + movement * Time.deltaTime);   
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesivity;

        theta += mouseX;
        theta = Mathf.Repeat(theta, 360.0f);

        targetVerticalRotation -= mouseY;
        targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, yMinLimit, yMaxLimit);
        phi = Mathf.MoveTowards(phi, targetVerticalRotation, verticalRotationSpeed * Time.deltaTime);

        if (isFirstPerson)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);

            transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);
        }
        else
        {
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
            thirdPersonCamera.transform.LookAt(transform);

            radius = Mathf.Clamp(radius - Input.GetAxis("MouseScrollWheel") * 5, minRadius, maxRadius);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        SetupCameras();
        SetActiveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleRotation();
        HandleCameraToggle();   
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0.0f, 0.6f, 0.0f);
        firstPersonCamera.transform.localRotation = Quaternion.identity;
    }

    void HandleCameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;
            SetActiveCamera();
        }
    }

    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);

    }
}
