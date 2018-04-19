using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DayItemData
{
    public int index;
    public DateTime date;
    public bool isCurrentMonth;
    public int selectionIndex;
}


public class DayItem : MonoBehaviour
{
    public Text labelIndex;
    public Text labelDay;
    public Image imgBackground;
    public Text labelSlectionIndex;

    private DayItemData m_data;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public DayItemData Data
    {
        get
        {
            return m_data;
        }
        set
        {
            m_data = value;
        }
    }

    public int Index
    {
        set
        {
            m_data.index = value;

            if (labelIndex != null)
                labelIndex.text = m_data.index.ToString();
        }
    }

    public DateTime Date
    {
        set
        {
            m_data.date = value;

            if (labelDay != null)
                labelDay.text = m_data.date.Day.ToString();
        }
        get
        {
            return m_data.date;
        }
    }

    public bool IsCurrentMonth
    {
        set
        {
            m_data.isCurrentMonth = value;

            imgBackground.color = m_data.isCurrentMonth ? new Color32(255, 255, 255, 50) : new Color32(255, 255, 255, 10);

            if (labelSlectionIndex != null)
                labelSlectionIndex.text = "";
        }
        get
        {
            return m_data.isCurrentMonth;
        }
    }

    public int SelectionIndex
    {
        set
        {
            m_data.selectionIndex = value;
            
            imgBackground.color = new Color32(144, 58, 66, 255);

            if (labelSlectionIndex != null)
                labelSlectionIndex.text = (m_data.selectionIndex + 1).ToString();
        }
    }
}
