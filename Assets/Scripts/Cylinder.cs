using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    // Start is called before the first frame update

    private int randomDirection;

    [SerializeField] float rotationSpeed;

    Rigidbody rb;

    void Start()
    {
        RandomDirection();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //deathRoll.Rotate(Vector3.forward * rotationSpeed * randomDirection * Time.deltaTime);

        
    }

    private void FixedUpdate()
    {
        rb.AddTorque(new Vector3(0, 0, 1) * rotationSpeed);
    }

    private void RandomDirection()
    {
        randomDirection = Random.Range(0, 1);

        if (randomDirection == 0)
        {
            randomDirection = -1;
        }
    }
}
