using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // SerializeField to make the values modifiable in the Unity Editor
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Stats healthStat;
    [SerializeField] private List<string> damageSources;
    [SerializeField] private GameObject weapon;

    private Vector3 inputMovement;

    // Property variables
    public Animator MyAnimator { get; set; }
    public Rigidbody2D MyRigidbody2D { get; set; }
    public Transform MyTransform { get; set; }
    public bool IsDead { get; set; }
    public SpriteRenderer WeaponSprite { get; set; }

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

	}

    // Anything physics related should go in this update function
    private void FixedUpdate()
    {
        HandleMovement();
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


}
