using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KinematicMover : Entity
{
    public Collider2D CollisionBox;
    public LayerMask collisionMask;
    public Rigidbody2D rb;
    Vector2 nextMove;
    public void KinematicMove(Vector2 newPos)
    {
        if (!CheckCollisionIn(newPos))
        {
            rb.MovePosition((Vector2)transform.position+newPos);
        } else
        {
        }
    }
    public void KinematicForceMove(Vector2 newPos)
    {
        rb.MovePosition((Vector2)transform.position + newPos);
    }
    public RaycastHit2D[] collisionResults { get; private set; }
    public bool CheckCollisionIn(Vector2 direction)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        filter.SetLayerMask(collisionMask);
        collisionResults = new RaycastHit2D[2];
        return (CollisionBox.Cast(direction, filter, collisionResults, direction.magnitude+.01f) > 0); 
    }
    public void UnStuck()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        filter.SetLayerMask(collisionMask);
        collisionResults = new RaycastHit2D[2];
        if (CollisionBox.Cast(Vector2.zero, filter, collisionResults, 0) > 0)
        {
            KinematicForceMove((Vector2)transform.position-collisionResults[0].point);
        }
    }
}
