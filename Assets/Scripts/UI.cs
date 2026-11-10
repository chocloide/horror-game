using UnityEngine;
using UnityEngine.UI; 

public class EnergyBar : MonoBehaviour
{
    [SerializeField] Image Energybar;
    [SerializeField] Image SusBar;
    [SerializeField] PlayerController player;
    [SerializeField] MonsterIA Monster;


    void Update(){
        Energybar.fillAmount = player.energy / player.maxEnergy;
        //SusBar.fillAmount = Monster.sus / 50;


        if (player.energy == 0){
            Energybar.color = new Color32(159, 159, 159, 125);
        }

        else if (player.energy == player.maxEnergy){
            Energybar.color = new Color32(255,255, 255, 255);
        }
    }
}
