using UnityEngine;

public class MovementBoundaryTest : MonoBehaviour
{
    public float highSpeed = 10f;
    public float lowSpeed = 1f;
    public float normalSpeed = 5f;

    private Rigidbody rb;
    private bool isClipping = false;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * currentSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        TestPlayerCollision();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetMovementSpeed(highSpeed);
            Debug.Log("High Speed Test Activated");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetMovementSpeed(lowSpeed);
            Debug.Log("Low Speed Test Activated");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetMovementSpeed(normalSpeed);
            Debug.Log("Normal Speed Activated");
        }
    }

    void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }

    void TestPlayerCollision()
    {
        RaycastHit hit;
        isClipping = false;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            isClipping = true;
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 1f))
        {
            isClipping = true;
        }
        if (Physics.Raycast(transform.position, transform.right, out hit, 1f))
        {
            isClipping = true;
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 1f))
        {
            isClipping = true;
        }

        if (isClipping)
        {
            Debug.LogWarning("Clipping detected! The player is clipping through an object.");
        }
        else
        {
            Debug.Log("No clipping detected.");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 1f);
        Gizmos.DrawRay(transform.position, -transform.forward * 1f);
        Gizmos.DrawRay(transform.position, transform.right * 1f);
        Gizmos.DrawRay(transform.position, -transform.right * 1f);
    }
}
