using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Player animal;
    [SerializeField] Vector3 offset;
    Vector3 lastAnimalPos;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - animal.transform.position;
    }

    // Update is called once per frame
    
    void Update()
    {
        if(animal.isDead || lastAnimalPos == animal.transform.position)
            return;

        var targetAnimalPos = new Vector3(
            animal.transform.position.x,
            0,
            animal.transform.position.z
        );

        transform.position = targetAnimalPos + offset;

        lastAnimalPos = animal.transform.position;
    }
}
