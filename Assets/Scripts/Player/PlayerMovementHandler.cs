
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] CinemachineCamera playerWalkCamera;
    PlayerInputHandler inputHandler;
    public  AnimationController animController;
    
    public PlayerData Data;
    
    private GrapplePoints GrapplePoints;
    public float LeapDistance = 2;
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
    
    private float TerminalVelocity()
    {
        float airDensity = 1.225f;
        return Mathf.Sqrt((2f * Data.mass * Mathf.Abs(Data.gravity))/ (airDensity * Data.dragCoeff * Data.area));
    }
    
    Vector3 moveDir => CamForward() * inputHandler.MoveInput.y + CamRight() * inputHandler.MoveInput.x;

    
    
    void Awake()
    {
        animController = GetComponent<AnimationController>();
        inputHandler = GetComponent<PlayerInputHandler>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Data.velocity = Vector3.zero;
        Data.isgrappleing = false;
        
    }

    void Update()
    {
        Data.isGrounded = controller.isGrounded;
        
        if (Data.isGrounded && Data.velocity.y < 0) Data.velocity.y = -2f;

        if (!Data.isgrappleing)
        {
            Data.velocity.y += Data.gravity * Time.deltaTime;
            Data.velocity.y = Mathf.Clamp(Data.velocity.y, -TerminalVelocity(), Mathf.Infinity);
            controller.Move(Vector3.up * Data.velocity.y * Time.deltaTime);
        }

        Grapple();

        if (Data.isgrappleing)
            StartGrapple();
        else
            HandleMove();
        
        
        
    }

    public void HandleMove()
    {
        if (inputHandler.SprintInput && !Data.isSprinting) Data.isSprinting = true;
        else if (!inputHandler.SprintInput && Data.isSprinting) Data.isSprinting = false;

        // Heavier = slower, tune the divisor to feel right
        float massMultiplier = 1f / (1f + Data.mass * 0.01f);
        
        if (inputHandler.MoveInput == Vector2.zero)
        {
            Data.currentSpeed = 0;
        }
        else
        {
            Data.currentSpeed = Data.isSprinting ? Data.sprintSpeed : Data.speed;
            Data.currentSpeed *= massMultiplier;
        }

        controller.Move(moveDir * Data.currentSpeed * Time.deltaTime);
    }

    public void Grapple()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, Data.GrappleRadius, Data.GrappleMask);
        foreach (Collider hit in hits)
        {
            Vector3 hitDirection = (hit.transform.position - transform.position).normalized;
            if (Vector3.Angle(playerWalkCamera.transform.forward, hitDirection) < playerWalkCamera.Lens.FieldOfView * 0.5f)
            {
                if (Vector3.Distance(transform.position, hit.transform.position) < Data.GrappleDistance)
                {
                    Debug.DrawLine(transform.position, hit.transform.position, Color.red);
                    if (!Data.isgrappleing && inputHandler.grapple)
                    {
                        GrapplePoints = hit.GetComponent<GrapplePoints>();
                        Data.grapplePoint = hit.transform.position;

                        // Calculate a one-time launch velocity toward the point
                        Vector3 direction = (Data.grapplePoint - transform.position);
                        Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z).normalized;

                        Data.grappleLaunchVelocity = flatDirection * GrapplePoints.speed 
                                                + Vector3.up * GrapplePoints.ySpeed;
                        Data.Timeleft = GrapplePoints.Time;
                        // Override current velocity so gravity arc starts fresh

                        
                        animController.anim.SetTrigger("Grapple");
                    }
                }
            }
        }
    }
    public void StartGrapple()
    {
        
        // Let gravity naturally pull the arc down
        Data.velocity.y += Data.gravity * Time.deltaTime;
        Data.velocity.y = Mathf.Clamp(Data.velocity.y, -TerminalVelocity(), Mathf.Infinity);

        controller.Move(Data.velocity * Time.deltaTime);

        Data.Counter += Time.deltaTime;

        float distanceToPoint = Vector3.Distance(transform.position, Data.grapplePoint);

        // Stop when close enough OR time runs out
        if (distanceToPoint < Data.grappleStopDistance || Data.Counter > Data.Timeleft)
        {
            Data.isgrappleing = false;
            GrapplePoints = null;
            Data.velocity = Vector3.zero; 
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Data.GrappleRadius);
    }
    
}