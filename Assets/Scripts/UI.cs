using UnityEngine;
using UnityEngine.UI; 

public class EnergyBar : MonoBehaviour
{
    [SerializeField] Image Energybar;
    [SerializeField] Image HealthBar;
    [SerializeField] Image SusBar;
    [SerializeField] PlayerController player;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] MonsterIA Monster;


    void Update(){
        Energybar.fillAmount = player.energy / player.maxEnergy;
        HealthBar.fillAmount = healthSystem.health / healthSystem.maxHealth;

        if (player.energy == 0){
            Energybar.color = new Color32(159, 159, 159, 125);
        }

        else if (player.energy == player.maxEnergy){
            Energybar.color = new Color32(254, 207, 0, 255);
        }
    }
}
