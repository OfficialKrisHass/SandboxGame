using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [Header("Movement")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float sprintMultiplier = 1.2f;

    [Header("Looking around")]
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private float verticalSensitivityMultiplier = 0.5f;
    [SerializeField] private float minX = -70.0f;
    [SerializeField] private float maxX = 10.0f;

    [Header("Body Parts")]
    [SerializeField] private Transform camHandle;

    private Rigidbody rb;

    private Vector2 mouseRotation;
    private Vector3 direction;
    private float multiplier = 1.0f;

    void Awake() {
        
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update() {
        
        mouseRotation.y += Input.GetAxis("Mouse X") * sensitivity;
        mouseRotation.x += Input.GetAxis("Mouse Y") * sensitivity * verticalSensitivityMultiplier;
        mouseRotation.x = Mathf.Clamp(mouseRotation.x, minX, maxX);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical   = Input.GetAxisRaw("Vertical");
        direction = transform.forward * vertical + transform.right * horizontal;

        transform.rotation = Quaternion.Euler(0.0f, mouseRotation.y, 0.0f);
        camHandle.rotation = Quaternion.Euler(mouseRotation.x, mouseRotation.y, 0.0f);

        if(Input.GetKeyDown(KeyCode.LeftShift)) multiplier = sprintMultiplier;
        else if(Input.GetKeyUp(KeyCode.LeftShift)) multiplier = 1.0f;

    }
    void FixedUpdate() {
        
        rb.AddForce(direction.normalized * speed * multiplier * Time.deltaTime * 100.0f, ForceMode.Acceleration);

    }

}