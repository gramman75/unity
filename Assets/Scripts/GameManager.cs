using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    float spawnTerm = 5f;
    float lastSpawnTime = 0;
    public GameObject player;
    public TextMeshProUGUI scoreText;
    float score;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        lastSpawnTime += Time.deltaTime;

        if (lastSpawnTime > spawnTerm)
        {
            lastSpawnTime -= spawnTerm;

            SpawnEnemy();
        }

        scoreText.text = ((int)score).ToString();
        
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-6f, 6f);
        GameObject obj = GetComponent<ObjectPool>().Get();
        obj.transform.position = new Vector3(x, y, 0);
        obj.GetComponent<EnemyController>().CallEnemy(player);

    }
}
