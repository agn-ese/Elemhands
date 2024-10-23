using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    List<Task> tasks;
    Task currentTask;


    private void Start()
    {
        tasks = new List<Task>();
        Task tutorial = new Task("Tutorial", "Impara le meccaniche");
        Task taskAria = new Task("TaskAria", "Usare gesto aria per arrivare alla chiave");
        Task taskTerra = new Task("TaskTerra", "Liberare spirito terra");
        Task finalTask = new Task("FinalTask", "Attivare i geyser con entrambi i gesti");
        tasks.Add(tutorial);
        tasks.Add(taskAria);
        tasks.Add(taskTerra);
        tasks.Add(finalTask);
        currentTask = tasks[0];
    }

    private void Update()
    {

    }

    void getAndStartNextTask()
    {
        int index = tasks.FindIndex(t => t.getName() == currentTask.getName());
        if (index + 1 < tasks.Count)
            currentTask = tasks[index + 1];
        currentTask.StartTask();

    }
}
