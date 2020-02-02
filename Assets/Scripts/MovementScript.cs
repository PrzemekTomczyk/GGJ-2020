using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody m_rb;

    float m_velocity;
    public float m_maxVelocity;
    public float m_rotationSpeed;
    public float m_initialVelocity;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_velocity = 0f;

        if (m_initialVelocity == 0.0f)
        {
            m_initialVelocity = 3f; 
        }
        if (m_maxVelocity == 0.0f)
        {
            m_maxVelocity = 13f; 
        }
        if (m_rotationSpeed == 0.0f)
        {
            m_rotationSpeed = 150f; 
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Rotate the object
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * m_rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.up * m_rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        { 
            // move forward
            if (m_velocity < m_maxVelocity)
            {
                m_velocity += 1f;
            }

            Vector3 velocity = transform.rotation * Vector3.forward;
            velocity *= m_velocity;

            m_rb.velocity = Vector3.Lerp(m_rb.velocity, velocity, Time.deltaTime);
        }
        else
        {
            if (m_velocity > m_initialVelocity)
            { // decriment the velocity if its greater than the initial 
                m_velocity -= 1f;
            }
            else // if it goes below it make it equal
            {
                m_velocity = m_initialVelocity;
            }
        }

    }
}
