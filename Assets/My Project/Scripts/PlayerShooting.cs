using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] Transform MuzzleSpawn;
    [SerializeField] GameObject MuzzleFlash;
    [SerializeField] GameObject ImpactStone;
    [SerializeField] GameObject ImpactMetal;
    [SerializeField] AudioClip SingleShotSound;
    [SerializeField] AudioClip RapidShotSound;
    //might not do this
    [SerializeField] float RapidDelay = 0.1f;
    [SerializeField] GameObject GrenadeSmoke;
    [SerializeField] AudioClip GrenadeSound;

    [SerializeField] GameObject GrenadeExplosion;
    [SerializeField] GameObject Flames;
   // [SerializeField] Transform FlameSpawn;

    [SerializeField] AudioClip FlameSound;
    [SerializeField] AudioClip PickupFX;
    private bool RapidPlay = true;

    //might not do this
    private bool RapidShooting = true;

    private AudioSource PlayerAudio;

    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
         Flames.gameObject.SetActive(false);
        PlayerAudio = GetComponent<AudioSource>();
      // Flames.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(SaveScript.WeaponID == 1)
        {
        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            //Instantiate means make a copy
            //always put the object being instanitated and its position and rotation
            Instantiate(MuzzleFlash, MuzzleSpawn.position, MuzzleSpawn.rotation);
        
            PlayerAudio.clip = SingleShotSound;
            PlayerAudio.loop = false;
            PlayerAudio.pitch = 1;
            PlayerAudio.Play();
        Hits();
        }
    }
     if(SaveScript.WeaponID == 2)
        {
        if(Input.GetMouseButton(1) && Input.GetMouseButton(0))
        {
            //Instantiate means make a copy
            //always put the object being instanitated and its position and rotation
            Instantiate(MuzzleFlash, MuzzleSpawn.position, MuzzleSpawn.rotation);
        
            if(RapidPlay == true)
            {
           // RapidPlay = false;
            PlayerAudio.clip = RapidShotSound;
            PlayerAudio.loop = true;
            PlayerAudio.pitch = 3;
            PlayerAudio.Play();

            }
        if(RapidShooting == true)
    {
      RapidShooting = false;
      //StartCoroutine is a way to slow things down. have to call function outside of update
      StartCoroutine(RapidFire());
        }
    }
        if(Input.GetMouseButtonUp(0))
        {
            PlayerAudio.Stop();
            RapidPlay = true;
        }
    }
    
        if(SaveScript.WeaponID == 3)
        {
        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            //Instantiate means make a copy
            //always put the object being instanitated and its position and rotation
            Instantiate(GrenadeSmoke, MuzzleSpawn.position, MuzzleSpawn.rotation);
        
            PlayerAudio.clip = GrenadeSound;
            PlayerAudio.loop = false;
            PlayerAudio.pitch = 1;
            PlayerAudio.PlayDelayed(0.3f);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //ray is the object, out hit is registering the hit, 1000 is how far
        if(Physics.Raycast(ray, out hit, 1000))
        {
            StartCoroutine(Grenade());
        }
    
        }
    }

    if(SaveScript.WeaponID == 4)
    {
        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
              //Instantiate(Flames, MuzzleSpawn.position, MuzzleSpawn.rotation);
           Flames.gameObject.SetActive(true);
           if(RapidPlay == true) 
                {
                    
                    RapidPlay = false;
                    PlayerAudio.clip = FlameSound;
                    PlayerAudio.loop = true;
                    PlayerAudio.pitch = 0.1f;
                    PlayerAudio.Play();

                }
            }
        

        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            Flames.gameObject.SetActive(false);
            if(RapidPlay == false) 
                {
                    PlayerAudio.Stop();
                    RapidPlay = true;
                }
            }
        }
    }


    

    void Hits()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //ray is the object, out hit is registering the hit, 1000 is how far
        if(Physics.Raycast(ray, out hit, 1000))
        {
            if(hit.transform.tag == "Stone")
            {
                Instantiate(ImpactStone, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if(hit.transform.tag == "Metal")
            {
                Instantiate(ImpactMetal, hit.point, Quaternion.LookRotation(hit.normal));
            }
             if(hit.transform.tag == "ExplodingBarrel")
            {
                //it saying find script with function and make the barrel explode
               hit.transform.gameObject.SendMessage("Explode");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("RapidFire"))
        {
            SaveScript.WeaponID = 2;
            PickupSound();
            Destroy(other.gameObject, 0.2f);
        }

           if(other.gameObject.CompareTag("GrenadeAmmo"))
        {
            SaveScript.WeaponID = 3;
            PickupSound();
            Destroy(other.gameObject, 0.2f);
        }

           if(other.gameObject.CompareTag("Flamethrower"))
        {
            SaveScript.WeaponID = 4;
            PickupSound();
            Destroy(other.gameObject, 0.2f);
        }
    void PickupSound()
    {
        PlayerAudio.clip = PickupFX;
        PlayerAudio.loop = false;
        PlayerAudio.pitch = 1;
        PlayerAudio.Play();
    }

    }
    //in order to call a coroutine we need a IEnumerator.  Pause type is WaitForSeconds
   //May not use this
    IEnumerator RapidFire()
    {
        yield return new WaitForSeconds(RapidDelay);

        Hits();
        RapidShooting = true;
    }

    IEnumerator Grenade()
    {
        yield return new WaitForSeconds(0.3f);

        Instantiate(GrenadeExplosion, hit.point, Quaternion.LookRotation(hit.normal));
    
     if(hit.transform.tag == "ExplodingBarrel")
            {
                //it saying find script with function and make the barrel explode
               hit.transform.gameObject.SendMessage("Explode");
            }
}
}



