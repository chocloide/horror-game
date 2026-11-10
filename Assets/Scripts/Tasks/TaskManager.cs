using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TaskManager : MonoBehaviour{
 public static TaskManager instance {  get; private set; }

    
    [SerializeField] GameObject TaskList;

    [SerializeField] List<Task> Inspectorlist = new List<Task>();
    List<Task> list = new List<Task>();

    private void Awake(){
        instance = this;
    }

    private void Start() {
        InteractableObject.OnInteract += checkObject;

        // si una tarea creada directamente desde el editor existe, crea el textmesh de la ui
        foreach (Task task in list){


            task.TextMeshObj = CreateTextMesh(task.TaskName);
            task.TextComponent = task.TextMeshObj.GetComponent<TextMeshProUGUI>();
        }

    }

    public void NewTaskPlace(string taskName, CheckArea placeTrigger = null){
        Task task = new Task();
       task.TextMeshObj = CreateTextMesh(taskName);

        task.name = taskName;
        task.type = Task.TaskType.Place;
        task.place = placeTrigger;

        list.Add(task);
    }

    public void NewTaskObject(string taskName, string Object = null, int Needed = 1){
        Task task = new Task();
        
        task.name = taskName;
        task.type = Task.TaskType.Object;
        task.obj = Object;
  
        task.maxAmount = Needed;
        string name = task.TaskName;
        
        //if (Needed > 1){
            //name += " : " + task.amount.ToString() + "/" + task.maxAmount.ToString();
        //}

        task.TextMeshObj = CreateTextMesh(name);
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

    public void checkObject(InteractableObject obj){
        foreach (Task t in list){
            if (t.obj == obj.objName){
                Debug.Log("AAa");
                if (t.amount != t.maxAmount){
                    t.amount++;
                    t.UpdateText();

                    if (t.amount == t.maxAmount) {
                        t.isCompleted = true;

                        t.TextComponent.fontStyle = TMPro.FontStyles.Strikethrough;
                        //Destroy(t.TextMeshObj);
                        //list.Remove(t);
                        break;
                    }
                }
            }
        }
    }


}