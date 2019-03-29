using UnityEngine;
using System.Collections;

public class Passthrough : MonoBehaviour {
    public Transform[] parent;

    void OnTriggerEnter2D(Collider2D other)
    {
        parent = GetComponentsInParent<Transform>();
        parent[1].position = new Vector3(parent[1].position.x, parent[1].position.y, -3.0f);   
    }
    void OnTriggerExit2D(Collider2D other)
    {
        parent[1].position = new Vector3(parent[1].position.x, parent[1].position.y, 0);
    }
}
