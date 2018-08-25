using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // SerializeField to make the values modifiable in the Unity Editor
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Stats healthStat;
    [SerializeField] private List<string> damageSources;
    [SerializeField] private GameObject weapon;
    [SerializeField] private Text playerScoreText;
    

    private Vector3 inputMovement;
    private Vector3 startPosition;
    private int playerScoreCount;

    // Property variables
    public Animator MyAnimator { get; set; }
    public Rigidbody2D MyRigidbody2D { get; set; }
    public Transform MyTransform { get; set; }
    public bool IsDead { get; set; }
    public SpriteRenderer WeaponSprite { get; set; }
    public int PlayerScoreCount {
        get { return playerScoreCount; }
        set { playerScoreCount = value; }
    }

    // Creates a singleton of the Player so we dont make multiple instances of the doggo :D
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if(!instance) { instance = GameObject.FindObjectOfType<PlayerController>(); }
            return instance;
        }
    }

	// Use this for initialization
	private void Start ()
    {
        PlayerScoreCount = 0;
        healthStat.Initialize();
        IsDead = false;
        WeaponSprite = weapon.GetComponent<SpriteRenderer>();
        MyTransform = GetComponent<Transform>();
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
	}
	
	// Put anything non physics related that needs updating here
	private void Update ()
    {
        UpdateScoreCount();
	}

    // Anything physics related should go in this update function
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void UpdateScoreCount()
    {
        playerScoreText.text = PlayerScoreCount.ToString();
    }

    // Will handle movement of the player
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativePoint = transform.InverseTransformPoint(worldPosition);

        MyAnimator.SetFloat("SpeedX", relativePoint.x);
        MyAnimator.SetFloat("SpeedY", relativePoint.y);

        inputMovement = new Vector2(horizontal, vertical);
        // Moves the player based on input * consistent time frame * speed withini the world space
        transform.Translate(inputMovement * Time.deltaTime * movementSpeed, Space.World);


        if (horizontal != 0 || vertical != 0){
            MyAnimator.SetBool("walking", true);
        } else {
            MyAnimator.SetBool("walking", false);
        }

    }

    // put him in level change function
    private Transform PlayerSpawnHandler()
    {
        return GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    // TODO add level change function
}
