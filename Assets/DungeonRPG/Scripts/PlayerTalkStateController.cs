using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalkStateController : MonoBehaviour
{
    public enum State
    {
        Normal,
        Talk
    }
    //　ユニティちゃんの状態
    private State state;
    //　ユニティちゃん会話処理スクリプト
    private PlayerTalkController playerTalkController;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        state = State.Normal;
        unityChanTalkScript = GetComponent<UnityChanTalkScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if (state == State.Normal) {
            if (characterController.isGrounded) {
                velocity = Vector3.zero;
    
                var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    
                if (input.magnitude > 0.1f) {
                    transform.LookAt(transform.position + input.normalized);
                    animator.SetFloat("Speed", input.magnitude);
                    if (input.magnitude > 0.5f) {
                        velocity += transform.forward * runSpeed;
                    } else {
                        velocity += transform.forward * walkSpeed;
                    }
                } else {
                    animator.SetFloat("Speed", 0f);
                }
    
                if(unityChanTalkScript.GetConversationPartner() != null
                    && Input.GetButtonDown("Jump")
                    ) {
                    SetState(State.Talk);
                }
            }
        } else if(state == State.Talk) {
    
        }
    
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    //　状態変更と初期設定
    public void SetState(State state) {
        this.state = state;
    
        if(state == State.Talk) {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            unityChanTalkScript.StartTalking();
        }
    }
    public State GetState() {
        return state;
    }
}
