using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nevergreen : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        Vector3 direction;

        direction = (player.transform.position- transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }
}
