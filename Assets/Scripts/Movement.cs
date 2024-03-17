using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator animator;
    public CharacterController controller;
    public float walkSpeed;
    public float runSpeed;
    public float smoothTime;
    public float gravity = 9.81f;
    float smoothVelocity;
    public Transform firstCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float running = Input.GetAxisRaw("Fire3"); // Fire3 представляет собой кнопку для бега (обычно левый шифт)

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float currentSpeed;
            if (running != 0f)
            {
                currentSpeed = runSpeed;
                animator.SetInteger("WalkIndecator", 2);
            }
            else
            {
                currentSpeed = walkSpeed;
                animator.SetInteger("WalkIndecator", 1);
            }

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + firstCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
            }
            else
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetInteger("WalkIndecator", 0);
        }
    }
}

