using UnityEngine;
using UnityEngine.Events;

/// 画面ポインティング入力
public class PointInput : MonoBehaviour
{
    [SerializeField] private Camera camera = null;

    public PointerEvent onPoint = new PointerEvent();
    public PointerEvent onUpdatePoint = new PointerEvent();
    public PointerEvent onDrag = new PointerEvent();

    [System.Serializable] public class PointerEvent : UnityEvent<Vector3> { }

    void Awake()
    {
        OnValidate();
    }

    void OnValidate()
    {
        if (TryGetComponent(out camera) == false)
        {
            Debug.LogError("カメラが設定されていません");
        }
    }

    void Start()
    {

    }

    void LateUpdate()
    {
        RaycastHit hit;
        //スクリーン（カメラ）からマウスの座標にまっすぐレイを飛ばす
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
        if (Physics.Raycast(ray, out hit)) {
            OnUpdatePoint(hit.point);
            if (Input.GetMouseButtonUp(0)) {
                OnPoint(hit.point);
            }
            if (Input.GetMouseButton(0)) {
                OnDrag(hit.point);
            }
        }
    }

    // 画面上をドラッグ
    private void OnDrag(Vector3 aPos)
    {
        onDrag.Invoke(aPos);
    }

    // 画面上をクリック/タッチ
    private void OnPoint(Vector3 aPos)
    {
        onPoint.Invoke(aPos);
    }

    // 画面上をポインタ移動
    private void OnUpdatePoint(Vector3 aPos)
    {
        onUpdatePoint.Invoke(aPos);
    }

}