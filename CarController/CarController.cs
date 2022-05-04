using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody sphereRB;
    
    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;

    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;
    
    private float normalDrag;
    public float modifiedDrag;
    
    public float alignToGroundTime;

    Quaternion originalRotation;


    void Start()
    {
        // Detach Sphere from car
        sphereRB.transform.parent = null;

        normalDrag = sphereRB.drag;

        originalRotation = transform.rotation;

    }

    void Update()
    {
        // Get Input
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        // Calculate Turning Rotation
        float newRot = turnInput * turnSpeed * Time.deltaTime * moveInput;
        
        if (isCarGrounded)
            transform.Rotate(0, newRot, 0, Space.World);

        // Set Cars Position to Our Sphere
        transform.position = sphereRB.transform.position;

        // Raycast to the ground and get normal to align car with it.
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1f, groundLayer);
        


        if (isCarGrounded)
        {
            // Rotate Car to align with ground
            Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);

        }
        else
        {
            // Return Car to it's original rotation when not grounded
            if (transform.rotation != originalRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, alignToGroundTime * Time.deltaTime);
            }
        }


        // Calculate Movement Direction
        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;
        
        // Calculate Drag
        sphereRB.drag = isCarGrounded ? normalDrag : modifiedDrag;
    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration); // Add Movement
        else
            sphereRB.AddForce(transform.up * -200f); // Add Gravity
    }
}
