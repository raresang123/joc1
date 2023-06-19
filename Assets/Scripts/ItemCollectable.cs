using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
  
    public virtual void Destroy()
        
    {
       
        Destroy(gameObject);
        
    }
    

}
