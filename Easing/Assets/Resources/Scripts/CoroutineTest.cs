using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineTest : MonoBehaviour
{
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        text.text = "Nキーで先に進む";
        yield return new WaitUntil(Next);   //指定した関数がTrueなら先に進む

        text.text = "3秒待機";
        yield return new WaitForSeconds(3.0f);

        text.text = "見えるかな？";
        yield return null;                  //1フレーム遅延して再度実行

        text.text = "コルーチン処理終了\nSキーで再度スタート";
    }

    bool Next()
    {
        return Input.GetKeyDown(KeyCode.N);
    }

    void Test()
    {
        Debug.Log("この関数ではWaitUntilは使えない");
    }


}
