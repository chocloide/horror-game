using UnityEngine;

//CreateAssetMenu permite agregar una opción directa en el menú del proyecto de unity
[CreateAssetMenu(fileName = "Item", menuName = "Inventario/Item", order =1)]
public class Item : ScriptableObject{
    public string id;
    public string description;
    public Sprite icon;
    public GameObject prefab;
}
