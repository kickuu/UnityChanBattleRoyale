using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    void Start()
    {
        EnemyGenerator();
    }

    void Update()
    {

    }

    void EnemyGenerator()
    {
        var rand = Random.insideUnitCircle * 5;
        var teki = Instantiate(enemy);
        teki.transform.position = new Vector3(rand.x, 16.0f, rand.y);
    }
}
