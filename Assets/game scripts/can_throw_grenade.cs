using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class can_throw_grenade : MonoBehaviour
{
    // Start is called before the first frame update
	private GameObject[] bullet_kit;
		private bool reloading_range=false;
    public GameObject[] grenade_present;
    private GameObject player;
    public GameObject grenade_checking;
    public bool can_throw=false;

    // Update is called once per frame
    void Update()
    {
        grenade_present= GameObject.FindGameObjectsWithTag("grenade");
        player= GameObject.FindGameObjectWithTag("Player");
        if(grenade_present.Length>0){
            can_throw=true;
        }
        else{
            can_throw=false;
        }


        bullet_kit= GameObject.FindGameObjectsWithTag("bullet_kit");
			foreach(var kit in bullet_kit){
				float dis = Vector3.Distance(player.transform.position,kit.transform.position);
				if(dis<2.5f && Input.GetKeyDown(KeyCode.R)){
                    grenade_present= GameObject.FindGameObjectsWithTag("grenade");
                    foreach(var gre in grenade_present){
                        Destroy(gre);
                    }
                Instantiate(Resources.Load("grenade_checking") as GameObject);
                 Instantiate(Resources.Load("grenade_checking") as GameObject);
                  Instantiate(Resources.Load("grenade_checking") as GameObject);
                   Instantiate(Resources.Load("grenade_checking") as GameObject);
                    Instantiate(Resources.Load("grenade_checking") as GameObject);
                     Instantiate(Resources.Load("grenade_checking") as GameObject);
                 Instantiate(Resources.Load("grenade_checking") as GameObject);
                  Instantiate(Resources.Load("grenade_checking") as GameObject);
				}
			}
        
    }
    public void destro_one(){
        grenade_present= GameObject.FindGameObjectsWithTag("grenade");
        if(grenade_present.Length>0){
            foreach(var gre in grenade_present){
                Destroy(gre);
                break;
            }
        }
    }
}
