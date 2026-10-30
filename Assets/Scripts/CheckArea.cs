using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CheckArea : MonoBehaviour
{
    public UnityEvent<CheckArea> onArea;
    public bool active = true;

    private void OnTriggerEnter(Collider collision){
        if (collision.CompareTag("Player") && active){
            onArea.Invoke(this);
        }
    }

}
