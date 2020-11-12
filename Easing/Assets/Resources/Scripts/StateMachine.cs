using System.Collections.Generic;
using System;
using System.Diagnostics;

//<T>・・・ジェネリック(テンプレート)クラス、Tの部分が
//使用者の好きな型に変えれる
public class StateMachine<T>
{
    private class State
    {
        //readonly・・・再代入不可な変数修飾子
        //、関数の呼び出しやメンバ変数への代入は可能.
        private readonly Action enterAction;  
        // 開始時に呼び出されるデリゲート(関数)
        private readonly Action updateAction; 
        // 更新時に呼び出されるデリゲート(関数)
        private readonly Action exitAction;   
        // 終了時に呼び出されるデリゲート(関数)

        public State(Action updateAct = null
                    ,Action enterAct  = null
                    ,Action exitAct   = null)
        {
            // ??演算子・・・代入する変数の中身がnullの場合??の
            // 右側にあるものが代入され.
            // nullを許容したくないときに使える.
            updateAction  = updateAct ?? delegate { };
            enterAction   = enterAct  ?? delegate { };
            exitAction    = exitAct   ?? delegate { };
        }
        public void Enter()
        {
            enterAction();
        }

        public void Update()
        {
            updateAction();
        }

        public void Exit()
        {
            exitAction();
        }
    }

    private Dictionary<T, State> mStateDictionary = new Dictionary<T, State>();
    private State mCurrentState;
    private T keyState;

    public void Add(T key, Action updateAct = null
                        , Action enterAct = null
                        , Action exitAct = null)
    {
        mStateDictionary.Add(key, new State(updateAct
                                            , enterAct
                                            , exitAct));
    }

    public bool ChangeState(T key)
    {
        if (mStateDictionary.ContainsKey(key) == false)
        {
            return false;
        }
        //?演算子・・・変数の中身がnullでなければ
        //変数にアクセスする.
        mCurrentState?.Exit();
        mCurrentState = mStateDictionary[key];
        mCurrentState?.Enter();
        keyState = key;
        return true;
    }

    public void Update()
    {
        if (mCurrentState == null)
        {
            return;
        }
        mCurrentState.Update();
    }

    public void Clear()
    {
        mStateDictionary.Clear();
        mCurrentState = null;
    }

    public T NowState()
    {
        return keyState;
    }
}