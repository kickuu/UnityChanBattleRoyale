using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    void Start()
    {
    }

    void Update()
    {

    }

    void EnemyGenerator()
    {
        Instantiate(enemy);
    }
}
