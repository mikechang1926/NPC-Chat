using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class RandomWander : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 5f;
    public Vector2 directionChangeIntervalRange = new Vector2(1f, 4f);

    private Vector3 direction;
    private Rigidbody rb;
    private Animator animator;

    private bool isIdle = false; // track if NPC is idle

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        if (!isIdle)
        {
            Vector3 move = direction * speed;
            rb.MovePosition(rb.position + move * Time.fixedDeltaTime);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }
    }

    void ChooseNewDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        float nextInterval = Random.Range(directionChangeIntervalRange.x, directionChangeIntervalRange.y);
        Invoke(nameof(ChooseNewDirection), nextInterval);
    }

    public void SetIdle(bool idleState)
    {
        isIdle = idleState;
        animator.SetBool("idle", idleState);

        if (idleState)
        {
            direction = Vector3.zero; // Stop moving immediately
        }
        else
        {
            ChooseNewDirection(); // resume wandering
        }
    }
}
