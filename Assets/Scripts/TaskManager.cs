using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task{
    public string name;

    public enum TaskType { Object, Place}
    public TaskType type;

    public CheckArea place;
    public TaskObject obj;

    public GameObject TextMeshObj;
    public TextMeshProUGUI TextComponent;

    public int amount = 0;
    public int maxAmount = 0;

    public bool isCompleted = false;

    public void UpdateText(){
        TextComponent.text = name + " : " + amount.ToString() + "/" + maxAmount.ToString();
    }

}

public class TaskManager : MonoBehaviour{
    [SerializeField] GameObject TaskList;
    private List<Task> list = new List<Task>();

    public void NewTaskPlace(string taskName, CheckArea placeTrigger = null){
        Task task = new Task();
       task.TextMeshObj = CreateTextMesh(taskName);

        task.name = taskName;
        task.type = Task.TaskType.Place;
        task.place = placeTrigger;

        list.Add(task);
    }

    public void NewTaskObject(string taskName, TaskObject Object = null, int Needed = 1){
        Task task = new Task();
        
        task.name = taskName;
        task.type = Task.TaskType.Object;
        task.obj = Object;
        
        task.maxAmount = Needed;
        string a = task.name;
        
        if (Needed > 1){
            a += " : " + task.amount.ToString() + "/" + task.maxAmount.ToString();
        }

        task.TextMeshObj = CreateTextMesh(a);
        task.TextComponent = task.TextMeshObj.GetComponent<TextMeshProUGUI>();

        list.Add(task);
    }

    GameObject CreateTextMesh(string text){
        GameObject obj = new GameObject("Task");

        obj.name = text;
        obj.transform.SetParent(TaskList.transform, false);
        obj.AddComponent<TextMeshProUGUI>();
        obj.layer = 8;

        TextMeshProUGUI textMesh = obj.GetComponent<TextMeshProUGUI>();

        textMesh.text = text;
        textMesh.color = new Color(0, 255, 0);
        textMesh.enableWordWrapping = false;
        textMesh.fontSize = 32;

        return obj;
    }

    public void checkPlace(CheckArea check){
        foreach (Task t in list){
            if (t.type == Task.TaskType.Place && t.place == check){
                t.isCompleted = true;
                Destroy(t.TextMeshObj);
                list.Remove(t);
                break;
            }
        }
    }

    public void updateObject(TaskObject taskObj){
        foreach (Task t in list){
            if (t.type == Task.TaskType.Object && t.obj == taskObj){
                if (t.amount != t.maxAmount){
                    t.amount++;
                    t.UpdateText();

                    if (t.amount == t.maxAmount) {
                        t.isCompleted = true;
                        Destroy(t.TextMeshObj);
                        list.Remove(t);
                        break;
                    }
                }
            }
        }
    }


}