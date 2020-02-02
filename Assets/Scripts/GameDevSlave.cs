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
        Error = 3
    }

    [SerializeField]
    private float m_rotation;
    private float m_rotateDirection;

    public float m_maxLeftRotation;
    public float m_maxRightRotation;

    private const float WORK_ROTATION_SPEED = 180.0f;
    private const float BUG_ROTATION_SPEED = WORK_ROTATION_SPEED / 2.0f;
    private const int NO_ROTATION_SPEED = 0;
    private const float ROTATION_DIFFERENCE = 15.0f;

    private SlaveWorkType m_workType;

    private Canvas m_gameCanvas;
    public Image m_linearBar;
    public Image m_radialBar;
    private Image m_progressBar;


    // Start is called before the first frame update
    void Start()
    {
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
        switch (m_workType)
        {
            case SlaveWorkType.Work:
                HandleSlaveWork(WORK_ROTATION_SPEED);
                break;
            case SlaveWorkType.Bug:
                HandleSlaveWork(BUG_ROTATION_SPEED);
                break;
            case SlaveWorkType.Start:
            case SlaveWorkType.Error:
                HandleSlaveWork(NO_ROTATION_SPEED);
                break;
            default:
                HandleSlaveWork(NO_ROTATION_SPEED);
                break;
        }

        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, m_rotation, 0.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HandlePlayerTrigger();
        }
    }

    private void HandlePlayerTrigger()
    {
        switch (m_workType)
        {
            case SlaveWorkType.Start:
                //get position from world space to canvas space
                Vector2 relativeCanvasPos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position) - m_gameCanvas.pixelRect.size / 2.0f;
                if (0 == Random.Range(0, 2))
                {
                    //spawn radial progress bar
                    m_progressBar = Instantiate(m_radialBar);

                }
                else
                {
                    //spawn radial progress bar
                    m_progressBar = Instantiate(m_linearBar);
                }

                //attach it to the canvas
                m_progressBar.transform.SetParent(m_gameCanvas.transform, false);
                //move it to the relative position
                m_progressBar.rectTransform.anchoredPosition = relativeCanvasPos;

                //make slave do work
                m_workType = SlaveWorkType.Work;
                break;
            //case SlaveWorkType.Work:
            //    HandleSlaveWork(WORK_ROTATION_SPEED);
            //    break;
            //case SlaveWorkType.Bug:
            //    HandleSlaveWork(BUG_ROTATION_SPEED);
            //    break;
            //case SlaveWorkType.Error:
            //    HandleSlaveWork(NO_ROTATION_SPEED);
            //    break;
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
