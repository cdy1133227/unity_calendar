using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Calendar : MonoBehaviour
{
    public static int rowCount = 6;
    public static int columnCount = 7;

    public GameObject dayPrefab;
    public GameObject dayParent;
    public Text uiYearMonth;
    public Text uiTitle;

    private Dictionary<int, DayItem> m_allDayItems = new Dictionary<int, DayItem>();
    private DateTime m_currentDateTime;
    private List<int> m_selectionsList;

    // Use this for initialization
    void Start()
    {
        InitDays();

        System.DateTime now = System.DateTime.Now;
        m_currentDateTime = now;

        SetMonth(now.Year, now.Month);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitDays()
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                GameObject go = Instantiate(dayPrefab, dayParent.transform);
                DayItem dayItem = go.GetComponent<DayItem>();

                DayItemData data = new DayItemData();
                data.index = CalendarIndex(i, j);
                data.date = DateTime.Today;
                dayItem.Data = data;

                m_allDayItems.Add(data.index, dayItem);
            }
        }
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="title"></param>
    public void SetTitle(string title)
    {
        if (uiTitle != null)
            uiTitle.text = title;
    }

    /// <summary>
    /// 设置日历当前显示的年月
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    public void SetMonth(int year, int month)
    {
        // 日历当前的日期
        m_currentDateTime = new DateTime(year, month, m_currentDateTime.Day);

        RandomSelectionExample();

        if (uiYearMonth != null)
            uiYearMonth.text = string.Format("{0}/{1:00}", year, month);

        // 当月第1天
        DateTime monthDay1 = new DateTime(year, month, 1);

        // 当月第1天的星期
        DayOfWeek monthDay1Week = monthDay1.DayOfWeek;

        // 日历index为0的那一天
        DateTime day0 = monthDay1 - new TimeSpan((int)monthDay1Week, 0, 0, 0);

        // 当月总天数
        int selectionIndex = 0;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                int index = CalendarIndex(i, j);
                DayItem dayItem = m_allDayItems[index];
                dayItem.Date = day0 + new TimeSpan(index, 0, 0, 0);

                dayItem.IsCurrentMonth = (dayItem.Date.Year == year && dayItem.Date.Month == month);

                if (dayItem.IsCurrentMonth && m_selectionsList != null && m_selectionsList.Contains(dayItem.Date.Day))
                {
                    dayItem.SelectionIndex = selectionIndex;
                    selectionIndex++;
                }
            }
        }
    }

    /// <summary>
    /// 设置选中的日期列表
    /// </summary>
    /// <param name="selectionDays"></param>
    public void SetSelections(List<int> selectionDays)
    {
        m_selectionsList = selectionDays;
    }

    /// <summary>
    /// 上一个月按钮点击事件
    /// </summary>
    public void OnPrevMonthClicked()
    {
        AddMonth(-1);
    }

    /// <summary>
    /// 下一个月按钮点击事件
    /// </summary>
    public void OnNextMonthClicked()
    {
        AddMonth(1);
    }

    /// <summary>
    /// 按月翻页
    /// </summary>
    /// <param name="offset"></param>
    private void AddMonth(int offset)
    {
        DateTime newDateTime = m_currentDateTime.AddMonths(offset);
        SetMonth(newDateTime.Year, newDateTime.Month);
    }

    private int CalendarIndex(int x, int y)
    {
        return x * columnCount + y;
    }

    /// <summary>
    /// 测试：随机选中几天
    /// </summary>
    private void RandomSelectionExample()
    {
        if (m_selectionsList == null)
            m_selectionsList = new List<int>();

        m_selectionsList.Clear();

        int dayCount = DateTime.DaysInMonth(m_currentDateTime.Year, m_currentDateTime.Month);
        for (int i = 0; i < dayCount; i++)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) < 0.3f)
            {
                m_selectionsList.Add(i + 1);
            }
        }
    }
}
