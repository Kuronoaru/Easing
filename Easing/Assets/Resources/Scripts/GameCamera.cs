using UnityEngine;

/// <summary>
/// 追いかけゲームカメラ
/// </summary>
public class GameCamera : MonoBehaviour
{
    [SerializeField] private float trackingLerp = 0.1f;
    [SerializeField] private float interestForwardDistance = 0f;
    [SerializeField] private float heightShiftThreashold = 2f;
    [SerializeField] private Transform initTarget;

    private Transform target;
    private Vector3 cameraOffset;
    private float currentInterestY;

    void Awake()
    {
        cameraOffset = transform.position;
    }

    void Start()
    {
        // 初期追跡オブジェクトが設定されているときは、注視キャラとして設定
        // シーンに配置されている状態をカメラの位置と回転とする
        if (initTarget != null) {
            cameraOffset = transform.position - initTarget.position;
            SetTarget(initTarget);
        }
    }

    //Update関数の後に呼び出される関数、何かをプログラムで追跡したいとなるときはLateUpdateに処理を書いた方が安全、Update関数は処理の順番が不定
    void LateUpdate()
    {
        UpdatePosition(trackingLerp);
    }

    public void SetTarget(Transform aTarget)
    {
        target = aTarget;
        currentInterestY = target.position.y;
        UpdatePosition(1.0f);
    }

    //Targetを追跡する処理、線形補間を使い距離を0〜1の正規化された変数で掛ける、変数の値によってターゲットの距離の何％進むか決定する
    private void UpdatePosition(float aLerp)
    {
        var targetPos = target != null ? target.position : Vector3.zero;
        if (Mathf.Abs(targetPos.y - currentInterestY) > heightShiftThreashold) 
        {
            currentInterestY = targetPos.y;
        }
        targetPos.y = currentInterestY;
        var fwd = target != null ? target.forward : Vector3.zero;
        transform.position = Vector3.Lerp(transform.position, targetPos + fwd * interestForwardDistance + cameraOffset, aLerp);
    }
}
