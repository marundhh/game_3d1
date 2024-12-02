using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    private string horizotalInput = "Horizontal";
    private string verticalInput = "Vertical";

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //anim.SetTrigger("Slash");
            if (Input.GetAxis(horizotalInput) != 0
                || Input.GetAxis(verticalInput) != 0)
            {
                anim.SetTrigger("SlashMask");
            }
            else { anim.SetTrigger("Slash"); }
        }
    }
}
