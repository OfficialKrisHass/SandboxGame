using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [Header("Movement")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float sprintMultiplier = 1.2f;

    [Header("Looking around")]
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private float minX = -70.0f;
    [SerializeField] private float maxX = 10.0f;

    [Header("Third Person")]
    [SerializeField] private bool thirdPerson = true;
    [SerializeField] private Vector3 camOffset = new Vector3(0.0f, 1.35f, -3.35f);
    [SerializeField] private float camXAxisRotOffset = 9.0f;
    [SerializeField] private float tpMinX = -70.0f;
    [SerializeField] private float tpMaxX = 10.0f;

    [Header("Body Parts")]
    [SerializeField] private Transform camHandle;
    [SerializeField] private Transform cam;

    private Rigidbody rb;

    private Vector3 direction;
    private float multiplier = 1.0f;

    private Vector2 mouseRotation;

    private bool thirdPersonLast;

    void Awake() {
        
        rb = GetComponent<Rigidbody>();
        thirdPersonLast = thirdPerson;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if(thirdPerson) {

            cam.transform.localPosition = camOffset;
            cam.transform.localRotation = Quaternion.Euler(camXAxisRotOffset, 0.0f, 0.0f);

        } else {

            cam.transform.localPosition = new Vector3(0.0f, 0.8f, 0.0f);
            cam.transform.localRotation = camHandle.rotation;

        }

    }

    void Update() {

        if(!thirdPersonLast && thirdPerson) {

            cam.transform.localPosition = camOffset;
            cam.transform.localRotation = Quaternion.Euler(camXAxisRotOffset, 0.0f, 0.0f);

        } else if(thirdPersonLast && !thirdPerson) {

            cam.transform.localPosition = new Vector3(0.0f, 0.8f, 0.0f);
            cam.transform.localRotation = camHandle.rotation;

        }
        
        mouseRotation.y += Input.GetAxis("Mouse X") * sensitivity;
        mouseRotation.x -= Input.GetAxis("Mouse Y") * sensitivity;
        
        if (thirdPerson)
            mouseRotation.x = Mathf.Clamp(mouseRotation.x, tpMinX, tpMaxX);
        else
            mouseRotation.x = Mathf.Clamp(mouseRotation.x, minX, maxX);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical   = Input.GetAxisRaw("Vertical");
        direction = transform.forward * vertical + transform.right * horizontal;

        transform.rotation = Quaternion.Euler(0.0f, mouseRotation.y, 0.0f);
        if (thirdPerson)
            camHandle.rotation = Quaternion.Euler(mouseRotation.x, mouseRotation.y, 0.0f);
        else
            cam.rotation = Quaternion.Euler(mouseRotation.x, mouseRotation.y, 0.0f);

        if(Input.GetKeyDown(KeyCode.LeftShift)) multiplier = sprintMultiplier;
        else if(Input.GetKeyUp(KeyCode.LeftShift)) multiplier = 1.0f;

        thirdPersonLast = thirdPerson;

    }
    void FixedUpdate() {
        
        rb.AddForce(direction.normalized * speed * multiplier * Time.deltaTime * 100.0f, ForceMode.Acceleration);

    }

}