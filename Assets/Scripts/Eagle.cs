using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Eagle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Animator animator;
    [SerializeField] GameObject oneT;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("Spin");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.z <= player.CurrentTravel-20)
            return;
        float posX = (player.transform.position.x - this.transform.position.x);
        if(posX != 0)
            transform.Translate(Vector3.left * posX * 1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if(this.transform.position.z <= player.CurrentTravel && player.gameObject.activeInHierarchy)
        {
            player.transform.SetParent(this.transform);
        }
    }

    public void SetUpTarget(Player target)
    {
        this.player = target;
    }
}
