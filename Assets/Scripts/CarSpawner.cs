using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject carSpawned;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] float minTimer = 3;
    [SerializeField] float maxTimer = 5;
    
    bool isRight;
    float timer = 3;
    private void Start()
    {
        isRight = Random.value > 0.5f ? true : false;
        timer = Random.Range(0,minTimer);

    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        timer = Random.Range(minTimer,maxTimer);

        var spawnPos = this.transform.position + Vector3.right*(isRight ? -(terrain.extent + 1) : terrain.extent + 1);
        var go = Instantiate(
            original: carSpawned,
            position: spawnPos,
            rotation: Quaternion.Euler(0, isRight ? 90 : -90, 0),
            parent: this.transform
        );

        var car = go.GetComponent<Car>();
        car.SetUp(terrain.extent);
    }
}
