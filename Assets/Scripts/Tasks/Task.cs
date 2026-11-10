using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Inventario/Task", order = 1)]
public class Task : ScriptableObject{
    public enum TaskType { Object, Place, Button }
    public TaskType type;

    public string TaskName;
    public CheckArea place;
    public string obj;

    public GameObject TextMeshObj;
    public TextMeshProUGUI TextComponent;

    public int amount = 0;
    public int maxAmount = 0;

    public bool isCompleted = false;

    public void UpdateText()
    {
        TextComponent.text = TaskName + " : " + amount.ToString() + "/" + maxAmount.ToString();
    }

}