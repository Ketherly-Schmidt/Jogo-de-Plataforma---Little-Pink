using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public int Score;

    public GameObject collected;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            Controller.instance.totalScore += Score;
            Controller.instance.UpdateScoreText();

            Destroy(gameObject, 0.25f);

        }
    }
}
