using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] CinemachineCamera playerWalkCamera;
    PlayerInputHandler inputHandler;
    
    public PlayerData Data;
    Vector3 velocity;
    bool onWall = false;
    
    bool isSprinting = false;
    Vector3 CamForward()
    {
        Vector3 forward = playerWalkCamera.transform.forward;
        forward.y = 0f;
        return forward.normalized;
    }
    
    Vector3 CamRight()
    {
        Vector3 right = playerWalkCamera.transform.right;
        right.y = 0f;
        return right.normalized;
    }
    
    Vector3 moveDir => CamForward() * inputHandler.MoveInput.y + CamRight() * inputHandler.MoveInput.x;

    bool isGrounded;
    
    void Start()
    {
        velocity = Vector3.zero;
        
        inputHandler = GetComponent<PlayerInputHandler>();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        if (inputHandler.JumpInput && isGrounded && !onWall)
            velocity.y = Data.jumpForce;
        if (inputHandler.JumpInput && !isGrounded && onWall)
            velocity = new Vector3();

        if (inputHandler.SprintInput && !isSprinting)
            isSprinting = true;
        else if  (!inputHandler.SprintInput && isSprinting)
            isSprinting = false;
        
        velocity.y += Data.gravity * Time.deltaTime;
        
        float CurrentSpeed = !isSprinting ? Data.speed : Data.sprintSpeed;
        Vector3 FinalDir = moveDir * CurrentSpeed + Vector3.up * velocity.y;
        
        controller.Move(FinalDir * Time.deltaTime);
    }
    
    // WAll Run Parkor based movement
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Vector3 WallNormal = hit.normal;
    //
    //    if (Mathf.Abs(WallNormal.x) > 0.5f) // Threshold to confirm side collision
    //    {
    //        if (WallNormal.x > 0f)
    //        {
    //            Debug.Log("Left Wall");
    //            velocity.y -= Data.gravity * Time.deltaTime;
    //            onWall = true;
    //        }
    //        else if (WallNormal.x < 0f)
    //        {
    //            Debug.Log("Right Wall");
    //            velocity.y -= Data.gravity * Time.deltaTime;
    //            onWall = true;
    //        }
    //    }
    //    else
    //    {
    //        onWall = false;
    //    }
    //}
}