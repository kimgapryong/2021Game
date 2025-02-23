using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreportiesIcon : IconsController
{
   
    public override bool Init()
    {
        base.Init();

        transform.GetComponent<SpriteRenderer>().sprite = Manager.Resources.Load<Sprite>("b_t_01.sprite");
         
        return true;
    }
    public override void OnSetEquipment<T>(CretureController owner)
    {
        //¿©±â¿¡ ·£´ý
        GameObject go = Manager.Resources.Instantiate("");
    }

}
