using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        Spawn,
        Running,
        Dying
    }

    public float speed = 1;
    public GameObject target;

    public Material flashMaterial;
    public Material defaultMaterial;

    State state;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("Player");
        //state = State.Running;
    }

    public void CallEnemy(GameObject target)
    {
        StartCoroutine(Spawn(target));
    }

    public IEnumerator Spawn(GameObject target)
    {
        this.target = target;
        state = State.Spawn;
        GetComponent<Character>().Initialize();
        GetComponent<Animator>().SetTrigger("Spawn");
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        state = State.Running;
        GetComponent<Collider2D>().enabled = true;

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = target.transform.position - transform.position;

        if (this.state == State.Running)
        {
            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            transform.Translate(direction.normalized * this.speed * Time.fixedDeltaTime);

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            float d = collision.GetComponent<Bullet>().damage;
            if (GetComponent<Character>().Hit(d))
            {
                StartCoroutine(FlashMaterial());
                // 살아 있음.
            } else
            {
                //GetComponent<Animator>().SetTrigger("Die");
                //this.state = State.Dying;
                //Invoke("AfterDying", 1.7f);
                StartCoroutine(Dying());
            };
        }
    }

    public IEnumerator FlashMaterial()
    {
        GetComponent<SpriteRenderer>().material = this.flashMaterial;

        yield return new WaitForSeconds(0.3f);


        GetComponent<SpriteRenderer>().material = this.defaultMaterial;

        yield return null;
            
    }

    public IEnumerator Dying()
    {
        this.state = State.Dying;
        GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(1.7f);
        gameObject.SetActive(false);
        //Destroy(gameObject);
        yield return null;

    }

}
