using UnityEngine;

public abstract class PhysicsObject : MonoBehaviour {

    [SerializeField] protected float health;
    
    [SerializeField] private Rigidbody rb;

    public Rigidbody rigidBody { get { return rb; } }

    void Awake() {
        
        if (rb != null) return;

        rb = GetComponent<Rigidbody>();

    }

    public virtual void OnHit(PhysicsObject other) {

        health -= Mathf.Abs((rb.velocity - other.rb.velocity).magnitude) * other.rb.mass;
        if (health <= 0.0f)
            OnDeath();

    }
    public virtual void OnDeath() {

        Destroy(gameObject);

    }

    void OnCollisionEnter(Collision other) {
        
        PhysicsObject obj = other.gameObject.GetComponent<PhysicsObject>();
        if (!obj) return;

        OnHit(obj);

    }

}