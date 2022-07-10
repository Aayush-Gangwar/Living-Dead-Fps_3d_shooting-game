using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_canvas_camera : MonoBehaviour
{
    // Start is called before the first frame update\
    public Transform cam;
void LateUpdate(){
    transform.LookAt(transform.position+cam.forward);
}
}
