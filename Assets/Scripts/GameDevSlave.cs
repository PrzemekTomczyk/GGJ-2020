using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDevSlave : MonoBehaviour
{
    enum SlaveWorkType
    {
        Start = 0,
        Work = 1,
        Bug = 2,
        Error = 3,
        Boosted = 4,
        Finished = 5
    }

    [SerializeField]
    private float m_rotation;
    private float m_rotateDirection;

    public float m_maxLeftRotation;
    public float m_maxRightRotation;

    private const float WORK_ROTATION_SPEED = 180.0f;
    private const float BOOST_WORK_ROTATION_SPEED = 250.0f;
    private const float BUG_ROTATION_SPEED = WORK_ROTATION_SPEED / 2.0f;
    private const int NO_ROTATION_SPEED = 0;
    private const float ROTATION_DIFFERENCE = 15.0f;
    private const int WORK_TIME = 10000;

    [SerializeField]
    private SlaveWorkType m_workType;

    //variables for boosting slaves
    private bool m_boostedWork;
    private bool m_needsCooldown;
    private float BOOST_TIME = 4.0f;
    private float BOOST_COOLDOWN_TIME = 2.5f;
    private float m_timer = 0.0f;

    //variables to handle errored slaves
    private bool m_erroredSlave;
    private float ERROR_TIME = 10.0f;
    private float MAX_TIME_BEFORE_NEXT_ERROR = 20.0f;

    private Canvas m_gameCanvas;
    public Image m_radialBar;
    private Image m_progressBar;
    private ProgressBar m_progressScript;
    public Color m_workColour;
    public Color m_bugColour;
    public Color m_boostColour;
    public Color m_errorColour;



    // Start is called before the first frame update
    void Start()
    {
        m_boostedWork = false;
        m_needsCooldown = true;
        m_gameCanvas = FindObjectOfType<Canvas>();
        m_workType = SlaveWorkType.Start;

        //this gives 0 or 1 ----min is inclusive and max is exclusive
        m_rotateDirection = Random.Range(0, 2);

        if (m_rotateDirection == 0)
        {
            m_rotateDirection = -1;
        }

        m_rotation = transform.localRotation.eulerAngles.y;

        m_maxLeftRotation = m_rotation - ROTATION_DIFFERENCE;
        m_maxRightRotation = m_rotation + ROTATION_DIFFERENCE;

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_boostedWork)
        {
            RollIfGameBreaks();
        }

        if (m_workType != SlaveWorkType.Start && m_workType != SlaveWorkType.Error)
        {
            m_timer += Time.deltaTime;
        }
        else
        {
            m_boostedWork = false;
            m_timer = 0.0f;
        }
        //handle boosted work
        if (m_boostedWork && m_timer >= BOOST_TIME)
        {
            m_boostedWork = false;
            m_needsCooldown = true;
            m_workType = SlaveWorkType.Work;
            m_timer = 0.0f;
        }
        else if (m_needsCooldown && m_timer >= BOOST_COOLDOWN_TIME)
        {
            m_needsCooldown = false;
            m_timer = 0.0f;
        }

        switch (m_workType)
        {
            case SlaveWorkType.Work:
                m_progressScript.m_colour = m_workColour;
                HandleSlaveWork(WORK_ROTATION_SPEED);
                break;
            case SlaveWorkType.Bug:
                m_progressScript.m_colour = m_bugColour;
                HandleSlaveWork(BUG_ROTATION_SPEED);
                break;
            case SlaveWorkType.Error:
                m_progressScript.m_colour = m_errorColour;
                HandleSlaveWork(NO_ROTATION_SPEED);
                break;
            case SlaveWorkType.Boosted:
                m_progressScript.m_colour = m_boostColour;
                HandleSlaveWork(BOOST_WORK_ROTATION_SPEED);
                break;
            default:
                HandleSlaveWork(NO_ROTATION_SPEED);
                break;
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, m_rotation, 0.0f));

        if (m_progressScript != null && m_progressScript.m_current >= m_progressScript.m_maximum)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HandlePlayerTrigger();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (m_workType != SlaveWorkType.Work && m_workType != SlaveWorkType.Finished)
            {

            }
        }
    }

    private void RollIfGameBreaks()
    {
        int randomNum = Random.Range(0, 10000);
        switch (m_workType)
        {
            case SlaveWorkType.Work:
                {
                    if (randomNum < 25)
                    {
                        m_workType = SlaveWorkType.Bug;
                    }
                    else if (randomNum < 5)
                    {
                        m_workType = SlaveWorkType.Error;
                    }
                    break;
                }
            case SlaveWorkType.Bug:
                {
                    if (randomNum < 15)
                    {
                        m_workType = SlaveWorkType.Error;
                    }
                    break;
                }
            default:
                break;
        }
    }

    private void HandlePlayerTrigger()
    {
        switch (m_workType)
        {
            case SlaveWorkType.Start:
                //get position from world space to canvas space
                Vector2 relativeCanvasPos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position) - m_gameCanvas.pixelRect.size / 2.0f;
                m_progressBar = Instantiate(m_radialBar);
                //attach it to the canvas
                m_progressBar.transform.SetParent(m_gameCanvas.transform, false);
                //move it to the relative position
                m_progressBar.rectTransform.anchoredPosition = relativeCanvasPos;
                m_progressScript = m_progressBar.GetComponent<ProgressBar>();
                m_progressScript.m_maximum = WORK_TIME;
                m_progressScript.m_colour = m_workColour;
                //make slave do work
                m_workType = SlaveWorkType.Work;
                break;
            case SlaveWorkType.Work:
                if (!m_needsCooldown && !m_boostedWork)
                {
                    m_workType = SlaveWorkType.Boosted;
                    m_progressScript.m_colour = m_boostColour;
                    m_boostedWork = true;
                }
                break;
            case SlaveWorkType.Bug:
                m_workType = SlaveWorkType.Work;
                m_needsCooldown = true;
                break;
            case SlaveWorkType.Error:
                m_needsCooldown = true;
                m_workType = SlaveWorkType.Work;
                break;
            default:
                //HandleSlaveWork(NO_ROTATION_SPEED);
                break;
        }
    }

    private void HandleSlaveWork(float t_workSpeed)
    {
        m_rotation += Time.deltaTime * t_workSpeed * m_rotateDirection;

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

        if (m_progressBar != null)
        {
            var progressScript = m_progressBar.GetComponent<ProgressBar>();
            progressScript.m_current += (int)(Time.deltaTime * t_workSpeed);
        }
    }
}
