using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Normal,
        Talk,
        Wait
    }
    // キャラクターコントローラー
    private CharacterController characterController;
    // アニメーター
    private Animator animator;
    //　キャラクターの速度
    private Vector3 velocity;
    //　キャラクターの歩くスピード
    [SerializeField] private float walkSpeed = 1f;
    //　キャラクターの走るスピード
    [SerializeField] private float runSpeed = 2f;
    // ジャンプ力
    [SerializeField]
    private float jumpSpeed = 8f;      
    // 回転速度
    [SerializeField] private float rotateSpeed = 1f;
    //　ユニティちゃんの状態
    private State state;
    //　ユニティちゃん会話処理スクリプト
    private PlayerTalkDirector playerTalkDirector;

    private float h, v;
 
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        state = State.Normal;
        // state = State.Wait;
        playerTalkDirector = GetComponent<PlayerTalkDirector>();
    }
 
    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("LeftHorizontal");     //左右矢印キーの値(-1.0~1.0)
        v = Input.GetAxis ("LeftVertical");      //上下矢印キーの値(-1.0~1.0)
        
        if (state == State.Normal) {
            // 接地しているかどうか
            if (characterController.isGrounded) {
                // 速度を0に
                velocity = Vector3.zero;
                // 横軸の入力と縦軸の入力を格納し、移動方向を決定
                var dir = new Vector3(h, 0f, v);
                
                // 入力ベクトルの長さから判定
                if (dir.magnitude > 0.1f) { 
                    //ベクトルをローカル座標からグローバル座標へ変換
                    dir = transform.TransformDirection(dir);
                    
                    // 左右矢印キーの値だけ回転
                    gameObject.transform.Rotate (new Vector3 (0, rotateSpeed * h, 0));

                    // アニメーションパラメータのSpeedの値にinput.magnitudeの値を渡す
                    animator.SetBool("Jump", false);
                    animator.SetFloat("Speed", dir.magnitude);
                    
                    if (Input.GetButtonDown("Action")) {
                        dir.y = jumpSpeed;
                        animator.SetBool("Jump", true);
                    }
                    // 速度を加算
                    if (dir.magnitude > 0.5f) {
                        velocity += transform.forward * v * runSpeed;
                    } else {
                        velocity += transform.forward * v * walkSpeed;
                    }
                } else {
                    // アニメーションパラメータを0にしてIdle状態へと遷移させる。
                    animator.SetBool("Jump", false);
                    animator.SetFloat("Speed", 0f);
                }
                // 会話相手が見つかったときに、マウス左クリックをしたら会話状態に遷移
                if (playerTalkDirector.GetConversationPartner() != null && Input.GetButtonDown("Action")) {
                    SetState(State.Talk);
                }
            }
        } else if (state == State.Talk) {
            // 何もしない

        } 
        // else if (state == State.Wait) {
        //     // 会話相手が見つかったときに、マウス左クリックをしたら会話状態に遷移
        //     if (playerTalkDirector.GetConversationPartner() != null && Input.GetButtonDown("Action")) {
        //         SetState(State.Talk);
        //     }
        // }
        
        // 重力
        velocity.y += Physics.gravity.y * Time.deltaTime;
        // characterControllerのMoveメソッドの引数に速度を渡してキャラクターを移動させる
        characterController.Move(velocity * Time.deltaTime);
        
        // -- シーン遷移時のデフォルト位置問題対処（シーン中に移動させない）
        // Wait以外のときに重力処理と移動処理を反映させる
        // if (state != State.Wait) {
        //     // 重力
        //     velocity.y += Physics.gravity.y * Time.deltaTime;
        //     // characterControllerのMoveメソッドの引数に速度を渡してキャラクターを移動させる
        //     characterController.Move(velocity * Time.deltaTime);
        // // Waitのとき
        // } else {
        //     characterController.Move(velocity * Time.deltaTime);

        //     // 横軸の入力と縦軸の入力どちらの入力も0じゃないときNormalに遷移
        //     if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0f)  || !Mathf.Approximately(Input.GetAxis("Vertical"), 0f)) {
        //         SetState(State.Normal);
        //     }
        // }
    }

    //　状態変更と初期設定
    public void SetState(State state) {
        this.state = state;
    
        if(state == State.Talk) {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            playerTalkDirector.StartTalking();
        }
    }
    // 状態を取得
    public State GetState() {
        return state;
    }
}