using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

    public float angularSpeed = 1.0f;

    // Update is called once per frame
    void FixedUpdate ()
    {
        float rotateX = Input.GetAxis("Vertical");
        float rotateZ = -Input.GetAxis("Horizontal");

        Vector3 rotation = new Vector3(rotateX, 0.0f, rotateZ) * angularSpeed;

        transform.Rotate(rotation * Time.deltaTime);
	}
}
