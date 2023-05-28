using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    void Awake() {
        
        instance = this;

    }

    [Header("Wind")]
    public bool windActive = false;
    public Vector3 windDirection = Vector3.zero;
    public float windForce = 0.0f;

}