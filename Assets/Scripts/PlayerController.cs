using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    public Material flashMaterial;
    public Material defaultMaterial;

    public AudioClip shotSound;

    Vector3 move;         
    // Start is called before the first frame updat
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)))
        {
            move += new Vector3(-1, 0, 0); 
            //transform.Translate(new Vector3(-this.GetDistance(), 0, 0));
        };

        if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)))
        {
            move += new Vector3(1, 0, 0); 
            //transform.Translate(new Vector3(this.GetDistance(), 0));
        };

        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.W)))
        {
            move += new Vector3(0, 1, 0); 
            //transform.Translate(new Vector3(0, this.GetDistance()));
        };

        if (Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.S)))
        {
            move += new Vector3(0, -1, 0); 
            //transform.Translate(new Vector3(0, -this.GetDistance()));
        };

        move = move.normalized;

        if (move.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        };

        if (move.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        };

        if (move.magnitude > 0)
        {
            GetComponent<Animator>().SetTrigger("Move");
        } else
        {
            GetComponent<Animator>().SetTrigger("Stop");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }


    }

    public void Shot()
    {
        GetComponent<AudioSource>().PlayOneShot(shotSound);
        Vector3 wordPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        wordPosition.z = 0;
        wordPosition -= (transform.position + new Vector3(0, -0.5f, 0));

        //GameObject bullet = Instantiate<GameObject>(bulletPrefab);
        GameObject bullet = GetComponent<ObjectPool>().Get(); 
        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = transform.position + new Vector3(0, -0.5f);
            bullet.GetComponent<Bullet>().Direction = wordPosition;

        }

    }

    private void FixedUpdate()
    {
        transform.Translate(move * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Conflict");
        if (collision.gameObject.tag == "Enemy")
        {
            if (transform.GetComponent<Character>().Hit(1))
            {
                StartCoroutine(FlashMaterial());
            } else
            {
                StartCoroutine(Dying());

            }
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
        GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(1.7f);
        gameObject.SetActive(false);
        //Destroy(gameObject);
        SceneManager.LoadScene("GameOverScene");
        yield return null;

    }
}
