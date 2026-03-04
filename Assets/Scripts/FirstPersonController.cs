using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 4.5f;
    public float gravity = -18f;
    public float jumpHeight = 1.2f;
    public Transform cameraPivot;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 85f;

    CharacterController cc;
    Vector3 velocity;
    float pitch;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mx);
        pitch -= my;
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    void Move()
    {
        Debug.Log($"H={Input.GetAxisRaw("Horizontal")} V={Input.GetAxisRaw("Vertical")}");        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        cc.Move(move * moveSpeed * Time.deltaTime);

        if (cc.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (cc.isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}