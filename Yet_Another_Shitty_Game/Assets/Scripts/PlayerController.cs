using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private Animator playerAnimation;
    public Vector3 respawnPoint;
    public LevelManager gameLevelManager;

    private int money = 0;
    private int peopleInGroup = 1;
    private bool isNearCharacter = false;
    public bool isTalking;

    // Kenttäkohtaiset asiat
    private int succeededMissions = 0;
    private bool isNearGoofy = false;
    private bool talkedToGoofy = false;
    private string text;
    private string currentMission;
    private bool isGoofyQuestCompleted = false;
    private bool hasMotorOil = false;
    private bool nearBusStop = false;

    public GameObject bus;

    CharacterController gn;
    BusController bc;

    private bool movingToLeft = false;
    private bool movingToRight = false;
    private bool fixMove = false;
    public float timeLeft = 0.5f;

    public bool getNearBusStop()
    {
        return nearBusStop;
    }

    public int getMoney()
    {
        return money;
    }

    public void decMoney(int m)
    {
        money -= m;
    }

    public int getPeopleInGroup()
    {
        return peopleInGroup;
    }

    public void incPeopleInGroup()
    {
        peopleInGroup++;
    }

    // Use this for initialization
    void Start()
    {
        bc = GameObject.Find("Bus").GetComponent<BusController>();
        gn = GameObject.Find("Goofy").GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        gameLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0f)
        {
            fixMove = false;
        }
        if (fixMove == true)
        {
            timeLeft -= Time.deltaTime;
            if (movingToLeft)
            {
                transform.localScale = new Vector2(1f, 1f);
            } else
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
        } else if (fixMove == false)
        {
            timeLeft = 0.5f;
        }
        if (isGoofyQuestCompleted)
        {
            hasMotorOil = false;
        }
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        movement = Input.GetAxis("Horizontal");
        if (isTalking == false && fixMove == false)
        {
            if (movement > 0f)
            {
                rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(1f, 1f);
                movingToLeft = false;
                movingToRight = true;
            }
            else if (movement < 0f)
            {
                rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(-1f, 1f);
                movingToRight = false;
                movingToLeft = true;
            }
            else
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
            if (Input.GetButtonDown("Jump") && isTouchingGround)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }
            playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
            playerAnimation.SetBool("OnGround", isTouchingGround);
        }
        if (Input.GetKeyDown("return") && isNearCharacter)
        {
            isTalking = true;
        }
        if (Input.GetKeyDown("backspace") && isTalking == true)
        {
            isTalking = false;
            if (isNearGoofy)
            {
                talkedToGoofy = true;
            }
        }
        if (isNearGoofy == true && talkedToGoofy == true && isGoofyQuestCompleted == false)
        {
            currentMission = "Get motor oil!";
        }
        if (hasMotorOil)
        {
            currentMission = "Return to Goofy!";
        }
        if (isNearGoofy == true && hasMotorOil == true)
        {
            hasMotorOil = false;
            isGoofyQuestCompleted = true;
            succeededMissions++;
            money += 10;
            currentMission = "";
            gn.setMissionComplete(8);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FallDetector")
        {
            gameLevelManager.Respawn();
        }
        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
        if (other.tag == "ArenaTriggers")
        {
            if (movingToRight)
            {
                fixMove = true;
                rigidBody.velocity = new Vector2((movement * speed) / -1, rigidBody.velocity.y);
            }
            else if (movingToLeft)
            {
                fixMove = true;
                rigidBody.velocity = new Vector2((movement * speed) / -1, rigidBody.velocity.y);
            }
        }
        if (other.gameObject.tag == "Coin")
        {
            money++;
        }
        if (other.gameObject.tag == "Characters")
        {
            isNearCharacter = true;
            Debug.Log("You are near " + other.gameObject.name + "!");
        }
        if (other.gameObject.name == "Bus")
        {
            if (bc.getStopped() == true)
            {
                isNearCharacter = true;
                Debug.Log("You are near " + other.gameObject.name + "!");
            }
        }
        if (other.gameObject.name == "Goofy")
        {
            isNearGoofy = true;
        }
        if (other.gameObject.name == "Motor_oil")
        {
            hasMotorOil = true;
        }
        if (other.gameObject.name == "Bus_Stop")
        {
            nearBusStop = true;
            // Debug.Log("Is player near to bus stop: " + nearBusStop);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Characters")
        {
            isNearCharacter = false;
            // Debug.Log(isNearCharacter);
        }
        if (other.gameObject.tag == "Services")
        {
            isNearCharacter = false;
            // Debug.Log(isNearCharacter);
        }
        if (other.gameObject.name == "Goofy")
        {
            isNearGoofy = false;
        }
        if (other.gameObject.name == "Bus_Stop")
        {
            nearBusStop = false;
            // Debug.Log("Is player near to bus stop: " + nearBusStop);
            bc.setStopped(false);
        }
    }

    void OnGUI()
    {
        text = "Current mission: " + currentMission;
        var textSize = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(10, 10, textSize.x, textSize.y), text);
        GUI.Label(new Rect(10, 26, 500, textSize.y), "Succeeded missions: " + succeededMissions);
        GUI.Label(new Rect(10, 42, 500, textSize.y), "Money: " + money);
    }
}
