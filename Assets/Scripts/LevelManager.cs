using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    List<Task> tasks;
    int currentTaskIndex;


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
        currentTaskIndex = 0;
    }

    private void Update()
    {
        if (tasks[currentTaskIndex].GetStatus() == Status.Completed)
            Debug.Log("The end"); 

    }

    //funzione per finire task in corso e iniziare la prossima
    public void endLastAndStartNextTask() 
    {
        tasks[currentTaskIndex].completeTask();
        if (currentTaskIndex + 1 < tasks.Count)
            currentTaskIndex += 1;
        tasks[currentTaskIndex].StartTask();

    }

}
