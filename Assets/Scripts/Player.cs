using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Create a static instance of the Player class
    private static Player _Instance;
    public static Player Instance { get { return _Instance; } }

    // Create a reference to the player's settings
    [Header("Player Settings")]
    [SerializeField] Transform referenceTransform;
    [SerializeField] float playerSpeed;
    [SerializeField] Vector3 defineCameraPosition;
    [SerializeField] Quaternion defineCameraAngle;

    // Create a reference to the player's state
    [Header("Player State")]
    public bool hasFallen;
    public bool hasFinished;

    private Rigidbody rb;
    private Camera playerCamera;
    private float horizontalInput, verticalInput;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Check if there is already an instance of Player
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject); // If there is an instance of Player that is not this, destroy it
        }
        else
        {
            _Instance = this; // If there is no instance of Player, set this as the instance
        }

        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerCamera.transform.SetPositionAndRotation(new Vector3(transform.position.x + defineCameraPosition.x,
            transform.position.y + defineCameraPosition.y,transform.position.z + defineCameraPosition.z), defineCameraAngle);
        CheckPlayerInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckFall(other);
        CheckFinish(other);
    }

    private void CheckPlayerInput()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            verticalInput = Input.GetAxis("Vertical");
        }
    }

    private void CheckFall(Collider other)
    {
        if (other.gameObject.CompareTag("DeathTrigger"))
        {
            hasFallen = true;
        }
    }

    private void CheckFinish(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            hasFinished = true;
        }
    }

    // Move the player based on the player's input
    private void HandleMovement()
    {
        rb.AddForce(playerSpeed * horizontalInput * referenceTransform.right);
        rb.AddForce(playerSpeed * verticalInput * referenceTransform.forward);
    }
}
