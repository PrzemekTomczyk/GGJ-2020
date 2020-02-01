using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDevSlave : MonoBehaviour
{
    [SerializeField]
    private float m_rotation;

    [SerializeField]
    private float initRotation;
    [SerializeField]
    private float m_maxLeftRotation;
    [SerializeField]
    private float m_maxRightRotation;
    [SerializeField]
    private float m_rotateDirection;
    [SerializeField]
    private const float ROTATION_SPEED = 180.0f;
    private const float ROTATION_DIFFERENCE = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        //this gives 0 or 1 ----min is inclusive and max is exclusive
        m_rotateDirection = Random.Range(0, 2);

        if (m_rotateDirection == 0)
        {
            m_rotateDirection = -1;
        }

        m_rotation = transform.localRotation.eulerAngles.y;
        initRotation = m_rotation;

        m_maxLeftRotation = m_rotation - ROTATION_DIFFERENCE;
        m_maxRightRotation = m_rotation + ROTATION_DIFFERENCE;

    }

    // Update is called once per frame
    void Update()
    {
        m_rotation += Time.deltaTime * ROTATION_SPEED * m_rotateDirection;

        if (m_rotation < m_maxLeftRotation)
        {
            m_rotateDirection = 1;

            m_rotation = m_maxLeftRotation;
        }
        else if (m_rotation > m_maxRightRotation)
        {
            m_rotateDirection = -1;
            m_rotation = m_maxRightRotation;
        }

        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, m_rotation, 0.0f));
    }
}
