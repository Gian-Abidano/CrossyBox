using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //static akan membuat variabel ini shared pada semua tree
    public static List<Vector3> AllTreePos = new List<Vector3>();

    private void OnEnable()
    {
        AllTreePos.Add(this.transform.position);
    }
    
    private void OnDisable()
    {
        AllTreePos.Add(this.transform.position);
    }
}
