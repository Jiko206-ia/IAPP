using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class CalendarEvent
{
    public string title;
    public DateTime dateTime;
}

public class CalendarService : MonoBehaviour
{
    private List<CalendarEvent> _events = new List<CalendarEvent>();

    public void AddEvent(string title, DateTime dt)
    {
        _events.Add(new CalendarEvent { title = title, dateTime = dt });
        Debug.Log($"Event added: {title} at {dt}");
    }

    public List<CalendarEvent> GetEventsForDate(DateTime date)
    {
        return _events.FindAll(e => e.dateTime.Date == date.Date);
    }
}
