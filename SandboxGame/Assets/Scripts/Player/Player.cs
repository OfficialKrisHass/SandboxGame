using UnityEngine;

public class Player : PhysicsObject {
    
    public override void OnHit(PhysicsObject other) {

        base.OnHit(other);

    }
    public override void OnDeath() {

        Debug.Log("Player Died!");

    }

}