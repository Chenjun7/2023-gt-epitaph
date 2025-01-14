using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkDaggerProjectile : Projectile
{   
    public float acceleration;

    // Update is called once per frame
    void Update()
    {
        if (force > 0) {
            force += acceleration;
        }
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // This is a hack, ideally have a debug mode for AoE (foreshadowing?)
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {}
}
