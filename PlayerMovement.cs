using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float SprintSpeed = 10f;
    [SerializeField] private float Sensitivity;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private float JumpForce;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundMask;
    private bool IsSprinting = false;
    private Vector2 PlayerMouseInput;
    private Vector3 PlayerMoveInput;
    private float xRot;


    [SerializeField] private Rigidbody rb;

    private void Start()
    {
      
        rb = GetComponent<Rigidbody>();
       
        
    }


    private void Update()
    {
        PlayerMoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));


        MoveCam();
        MovePlayer();

    }
    private void MovePlayer()
    {

        Speed = Speed;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed += SprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed -= SprintSpeed;
        }
       
        
            



        Vector3 MoveVector = transform.TransformDirection(PlayerMoveInput) * Speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);


       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere(GroundCheck.position, 0.1f, GroundMask))
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }




        }
    }



    private void MoveCam()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        xRot -= PlayerMouseInput.y * Sensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

    }


}
