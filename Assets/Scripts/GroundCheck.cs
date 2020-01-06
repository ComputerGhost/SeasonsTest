using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    const string GROUND_TAG = "Ground";
    const string GROUND_LAYER = "Ground";

    [SerializeField] Collider2D collider;
    [SerializeField] float rayLength = 0.5f;
    [SerializeField] float rayGap = 0.5f;

    public bool IsGrounded { get; internal set; } = false;
    public float Airtime { get { return IsGrounded ? 0f : Time.time - airStartTime; } }

    float airStartTime;

    float centerY { get { return collider.bounds.center.y; } }
    float left { get { return centerX - rayGap / 2; } }
    float centerX { get { return collider.bounds.center.x; } }
    float right { get { return centerX + rayGap / 2; } }

    void FixedUpdate()
    {
        bool nowGrounded =
            IsGroundedAt(new Vector2(left, centerY)) ||
            IsGroundedAt(new Vector2(centerX, centerY)) ||
            IsGroundedAt(new Vector2(right, centerY));
        if (IsGrounded && !nowGrounded)
            airStartTime = Time.time;
        IsGrounded = nowGrounded;
    }

    private bool IsGroundedAt(Vector2 position)
    {
        var hit = Physics2D.Raycast(position, Vector2.down, rayLength, LayerMask.GetMask(GROUND_LAYER));
        return hit.collider?.CompareTag(GROUND_TAG) ?? false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(left, centerY), new Vector2(left, centerY - rayLength));
        Gizmos.DrawLine(new Vector2(centerX, centerY), new Vector2(centerX, centerY - rayLength));
        Gizmos.DrawLine(new Vector2(right, centerY), new Vector2(right, centerY - rayLength));
    }

}
