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
        dialogController.transform.gameObject.SetActive(false);
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
        movement = new Vector2(input_x, input_y);

        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
        VerificaFlipOLD(input_x);
    }
     public void VerificaFlipOLD(float x) {
        if (isWalking)
        {
            if (x > 0 && lookLeft == true)
            {
                Flip();
            } else if(x < 0 && lookLeft == false)
            {
                Flip();
            }
        }
    }
    private void Flip()
    {
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
        lookLeft = !lookLeft;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        NpcSentence npc = other.transform.GetComponent<NpcSentence>();
        if(other.tag == "NPC_GUARDIAN") {
            dialogController.transform.gameObject.SetActive(true);
            dialogController.initializeDialog(npc.sentences, npc.npcName, npc.currentSentence);
        } else if(other.tag == "NPC") {
            dialogController.transform.gameObject.SetActive(true);
            dialogController.initializeDialog(npc.sentences, npc.npcName, npc.currentSentence);
            if(npc.nextNpcQuest != null){
                npc.nextNpcQuest.currentSentence = npc.sentenceQuestNextNpc;
            }
        } else if(other.tag == "PORTAL") {
            Transform obj =  other.transform.GetChild(0);
            obj.gameObject.GetComponent<NpcSentence>().changeAnimation();
            obj =  other.transform.GetChild(1);
            obj.gameObject.GetComponent<NpcSentence>().changeAnimation();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.name == "NPCABRIRBAU") {
            if(other.gameObject.GetComponent<NpcSentence>().currentSentence == 1){
                bauAbrir.SetActive(true);
            }
            dialogController.transform.gameObject.SetActive(false);
        } else if(other.tag == "NPC_GUARDIAN") {
            dialogController.transform.gameObject.SetActive(false);
        }else if(other.tag == "NPC") {
            dialogController.transform.gameObject.SetActive(false);
        } else if(other.tag == "PORTAL") {
            Transform obj =  other.transform.GetChild(0);
            obj.gameObject.GetComponent<NpcSentence>().changeAnimation();
            obj =  other.transform.GetChild(1);
            obj.gameObject.GetComponent<NpcSentence>().changeAnimation();

        }
    }

public bool controle;
    public void UpDown(float input) {
        input_y = input;
        if(input == 0){
            tecladoInput = true;
        } else {
            tecladoInput = false;
        }
    }
    public void RighLefht(float input) {
        input_x = input;
        if(input == 0){
            tecladoInput = true;
        } else {
            tecladoInput = false;
        }
    }
}
