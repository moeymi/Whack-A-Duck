using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject character;

    [SerializeField]
    private float moveSpeed = 5f; // The character's movement speed

    [SerializeField]
    private float rotationSpeed = 20f; // The character's movement speed

    private CharacterController chController; // Reference to the Rigidbody component
    private Vector3 movement;
    private Animator animator;

    private void Start()
    {
        chController = character.GetComponent<CharacterController>();
        animator = character.GetComponent<Animator>();
    }

    private void Update()
    {
        // Get horizontal and vertical inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement vector based on input
        movement = Vector3.ClampMagnitude(new Vector3(horizontalInput, 0f, verticalInput), 1);

        // Rotate the character to face the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            chController.transform.rotation = Quaternion.RotateTowards(chController.transform.rotation,
                toRotation, Time.deltaTime * rotationSpeed);
        }

        chController.Move(movement * Time.deltaTime * moveSpeed);

        animator.SetBool("isRunning", horizontalInput != 0 || verticalInput != 0);
    }
}
