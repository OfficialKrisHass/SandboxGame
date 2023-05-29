using UnityEngine;

public class Player : PhysicsObject {

    public static Player instance;
    private void Awake() { instance = this; }

    [Header("Body Parts")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform eyes;
    public Transform mainCam { get { return cam; } }
    public Transform camHandle { get {return eyes; } }

    [Header("Moving Objects")]
    [SerializeField] private KeyCode moveObjectKey = KeyCode.Mouse0;
    [SerializeField] private float maxObjDst = 100.0f;
    [SerializeField] private float objDst;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float zoomSpeed = 0.25f;
    [SerializeField] private Rigidbody movingObject;

    [Header("Rotating Objects")]
    [SerializeField] private float rotSpeed = 1.0f;
    [SerializeField] private KeyCode rotXPos = KeyCode.Keypad5;
    [SerializeField] private KeyCode rotXNeg = KeyCode.Keypad2;
    [SerializeField] private KeyCode rotYPos = KeyCode.Keypad4;
    [SerializeField] private KeyCode rotYNeg = KeyCode.Keypad6;
    [SerializeField] private KeyCode rotZPos = KeyCode.Keypad1;
    [SerializeField] private KeyCode rotZNeg = KeyCode.Keypad3;

    void Update() {
        
        if (Input.GetKeyDown(moveObjectKey))
            StartMovingObject();
        
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0.0f) objDst += zoomSpeed;
        else if(scroll < 0.0f) objDst -= zoomSpeed;

    }
    void FixedUpdate() {

        if(movingObject)
            MoveObject();
        
    }

    void StartMovingObject() {

        RaycastHit hit;
        if (!Physics.Raycast(eyes.position, eyes.forward, out hit, maxObjDst))
            return;

        PhysicsObject obj = hit.transform.GetComponent<PhysicsObject>();
        if(!obj || !obj.moveable)
            return;

        movingObject = hit.rigidbody;
        objDst = hit.distance;

    }
    void MoveObject() {

        Vector3 pos = eyes.position + eyes.forward * objDst;
        movingObject.AddForce(pos - movingObject.position, ForceMode.Impulse);

        Vector3 rot = movingObject.rotation.eulerAngles;
             if(Input.GetKey(rotXPos)) rot.x += rotSpeed;
        else if(Input.GetKey(rotXNeg)) rot.x -= rotSpeed;
        else if(Input.GetKey(rotYPos)) rot.y += rotSpeed;
        else if(Input.GetKey(rotYNeg)) rot.y -= rotSpeed;
        else if(Input.GetKey(rotZPos)) rot.z += rotSpeed;
        else if(Input.GetKey(rotZNeg)) rot.z -= rotSpeed;

        movingObject.rotation = Quaternion.Euler(rot);

    }
    
    public override void OnHit(PhysicsObject other) {

        base.OnHit(other);

    }
    public override void OnDeath() {

        Debug.Log("Player Died!");

    }

}