using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator animator;
    public CharacterController controller;
    public CinemachineFreeLook freeLookCamera;
    public float walkSpeed;
    public float runSpeed;
    public float sneakSpeed;
    public float smoothTime;

    private float smoothVelocity;
    private float horizontal;
    private float vertical;
    private float sneakingKey;
    private float runningKey;

    private Vector3 direction;

    private const float gravity = 10000f;

    private void Start()
    {
        Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        sneakingKey = Input.GetAxisRaw("Fire1"); // left ctrl for sneaking
        runningKey = Input.GetAxisRaw("Fire3"); // left shift for running

        float cameraAngle = freeLookCamera.State.CorrectedOrientation.eulerAngles.y;

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float currentSpeed;

            if (runningKey != 0f)
            {
                currentSpeed = runSpeed;
                animator.SetInteger("WalkIndecator", 3);
            }
            else if (sneakingKey != 0f)
            {
                currentSpeed = sneakSpeed;
                animator.SetInteger("WalkIndecator", 1);
            }
            else
            {
                currentSpeed = walkSpeed;
                animator.SetInteger("WalkIndecator", 2);
            }

            Quaternion lookRotation = Quaternion.Euler(0f, cameraAngle, 0f);
            direction = lookRotation * direction;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, smoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(currentSpeed * Time.deltaTime * moveDirection.normalized);
        }
        else
        {
            animator.SetInteger("WalkIndecator", 0);
        }

        if (!controller.isGrounded)
        {
            direction.y -= gravity * Time.fixedDeltaTime;
            controller.Move(direction * Time.fixedDeltaTime);
        }
    }
}
