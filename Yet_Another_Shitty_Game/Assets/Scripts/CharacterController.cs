using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject player;
    public GameObject character;
    public string characterName;
    public string[] messages;
    private Vector3 playerPosition;
    private Vector3 characterPosition;
    private string text;
    bool triggered;
    bool isTalkedTo = false;

    public int missionBriefinglength;
    public bool isMissionComplete = false;
    public bool doesFollow;
    public bool willFollow = false;

    // Enable only if followable
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public Transform groundCheckPoint;
    public Vector3 respawnPoint;
    private bool isTouchingGround;
    private Animator characterAnimation;

    public int i;

    PlayerController pn;

    // Start is called before the first frame update
    void Start()
    {
        pn = GameObject.Find("Player").GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
        characterAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
    }

    public void setMissionComplete(int x)
    {
        isMissionComplete = true;
        Debug.Log(characterName + "'s quest is complete! It is " + isMissionComplete +"!");
        setI(x);
    }

    private void setI(int x)
    {
        this.i = x;
    }

    // Update is called once per frame
    void Update()
    {
        if (doesFollow)
        {
            characterAnimation.SetBool("OnGround", isTouchingGround);
            characterAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
            isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
            movement = Input.GetAxis("Horizontal");
        }
        if (isMissionComplete)
        {
            i = missionBriefinglength + 1;
        }
        playerPosition = new Vector3(
            player.transform.position.x,
            transform.position.y,
            transform.position.z
        );
        characterPosition = new Vector3(
            character.transform.position.x,
            transform.position.y,
            transform.position.z
        );
        if (playerPosition.x < characterPosition.x)
        {
            character.transform.localScale = new Vector2(1f, 1f);
        }
        else if (playerPosition.x > characterPosition.x)
        {
            character.transform.localScale = new Vector2(-1f, 1f);
        }
        if (willFollow)
        {
            if (movement < 0)
            {
                if (playerPosition.x + 3 < characterPosition.x)
                    rigidBody.velocity = new Vector2(movement * speed * 1.25f, rigidBody.velocity.y);
            }
            else if (movement > 0)
            {
                if (playerPosition.x - 3 > characterPosition.x)
                    rigidBody.velocity = new Vector2(movement * speed * 1.25f, rigidBody.velocity.y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerPosition = new Vector3(
            player.transform.position.x,
            transform.position.y,
            transform.position.z
        );
        characterPosition = new Vector3(
            character.transform.position.x,
            transform.position.y,
            transform.position.z
        );
        if (other.gameObject.name == "Player")
        {
            triggered = true;
        }
        if (other.gameObject.name == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        if (other.tag == "JumpTriggers")
        {
            rigidBody.velocity = new Vector2(speed, jumpSpeed);
        }
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(character.transform.position);
        var textSize = GUI.skin.label.CalcSize(new GUIContent(text));
        if (triggered) //The dialogue starts.
        {
            if (isTalkedTo == false)
            {
                text = "Press Enter to talk!";
                GUI.contentColor = Color.black;
                GUI.Label(new Rect(position.x - 55, position.y - 60, textSize.x, textSize.y), text);
            }
            else if (isTalkedTo == true)
            {
                if (Input.GetKeyUp("space"))
                {
                    if (i < missionBriefinglength - 1)
                    {
                        i = i + 1;
                    }
                }
                text = characterName + ": " + messages[i];
                GUI.contentColor = Color.black;
                GUI.Label(new Rect(10, 400, textSize.x, textSize.y), text);
            }
            if (Input.GetKeyDown("return") && isTalkedTo == false)
            {
                isTalkedTo = true;
            }
            else if (Input.GetKeyDown("backspace") && isTalkedTo == true)
            {
                i = 0;
                isTalkedTo = false;
                if (doesFollow)
                {
                    willFollow = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            triggered = false;
        }
    }
}
