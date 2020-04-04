﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    // NPCの列挙体
    public enum State
    {
        Wait,
        Walk,
        Talk
    }

    //　目的地
    private Vector3 destination;
    //　巡回する位置の親
    [SerializeField] private Transform patrolPointsParent = null;
    //　巡回する位置
    private Transform[] patrolPositions;
    //　次に巡回する位置
    private int nowPatrolPosition = 0;
    //　エージェント
    private NavMeshAgent navMeshAgent;
    //　アニメーター
    private Animator animator;
    //　村人の状態
    private State state;
    //　待機した時間
    private float elapsedTime;
    //　待機する時間
    [SerializeField] private float waitTime = 5f;
    //　会話内容保持スクリプト
    private string conversation = null;
    //　ユニティちゃんのTransform
    private Transform conversationPartnerTransform;
    //　村人がユニティちゃんの方向に回転するスピード
    [SerializeField] private float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        //　巡回地点を設定
        patrolPositions = new Transform[patrolPointsParent.transform.childCount];
        for (int i = 0; i < patrolPointsParent.transform.childCount; i++) {
            patrolPositions[i] = patrolPointsParent.transform.GetChild(i);
        }
        SetState(State.Wait);
    }

    // Update is called once per frame
    void Update()
    {
        //　見回り
        if (state == State.Walk) {
            //　エージェントの潜在的な速さを設定
            animator.SetFloat("Speed", navMeshAgent.desiredVelocity.magnitude);
 
            //　目的地に到着したかどうかの判定
            if (navMeshAgent.remainingDistance < 0.1f) {
                SetState(State.Wait);
            }
            //　到着していたら一定時間待つ
        } else if (state == State.Wait) {
            elapsedTime += Time.deltaTime;
 
            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime) {
                SetState(State.Walk);
            }
        } else if(state == State.Talk) {
            //　村人がユニティちゃんの方向をある程度向くまで回転させる
            // ユニティちゃんの位置から村人の位置を引いてユニティちゃんの方向を求めたものと村人の前方の角度をVector3.Angleで求め、5度より大きい時は村人をユニティちゃんの方向に向かせる。
            if (Vector3.Angle(transform.forward, new Vector3(conversationPartnerTransform.position.x, transform.position.y, conversationPartnerTransform.position.z) - transform.position) > 5f) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(conversationPartnerTransform.position.x, transform.position.y, conversationPartnerTransform.position.z) - transform.position), rotationSpeed * Time.deltaTime);
                animator.SetFloat("Speed", 1f);
            } else {
                animator.SetFloat("Speed", 0f);
            }
        }
    }

    //　村人の状態変更
    public void SetState(State state, Transform conversationPartnerTransform = null) {
        this.state = state;
        if (state == State.Wait) {
            elapsedTime = 0f;
            animator.SetFloat("Speed", 0f);
        } else if(state == State.Walk) {
            SetNextPosition(patrolPositions[nowPatrolPosition].position);
            navMeshAgent.SetDestination(GetDestination());
            navMeshAgent.isStopped = false;
        } else if(state == State.Talk) {
            navMeshAgent.isStopped = true;
            animator.SetFloat("Speed", 0f);
            this.conversationPartnerTransform = conversationPartnerTransform;
        }
    }

    //　Conversionスクリプトを返す
    public string GetConversation() {
        conversation = LoadConversation();
        return conversation;
    }

    // 会話文を取得
    private string LoadConversation() {
        // データ読み込み。
        string NPCConversation = File.ReadAllText("Assets/DungeonRPG/Conversation/conversation.txt", Encoding.GetEncoding("utf-8")); 
        return NPCConversation;
    }
 
    //　巡回地点を順に目的地として設定する
    public void SetNextPosition(Vector3 position) {
        destination = position;
        nowPatrolPosition++;
        // 一周したら初期地点
        if (nowPatrolPosition >= patrolPositions.Length) {
            nowPatrolPosition = 0;
        }
    }
 
    //　目的地を取得する
    public Vector3 GetDestination() {
        return destination;
    }
}
