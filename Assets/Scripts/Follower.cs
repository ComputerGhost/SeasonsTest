using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject target;
    public float speed = 2.0f;

    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, interpolation);
        position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, interpolation);

        this.transform.position = position;
    }
}
