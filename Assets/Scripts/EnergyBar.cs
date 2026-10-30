using UnityEngine;
using UnityEngine.UI; 

public class EnergyBar : MonoBehaviour
{
    Image bar;
    [SerializeField] PlayerController player;

    void Start(){
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update(){
        bar.fillAmount = player.energy / player.maxEnergy;
        
        if (player.energy == 0){
            bar.color = new Color32(159, 159, 159, 125);
        }
        else if (player.energy == player.maxEnergy){
            bar.color = new Color32(255,255, 255, 255);
        }
    }


}
