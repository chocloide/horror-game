using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task{
    public string name;

    public enum TaskType { Object, Place}
    public TaskType type;

    public CheckArea place;
    public int amount = 0;
    public int maxAmount = 0;

    public bool isCompleted = false;

}

public class TaskManager : MonoBehaviour{
    [SerializeField] GameObject TaskList;
    private List<Task> list = new List<Task>();

    public void AddTask(string taskName, Task.TaskType type, CheckArea placeTrigger = null){
        Task task = new Task();
        GameObject obj = new GameObject("Task");

        obj.name = taskName;
        obj.transform.SetParent(TaskList.transform, false);
        obj.AddComponent<TextMeshProUGUI>();
        obj.layer = 8;

        TextMeshProUGUI textMesh = obj.GetComponent<TextMeshProUGUI>();
        RectTransform rect = obj.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector3(-175, 100 - (32 * list.Count), 0);

        task.name = taskName;
        task.type = type;

        if (type == Task.TaskType.Place){
            task.place = placeTrigger;
        }

        textMesh.text = taskName;
        textMesh.color = new Color(0, 255, 0);
        textMesh.enableWordWrapping = false;

        textMesh.fontSize = 32;
        list.Add(task);
    }

    public void checkPlace(CheckArea checkPlace){
        foreach (Task t in list){
            if (t.type == Task.TaskType.Place && t.place == checkPlace){
                GameObject taskText = GameObject.Find(t.name);
                t.isCompleted = true;
                Destroy(taskText);
                list.Remove(t);
                Debug.Log(t.name + " terminada");
                break;
            }
        }
    }
}