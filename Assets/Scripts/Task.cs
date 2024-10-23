using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { NotStarted, OnGoing, Completed };

public class Task: MonoBehaviour
{
    private string taskName;
    private string taskDescription;
    private Status status;
    private List<Task> tasks;
    private Task currentTask; 

    public Task(string taskName, string taskDescription)
    {
        this.taskName = taskName;
        this.taskDescription = taskDescription;
        this.status = Status.NotStarted;
    }

    public void completeTask()
    {
        this.status = Status.Completed;
        int index = tasks.FindIndex(t => t.taskName == currentTask.getName());
        if(index+1 < tasks.Count) 
            currentTask = tasks[index + 1];
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
        return this.name;
    }

    private void Start()
    {
        currentTask = tasks[0];
        tasks = new List<Task>();
        Task tutorial = new("Tutorial", "Impara le meccaniche");
        Task taskAria = new("TaskAria", "Usare gesto aria per arrivare alla chiave");
        Task taskTerra = new("TaskTerra", "Liberare spirito terra");
        Task finalTask = new("FinalTask", "Attivare i geyser con entrambi i gesti");
        tasks.Add(tutorial);
        tasks.Add(taskAria);
        tasks.Add(taskTerra);
        tasks.Add(finalTask);
    }

    private void Update()
    {
        
    }
}
