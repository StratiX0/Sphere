using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public static Player instance { get { return _instance; } }

    [SerializeField] Transform transformReference;

    Transform playerSpawn;

    Rigidbody rb;

    Camera cam;

    [SerializeField] float speed;

    private float horizontal, vertical;

    public bool fall, finish = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
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
            fall = true;
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            finish = true;
        }
    }
}
