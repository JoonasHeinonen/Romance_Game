using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{
    public GameObject player;
    PlayerController pc;

    public float speed = 5f;
    private float movement = 2f;
    private Rigidbody2D rigidBody;
    public Vector3 respawnPoint;
    public bool nearBusStop = false;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        Debug.Log(pc.getNearBusStop());
    }

    // Update is called once per frame
    void Update()
    {
        if (movement > 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
        } else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    public float getSpeed()
    {
        return this.speed;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    void setMovement(float movement)
    {
        this.movement = movement;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BusTeleporter")
        {
            transform.position = respawnPoint;
            nearBusStop = true;
        }
        if (other.gameObject.name == "Bus_Stop")
        {
            Debug.Log("The bus is next to the stop!");
            if (pc.getNearBusStop() == true)
            {
                setSpeed(0f);
                SoundManagerScript.PlaySound("busstop");
                Debug.Log("The bus is picking up you!");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Bus_Stop")
        {
            if (pc.getNearBusStop() == true)
            {
                nearBusStop = false;
            }
        }
    }
}
