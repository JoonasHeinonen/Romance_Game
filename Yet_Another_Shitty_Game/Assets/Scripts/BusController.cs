using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{
    public GameObject player;
    public GameObject busObject;
    public string name;
    public string[] messages;
    private int price = 3;
    public int missionBriefinglength;
    PlayerController pc;

    public float speed = 5f;
    private float movement = 2f;
    private Rigidbody2D rigidBody;
    public Vector3 respawnPoint;
    public bool nearBusStop = false;
    private bool busStopped = false;
    bool interactingWithBus = false;
    bool triggered;
    public bool boughtATicket = false;

    private string text;

    public int i;


    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (boughtATicket == false) { 
        // Debug.Log(pc.getPeopleInGroup() + "Price of Tickets: " + price + "$");
            if (busStopped == false)
            {
                if (busStopped == false)
                {
                    if (speed <= 5f)
                    {
                        speed += 0.05f;
                    }
                }
                if (movement > 0f)
                {
                    rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
                    transform.localScale = new Vector2(1f, 1f);
                } else
                {
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }
            }
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
        if (other.gameObject.name == "Bus_Stop" && pc.getNearBusStop() == true)
        {
            setSpeed(0f);
            setStopped(true);
            // SoundManagerScript.PlaySound("busstop");
            triggered = true;
            // Debug.Log("The bus is picking up you!");
            // Debug.Log(getStopped());
        }
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(busObject.transform.position);
        var textSize = GUI.skin.label.CalcSize(new GUIContent(text));
        if (triggered) //The dialogue starts.
        {
            if (interactingWithBus == false)
            {
                text = "Press Enter to use the bus service!";
                GUI.contentColor = Color.white;
                GUI.Label(new Rect(position.x - 55, position.y - 180, textSize.x, textSize.y), text);
            }
            else if (interactingWithBus == true)
            {
                if (Input.GetKeyUp("space"))
                {
                    if (i < missionBriefinglength - 1 && pc.getMoney() < price)
                    {
                        i = i + 1;
                    }
                }
                if (Input.GetKeyUp(KeyCode.Y))
                {
                    Debug.Log("Hop aboard!");
                    pc.decMoney(price);
                    boughtATicket = true;
                } else if (Input.GetKeyUp(KeyCode.N) && interactingWithBus == true)
                {
                    i = 0;
                    interactingWithBus = false;
                }
                text = messages[i];
                GUI.contentColor = Color.black;
                GUI.Label(new Rect(10, 400, textSize.x, textSize.y), text);
            }
            if (Input.GetKeyDown("return") && interactingWithBus == false && triggered == true && boughtATicket == false)
            {
                interactingWithBus = true;
            }
            else if (Input.GetKeyDown("backspace") && interactingWithBus == true)
            {
                i = 0;
                interactingWithBus = false;
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
                triggered = false;
            }
        }
        if (other.gameObject.name == "Player")
        {
            triggered = false;

            if (pc.getNearBusStop() == true)
            {
                setStopped(false);
            }

            // Debug.Log(getStopped());
        }
    }

    public void setPrice(int price)
    {
        this.price = price;
    }

    public int getPrice()
    {
        return price;
    }

    public void setStopped(bool stopped)
    {
        if (stopped == false)
        {
            setSpeed(0.05f);
        }
        this.busStopped = stopped;
    }

    public bool getStopped()
    {
        return busStopped;
    }
}
