using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_grenade : MonoBehaviour
{
    public float throw_force=20f;
    private GameObject grenade_prefab;
    public Transform grenade_position;
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        player= GameObject.FindGameObjectWithTag("Player");
        can_throw_grenade ctg=player.transform.GetComponent<can_throw_grenade>();
        if(Input.GetKeyDown(KeyCode.G) && ctg.can_throw==true){
            throw_gre();
            ctg.destro_one();
        }
        
    }
    public void throw_gre(){
        grenade_prefab=Resources.Load<GameObject>("grenade");
        GameObject grenade = Instantiate(grenade_prefab,grenade_position.position,transform.rotation);
        Rigidbody rbd=grenade.GetComponent<Rigidbody>();
        rbd.AddForce(transform.forward*throw_force,ForceMode.VelocityChange);
    }
}
