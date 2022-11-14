using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    [SerializeField] GameObject Eagle;
    [SerializeField] Player player;
    [SerializeField] int spawnZPos = 7;
    [SerializeField] float timeOut = 20;

    float timer = 0;
    int playerLastMaxTravel = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.MaxTravel != playerLastMaxTravel)
        {
            //Reset Timer if Player moving
            timer=0;
            playerLastMaxTravel = player.MaxTravel;
            return;
        }

        //Timer if not move
        if(timer < timeOut)
        {
            timer += Time.deltaTime;
            return;
        }

        //If Timer equal to TimeOut
        if(player.IsJumping() == false && player.isDead == false)
            SpawnEagle();
    }

    private void SpawnEagle()
    {
        player.enabled = false;
        var position = new Vector3(0,1, player.CurrentTravel + spawnZPos);
        var rotation = Quaternion.Euler(0,180,0);
        var eagleObject = Instantiate(Eagle, position, rotation);
        var eagle = eagleObject.GetComponent<Eagle>();
        eagle.SetUpTarget(player);
    }

}
