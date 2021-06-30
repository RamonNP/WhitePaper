using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class PlayerControllerInput : MonoBehaviour
{
    public GameObject controleMobile;
    public DialogController dialogController;
    public bool lookLeft;
    public bool tecladoInput;
    private Rigidbody2D rb2D;
    private float input_x = 0;
    private float input_xC = 0;
    private float input_y = 0;
    [SerializeField] private float speed;
    [SerializeField]private bool isWalking = false;
    Vector2 movement = Vector2.zero;
    Vector2 movementControle = Vector2.zero;
    private Animator playerAnimator;
    public GameObject bauAbrir;
    // Start is called before the first frame update
    void Start()
    {
        controleMobile.SetActive(isMobile());
        bauAbrir.SetActive(false);
        tecladoInput = !isMobile();
        rb2D = GetComponent<Rigidbody2D>(); 
        playerAnimator = GetComponent<Animator>(); 
    }

     public bool isMobile()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return WebGLHandler.IsMobile();
        #endif
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(tecladoInput){
            input_x = Input.GetAxisRaw("Horizontal");
            input_y = Input.GetAxisRaw("Vertical");
        } else {
            input_x = MSJoystickController.joystickInput.x;
            input_y = MSJoystickController.joystickInput.y;
        }
        isWalking = (input_x != 0 || input_y != 0);
        if(isWalking){
            playerAnimator.SetBool("isWalking", true);
        } else {
            playerAnimator.SetBool("isWalking", false);
        }
        //if(input_x > 0 || input_y > 0) {
            movement = new Vector2(input_x, input_y);
        //}
        //if(movementControle != 0)
        if(controle){

        //rb2D.MovePosition(rb2D.position + movementControle * speed * Time.fixedDeltaTime);
        } else {

        }
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
        VerificaFlipOLD(input_x);
        //VerificaFlipOLD(input_xC);
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
        if(other.tag == "NPC") {
            dialogController.transform.gameObject.SetActive(true);
            dialogController.initializeDialog(other.GetComponent<NpcSentence>().sentences, other.GetComponent<NpcSentence>().npcName);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        dialogController.transform.gameObject.SetActive(false);
        if(other.gameObject.name == "NPCABRIRBAU") {
            bauAbrir.SetActive(true);
        }
    }

public bool controle;
    public void UpDown(float input) {
        //print(input);
        input_y = input;
        if(input == 0){
            tecladoInput = true;
        } else {
            tecladoInput = false;
        }
        //movementControle = new Vector2(input_x, input_y);
        //controle =true;
    }
    public void RighLefht(float input) {
        //print(input);
        input_x = input;
        if(input == 0){
            tecladoInput = true;
        } else {
            tecladoInput = false;
        }
        //movementControle = new Vector2(input_x, input_y);
        //controle =true;
        //input_xC = input;
       // if(input > 0)
    }
}
