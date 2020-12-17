using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    float interval = 5f;
    float time = 0f;

    void Start()
    {
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > interval)
        {
            EnemyGenerator();
            time = 0f;
        }
    }

    void EnemyGenerator()
    {
        var rand = Random.insideUnitCircle * 5;
        var teki = Instantiate(enemy);
        teki.transform.position = new Vector3(rand.x, 16.0f, rand.y);
    }
}
