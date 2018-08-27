using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireBoss : MonoBehaviour {

    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private int bossScoreValue = 1000;
    [SerializeField] private List<string> damageSources;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private BoxCollider2D ignoreFireCollider; 

    private SpriteRenderer bossSpriteRenderer;
    private Animator bossAnimator;
    private float immortalTime = 1;
    private bool immortal = false;
    private float deathTimer = 0;
    private GameObject[] destinations;
    private Vector2 destinationPoint;
    private Vector2 target;

    private bool IsDead { get; set; }
    public float CurrentHp { get { return currentHp; } }
    public int Damage { get; set; }
    private Transform currentDestination;
    private int currentDestinationIndex;

    private static FireBoss instance;
    public static FireBoss Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<FireBoss>(); }
            return instance;
        }
    }

    // Use this for initialization
    void Start () {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ignoreFireCollider, true);
        transform.position = new Vector2(GameObject.FindGameObjectWithTag("BossSpawnPoint").transform.position.x, GameObject.FindGameObjectWithTag("BossSpawnPoint").transform.position.y);
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        bossAnimator = GetComponent<Animator>();
        currentHp = maxHp;
        Damage = 25;
        IsDead = false;
        destinations = GameObject.FindGameObjectsWithTag("BossPatrolPoint");
        for(int i = 0; i < destinations.Length; i++)
        {
            Debug.Log(destinations[i].name.ToString() + " Index: " + i);
        }
        currentDestinationIndex = 0;
        currentDestination = destinations[currentDestinationIndex].transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (CurrentHp > 0 && destinations.Length > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentDestination.transform.position, Time.deltaTime * movementSpeed);
            // Check to see if we reached patrol point and if yes then change to next patrol point
            if (Vector3.Distance(transform.position, currentDestination.position) < .5f)
            {
                if (currentDestinationIndex + 1 < destinations.Length)
                {
                    currentDestinationIndex++;
                }
                else
                {
                    currentDestinationIndex = 0;
                }
                currentDestination = destinations[currentDestinationIndex].transform;
            }
        }
        // Turn to face destination
        //Vector3 destinationDirection = currentDestination.position - transform.position;
        //float angle = Mathf.Atan2(destinationDirection.y, destinationDirection.x) * Mathf.Rad2Deg - 90f;
        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);

        // Handles death
        if (currentHp <= 0)
        {
            IsDead = true;
            bossAnimator.SetTrigger("Death");
            deathTimer += Time.deltaTime;

            if (deathTimer >= 2f)
            {
                Destroy(gameObject);
            }

        }
    }

    private IEnumerator TakeDamage(Collider2D collision)
    {
        if (currentHp > 0 && !immortal)
        {
            PlayerController.Instance.PlayerScoreCount += bossScoreValue;
            if (collision.tag == "ShotgunBullet")
            {
                currentHp -= PlayerController.Instance.Shotgun.GetComponent<ShotgunController>().Damage;
            }
            if (collision.tag == "PistolBullet")
            {
                currentHp -= PlayerController.Instance.Pistol.GetComponent<PistolController>().Damage;
            }

            if (!IsDead)
            {
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            bossSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            bossSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageSources.Contains(collision.tag)) { StartCoroutine(TakeDamage(collision)); }
    }
}
