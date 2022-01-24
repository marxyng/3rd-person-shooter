using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Animator Anim;
    [SerializeField] float StillRotateSpeed = 0.5f;
    [SerializeField] float WalkRotateSpeed = 1.5f;
    [SerializeField] float RunRotateSpeed = 1.5f;
    [SerializeField] float AimRotateSpeed = 0.6f;
    [SerializeField] GameObject Crosshair;
    private float RotateSpeed;

    private AnimatorStateInfo PlayerInfo;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        Crosshair.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInfo = Anim.GetCurrentAnimatorStateInfo(0);
        float MoveDirection = Input.GetAxis("Vertical");
        float RotateDirection = Input.GetAxis("Mouse X");

        if(PlayerInfo.IsTag("Still"))
        {
            RotateSpeed = StillRotateSpeed;
            Crosshair.gameObject.SetActive(false);
        }

        if(PlayerInfo.IsTag("Walk"))
        {
            RotateSpeed = WalkRotateSpeed;
        }

        if(PlayerInfo.IsTag("Run"))
        {
            RotateSpeed = RunRotateSpeed;
        }

         if(PlayerInfo.IsTag("Aiming"))
        {
            RotateSpeed = AimRotateSpeed;
            Crosshair.gameObject.SetActive(true);
        }


        if(MoveDirection > 0)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Anim.SetBool("Running", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Anim.SetBool("Running", false);
        }
        else
        {
            Anim.SetBool("Walk", true);
        }
        }

        if(RotateDirection > 0)
        {
            this.transform.Rotate(Vector3.up * RotateSpeed);
        }

        if(MoveDirection == 0)
        {
            Anim.SetBool("Walk", false);
             Anim.SetBool("WalkBack", false);
             Anim.SetBool("Running", false);
        }

        if(MoveDirection < 0)
        {
            Anim.SetBool("WalkBack", true);
        }

         if(RotateDirection < 0)
        {
            this.transform.Rotate(Vector3.up * -RotateSpeed);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Anim.SetBool("Aim", true);
        }

        if(Input.GetMouseButtonUp(1))
        {
            Anim.SetBool("Aim", false);
        }

      
    }
}
