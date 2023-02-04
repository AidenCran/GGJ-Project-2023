using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class camerabillboard : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
    }
}
