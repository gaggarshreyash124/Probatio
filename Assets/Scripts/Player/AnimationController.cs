using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerData PlayerData;
    public Animator anim;
    public PlayerInputHandler inputHandler;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        anim.SetBool("Run",PlayerData.isSprinting);
        anim.SetBool("Walk",PlayerData.currentSpeed != 0);
        anim.SetBool("Grounded",PlayerData.isGrounded);
        
    }

    public void GrappleStart()
    {
        PlayerData.isgrappleing = true;
        PlayerData.velocity = PlayerData.grappleLaunchVelocity;
        PlayerData.Counter = 0;
    }
    
}
