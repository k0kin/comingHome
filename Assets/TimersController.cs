using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimersController : MonoBehaviour
{
    public event Action<Timer> TimerAdded;
    public event Action<string> TimerCompleted;
    public TimersConnector connector;
    private Dictionary<string, Timer> activeTimers = new Dictionary<string, Timer>();
    public Dictionary<string, Timer> ActiveTimers => activeTimers;
    private void Awake()
    {
        connector.Controller = this;
    }
    Timer newTimer;
    List<Timer> completedTimers = new List<Timer>();

    private void Update()
    {
        completedTimers.Clear();
        foreach(KeyValuePair<string, Timer> timer in activeTimers)
        {
            timer.Value.Update();
            if (timer.Value.IsTimerCompleted)
                completedTimers.Add(timer.Value);
        }
        for (int i = 0; i < completedTimers.Count; i++)
        {
            activeTimers.Remove(completedTimers[i].Id);
            TimerCompleted?.Invoke(completedTimers[i].Id);
        }
    }

    public Timer AddTimer(string timerId, DateTime startTime, float durationInSeconds)
    {
        Debug.LogError("Timer added");
        Timer newTimer = new Timer(timerId, startTime, durationInSeconds);
        activeTimers.Add(timerId, newTimer);
        TimerAdded?.Invoke(newTimer);
        return newTimer;
    }

    public Timer AddTimer(string timerId, float durationInSeconds)
    {
        return AddTimer(timerId, DateTime.UtcNow, durationInSeconds);
    }

    public void CompleteTimer(string id)
    {
        Timer timer = null;
        if (activeTimers.TryGetValue(id, out timer))
        {
            timer.Finish();
            TimerCompleted?.Invoke(id);
            activeTimers.Remove(id);
        }
    }
}

public class Timer
{
    public event Action Completed;
    public event Action<double,double> Updated;
    string id;
    public string Id => id;
    DateTime startingTime;
    double durationInSeconds;

    double elapsedSeconds;
    public double ElapsedSeconds => elapsedSeconds;
    public double RemainingSeconds => durationInSeconds - elapsedSeconds;

    bool isTimerCompleted;
    public bool IsTimerCompleted => isTimerCompleted;
    public Timer(string id, float duration)
    {
        this.id = id;
        startingTime = DateTime.UtcNow;
        durationInSeconds = duration;
    }
    public Timer(string id,DateTime startTime, double duration)
    {
        this.id = id;
        this.startingTime = startTime;
        durationInSeconds = duration;
    }
    public void Update()
    {
        if (!isTimerCompleted)
        {
            double elapsedSeconds = DateTime.UtcNow.Subtract(startingTime).TotalSeconds;
            this.elapsedSeconds = elapsedSeconds;
            if (elapsedSeconds >= durationInSeconds)
            {
                Finish();
            }
            else
            {
                Updated?.Invoke(durationInSeconds, elapsedSeconds);
            }
        }
    }
    public void ReduceDuration(float secondsToReduce)
    {
        TimeSpan timeToReduce = TimeSpan.FromSeconds(secondsToReduce);
        startingTime.Subtract(timeToReduce);

        double elapsedSeconds = DateTime.UtcNow.Subtract(startingTime).TotalSeconds;
        Updated?.Invoke(durationInSeconds, elapsedSeconds);
    }

    public void Finish()
    {
        isTimerCompleted = true;
        Completed?.Invoke();
    }

}