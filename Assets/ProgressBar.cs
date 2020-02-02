using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public int m_maximum;
    public int m_current;
    public Image m_mask;
    public Image m_fill;
    public Color m_colour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)m_current / (float)m_maximum;
        m_mask.fillAmount = fillAmount;

        m_fill.color = m_colour;
    }
}
