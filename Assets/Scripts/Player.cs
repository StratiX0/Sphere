using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform transformReference;

    Transform playerSpawn;

    Rigidbody rb;

    Camera cam;

    [SerializeField] float speed;

    private float horizontal, vertical;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        playerSpawn = GameObject.FindWithTag("Spawn").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.position = playerSpawn.position;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.rotation = Quaternion.Euler(15, 0, 0);
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 5);

        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            vertical = Input.GetAxis("Vertical");
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transformReference.right * horizontal * speed);
        rb.AddForce(transformReference.forward * vertical * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathTrigger"))
        {
            transform.position = playerSpawn.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
