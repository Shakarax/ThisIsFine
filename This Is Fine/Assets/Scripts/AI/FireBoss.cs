using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : MonoBehaviour {

    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private int bossScoreValue = 1000;
    [SerializeField] private List<string> damageSources;

    private SpriteRenderer bossSpriteRenderer;
    private Animator bossAnimator;
    private float immortalTime = 1;
    private bool immortal = false;
    private float deathTimer = 0;

    private bool IsDead { get; set; }
    public float CurrentHp { get { return currentHp; } }


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
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        bossAnimator = GetComponent<Animator>();
        currentHp = maxHp;
        IsDead = false;
	}
	
	// Update is called once per frame
	void Update () {
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
