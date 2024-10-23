using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { NotStarted, OnGoing, Completed };

public class Task
{
    private string taskName;
    private string taskDescription;
    private Status status;

    public Task(string taskName, string taskDescription)
    {
        this.taskName = taskName;
        this.taskDescription = taskDescription;
        this.status = Status.NotStarted;
    }

    public string GetName()
    {
        return this.taskName;
    }

    public void completeTask()
    {
        this.status = Status.Completed;
    }

    public Status GetStatus()
    {
        return this.status;
    }

    public void StartTask()
    {
        if (this.status == Status.NotStarted) this.status = Status.OnGoing;
    }

    public string getName()
    {
        return this.taskName;
    }
}

