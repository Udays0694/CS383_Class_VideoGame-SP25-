using UnityEngine;


//notes : uses virtual in the superclass and override in the sublcass which works to allow dynamic binding. 

//Without the use of virtual and override in their respective classes, static type ItemBase is used to decide which method is called, leading to
//PotionItem's method never being called. 
public class ItemBase //Superclass 
{
    public void Use()
    {
        Debug.Log("Used Item");
    }
}

public class PotionItem : ItemBase //Subclass
{
    public void Use()
    {
        Debug.Log("Healed Player");
    }
}

public class DynamicBindingDemo : MonoBehaviour
{
    private void Start()
    {
        ItemBase item = new PotionItem(); //Static type is ItemBase, dynamic type is PotionItem
        item.Use();

        //
    }
}
