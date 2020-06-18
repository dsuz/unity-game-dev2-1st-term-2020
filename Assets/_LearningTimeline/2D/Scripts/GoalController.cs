using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Player（タグが付いたオブジェクト）がゴールに接触した時の処理をするためのコンポーネント
/// 接触判定がトリガーでなく距離なのは、トリガーに接触すると、使っているアセットの「攻撃をくらったアニメーション」が再生されてしまうため
/// </summary>
public class GoalController : MonoBehaviour
{
    /// <summary>Player タグが付いたオブジェクト</summary>
    GameObject m_player;
    /// <summary>ゴールしたと判定する距離</summary>
    [SerializeField] float m_goalDistance = 0.2f;
    /// <summary>ゴールした時に再生するもの</summary>
    [SerializeField] PlayableDirector m_playable;
    /// <summary>ゴールしたかどうかを判定するフラグ。ゴール判定を一回しかやりたくないために用意した。</summary>
    bool m_isFinished;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player)
        {
            Debug.Log("Player found: " + m_player.name);
        }
    }

    private void FixedUpdate()
    {
        if (m_isFinished) return;   // 既にゴールした場合は何もしない

        float distance = Vector3.Distance(this.transform.position, m_player.transform.position);    // ゴールしたかどうかを判定する
        if (distance < m_goalDistance)
        {
            // ゴールしたと判定されたら、フラグを立てて Timeline を再生する
            m_isFinished = true;
            Debug.Log("Goal");

            if (m_playable)
            {
                m_playable.gameObject.SetActive(true);
                Debug.Log("Play");
                m_playable.Play();
            }
        }
    }
}
