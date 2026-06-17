using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public Vector3 velocity;
    public bool isGrounded;
    public bool isSprinting;

    public Vector3 GrapplePoint;
    
    public float GrappleRadius;
    public float GrappleDistance;
    
    public Vector3 grapplePoint;
    public Vector3 grappleLaunchVelocity;
    public float grappleStopDistance = 1.5f;
    
    public bool isgrappleing = false;
    
    public LayerMask GrappleMask;
    public float Counter;
    
    [Tooltip("Weight of a person")]
    public float mass = 65;
    [Tooltip("Air resistance acting on the object(1 is defualt)")]
    public float dragCoeff = 1;    
    [Tooltip("The area of the feet which is moving downward")]
    public float area = 0.5f;         
}
