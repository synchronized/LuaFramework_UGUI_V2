using UnityEngine;
using System.Collections;

namespace LuaFramework {

    /// <summary>
    /// </summary>
    public class Main : MonoBehaviour {

        GameManager m_gameMgr;

        private void Awake() {
            DontDestroyOnLoad(gameObject);  //防止销毁自己
            m_gameMgr = GameManager.Instance;
            m_gameMgr.AddManager<StartupManager>();
            m_gameMgr.OnGameInit();
        }

        private void Start() {
            m_gameMgr.OnGameStart();
        }

        private void Update() {
            m_gameMgr.OnGameUpdate();
        }

        private void OnDestroy() {
            m_gameMgr.OnGameDestroy();
        }
    }
}