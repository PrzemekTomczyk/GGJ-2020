using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    Rigidbody m_rb;

    float m_velocity;
    public float m_maxVelocity;
    public float m_rotationSpeed;
    Quaternion m_direction;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_velocity = 0f;
        m_maxVelocity = 15f;
        m_rotationSpeed = 150f;
        m_direction = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * m_rotationSpeed * Time.deltaTime);
            m_maxVelocity = 14f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * m_rotationSpeed * Time.deltaTime);
            m_maxVelocity = 14f;
        }
        else
        {
            m_maxVelocity = 15f;
        }
        if (Input.GetKey(KeyCode.W))
        { // move forward
            m_velocity = m_maxVelocity;

            Vector3 velocity = transform.rotation * Vector3.forward;
            velocity *= m_velocity;

            m_rb.velocity = Vector3.Lerp(m_rb.velocity, velocity, Time.deltaTime);
        }

    }
}
