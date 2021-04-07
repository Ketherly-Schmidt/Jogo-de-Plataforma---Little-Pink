using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;//controla a velocidade do personagem
    public float JumpForce;//controla a força do pulo

    public bool isJumping;
    public bool doubleJump;
    private Rigidbody2D rig;

    private Animator animator;

    bool isBlowing;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);//horizontal é porque eu quero que ele se movimente apenas para os lados
        transform.position += movement * Time.deltaTime * Speed;//

        if(Input.GetAxis("Horizontal") > 0f){
            animator.SetBool("wlak", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f){
            animator.SetBool("wlak", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if(Input.GetAxis("Horizontal") == 0f){
            animator.SetBool("wlak", false);
        }
        

    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isBlowing){//realiza a conferencia se ele esta no chao, caso sim, ele pode pular novamente
            if (!isJumping){
                rig.AddForce(new Vector2 (0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                animator.SetBool("jump", true);

            }

            else{
                if(doubleJump){
                    rig.AddForce(new Vector2 (0f, JumpForce), ForceMode2D.Impulse);//realiza a verificação de que nao esta no chao e nao pula mais
                    doubleJump = false;
                }

            }

        }

    }

    void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.layer == 8){//detecta que ele esta colidindo alguma superficie do jogo
            isJumping = false;
            animator.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Spike"){//destroi o player quando ele tocar nos espinhos
            Controller.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Saw"){//destroi o player quando ele tocar na serra
            Controller.instance.ShowGameOver();
            Destroy(gameObject);
        }

    }

    void OnCollisionExit2D(Collision2D collision){

        if(collision.gameObject.layer == 8){//detecta que ele saiu da superficie, esta pulando ou em cima de alguma plataforma
            isJumping = true;
            animator.SetBool("jump", true);
            
        }
    }

    void OnTriggerStay2D (Collider2D collider){//detecta que o player esta no ventilador
        if (collider.gameObject.layer == 11){
            isBlowing = true;
        }
    }

    void OnTriggerExit2D (Collider2D collider){//detecta que o player não esta no ventilador
        if (collider.gameObject.layer == 11){
            isBlowing = false;
        }
    }
}
