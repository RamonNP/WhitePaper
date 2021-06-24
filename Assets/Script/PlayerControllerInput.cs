using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerInput : MonoBehaviour
{
    public DialogController dialogController;
    public bool lookLeft;
    private Rigidbody2D rb2D;
    private float input_x = 0;
    private float input_y = 0;
    [SerializeField] private float speed;
    [SerializeField]private bool isWalking = false;
    Vector2 movement = Vector2.zero;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); 
        playerAnimator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);
        if(isWalking){
            playerAnimator.SetBool("isWalking", true);
        } else {
            playerAnimator.SetBool("isWalking", false);
        }
        movement = new Vector2(input_x, input_y);
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
        VerificaFlipOLD(input_x);
    }
     public void VerificaFlipOLD(float x) {


         if (isWalking)
        {
            //playerAnimator.SetFloat("input_x", input_x);
            //playerAnimator.SetFloat("input_y", input_y);
            if (x > 0 && lookLeft == true)
            {
                Flip();
                //NetworkManager.instance.EmitAnimation ("Flip", x.ToString(), GetComponent<Player>().entity.id);
            } else if(x < 0 && lookLeft == false)
            {
                Flip();
                //NetworkManager.instance.EmitAnimation ("Flip", x.ToString(), GetComponent<Player>().entity.id);
            }
        }
    }
    private void Flip()
    {
        //USADO PELO SERVIDOR PARA ATUALIZAR POSIÇOES DE OUTROS JOGADORES
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
        lookLeft = !lookLeft;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        dialogController.transform.gameObject.SetActive(true);
        dialogController.initializeDialog(other.GetComponent<NpcSentence>().sentences, other.GetComponent<NpcSentence>().npcName);
    }

    private void OnTriggerExit2D(Collider2D other) {
        dialogController.transform.gameObject.SetActive(false);
    }
}
