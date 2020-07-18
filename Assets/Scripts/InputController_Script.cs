using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController_Script : MonoBehaviour
{
  
   
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0)
        {
            Touch l_touch = Input.GetTouch(0);
            Vector3 l_touchPosition =  Camera.main.ScreenToWorldPoint(l_touch.position);        

        }
    }
}
