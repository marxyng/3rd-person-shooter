using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    //public static variable is one that can be read, accessed, and changed by other scripts
    //1- singleshot, 2- rapidfire, 3-grenadelauncher, 4 -flamethrower
    
    public static int WeaponID = 1;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
