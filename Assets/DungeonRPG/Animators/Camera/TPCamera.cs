using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode, DisallowMultipleComponent]
public class TPCamera : MonoBehaviour {
    
    // private GameObject player;       //プレイヤー格納用
    // [SerializeField]
    // private float rotateSpeed = 3f; //回転させるスピード


	// // Use this for initialization
	// void Start () {
    //     //unitychanをplayerに格納
    //     player = GameObject.Find("Player");
	// }
	
	// // Update is called once per frame
    // void Update () {
    //     //左シフトが押されている時
    //     if (Input.GetKey(KeyCode.LeftShift))
    //     {
    //         //ユニティちゃんを中心に-5f度回転
    //         transform.RotateAround(player.transform.position, Vector3.up, -rotateSpeed);
    //     }
    //     //右シフトが押されている時
    //     else if(Input.GetKey(KeyCode.RightShift))
    //     {
    //         //ユニティちゃんを中心に5f度回転
    //         transform.RotateAround(player.transform.position, Vector3.up, rotateSpeed);
    //     }
    // private GameObject mainCamera;              //メインカメラ格納用
    // private GameObject playerObject;            //回転の中心となるプレイヤー格納用
    // public float rotateSpeed = 2.0f;            //回転の速さ
 
    // //呼び出し時に実行される関数
    // void Start()
    // {
    //     //メインカメラとユニティちゃんをそれぞれ取得
    //     mainCamera = Camera.main.gameObject;
    //     playerObject = GameObject.Find("Player");
    // }
 
 
    // //単位時間ごとに実行される関数
    // void Update()
    // {
    //     //rotateCameraの呼び出し
    //     rotateCamera();
    // }
 
    // //カメラを回転させる関数
    // private void rotateCamera()
    // {
    //     //Vector3でX,Y方向の回転の度合いを定義
    //     Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed,Input.GetAxis("Mouse Y") * rotateSpeed, 0);
 
    //     //transform.RotateAround()をしようしてメインカメラを回転させる
    //     mainCamera.transform.RotateAround(playerObject.transform.position, Vector3.up, angle.x);
    //     mainCamera.transform.RotateAround(playerObject.transform.position, transform.right, angle.y);
    // }

    public GameObject target; // an object to follow
    public Vector3 offset; // offset form the target object

    [SerializeField] private float distance = 4.0f; // distance from following object
    [SerializeField] private float polarAngle = 45.0f; // angle with y-axis
    [SerializeField] private float azimuthalAngle = 45.0f; // angle with x-axis

    [SerializeField] private float minDistance = 1.0f; // ホイールで近づける最小距離
    [SerializeField] private float maxDistance = 7.0f; // ホイールで遠ざける最大距離
    [SerializeField] private float minPolarAngle = 5.0f; // 向ける上下アングルの最小角度
    [SerializeField] private float maxPolarAngle = 75.0f; // 向ける上下アングルの最大角度
    [SerializeField] private float mouseXSensitivity = 5.0f; // マウスX軸のセンシ
    [SerializeField] private float mouseYSensitivity = 5.0f; // マウスY軸のセンシ
    [SerializeField] private float scrollSensitivity = 5.0f; // スクロールのセンシ

    void Start () {
        //Playerをtargetに格納
        target = GameObject.Find("Player");
	}

    void LateUpdate()
    {
        // マウスの右クリック押したら
        if (Input.GetMouseButton(1)) {
            // アングルを更新
            updateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        // スクロールで距離更新
        updateDistance(Input.GetAxis("Mouse ScrollWheel"));

        // ターゲットの位置とオフセットの和を格納
        var lookAtPos = target.transform.position + offset;
        // カメラの向きを更新
        updatePosition(lookAtPos);
        // 指定した方向にカメラを向かせる
        transform.LookAt(lookAtPos);
    }

    // アングルを更新
    void updateAngle(float x, float y)
    {
        // 現在のX軸アングルから、マウスのX軸の移動距離を引いた値を格納
        x = azimuthalAngle - x * mouseXSensitivity;
        // 新しいX軸のアングルをxから360度の間で決める
        azimuthalAngle = Mathf.Repeat(x, 360);

        // 現在のY軸アングルから、マウスのY軸の移動距離を引いた値を格納
        y = polarAngle + y * mouseYSensitivity;
        // 新しいY軸のアングルを上下アングルの最小角度から上下アングルの最大距離の間で決める
        polarAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
    }

    // キャラクターのまで距離を更新
    void updateDistance(float scroll)
    {
        // 現在距離からマウスのスクロール距離を引いた値を格納
        scroll = distance - scroll * scrollSensitivity;
        // 新しい距離をホイールで近づける最小距離から最大距離の間で決める
        distance = Mathf.Clamp(scroll, minDistance, maxDistance);
    }

    // カメラの向きを更新
    void updatePosition(Vector3 lookAtPos)
    {
        // 角度をラジアンに変換
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        // 新しくカメラの向きを更新(球面座標を直交座標に変換)
        // https://ja.wikipedia.org/wiki/%E7%90%83%E9%9D%A2%E5%BA%A7%E6%A8%99%E7%B3%BB
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }
}