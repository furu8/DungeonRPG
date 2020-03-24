using UnityEngine;

// 属性
[ExecuteInEditMode, DisallowMultipleComponent]
// ExecuteInEditMode：コンポーネントのUpdateやStartといったイベントを、ゲームを再生しない状態でも動作。ランタイムで動作する挙動を確認する際に便利。
// DisallowMultipleComponent：同一オブジェクトに複数のコンポーネントを追加不可にする。

public class PlayerTPCamera : MonoBehaviour {

    public GameObject target; // an object to follow
    public Vector3 offset; // offset form the target object

    [SerializeField] private float distance = 4.0f; // distance from following object
    [SerializeField] private float polarAngle = 45f; // angle with y-axis
    [SerializeField] private float azimuthalAngle = 270f; // angle with x-axis

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

    // カメラはPlayerよりもあとに必ずUpdateするためにLateUpdate
    void LateUpdate()
    {
        // マウスの右クリック押したら
        if (Input.GetMouseButton(1)) {
            // アングルを更新
            updateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        // スクロールで距離更新
        updateDistance(Input.GetAxis("Mouse ScrollWheel"));

        // ターゲットの位置とオフセット(微調整用の変数)の和を格納
        var lookAtPos = target.transform.position + offset;
        // カメラの向きを更新
        updateCameraPosition(lookAtPos);
        // 指定した方向にカメラを向かせる
        transform.LookAt(lookAtPos);
    }

    // ターゲットに対するアングルを更新
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

    // ターゲットのまで距離を更新
    void updateDistance(float scroll)
    {
        // 現在距離からマウスのスクロール距離を引いた値を格納
        scroll = distance - scroll * scrollSensitivity;
        // 新しい距離をホイールで近づける最小距離から最大距離の間で決める
        distance = Mathf.Clamp(scroll, minDistance, maxDistance);
    }

    // ターゲットの位置からカメラの位置を計算
    void updateCameraPosition(Vector3 lookAtPos)
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