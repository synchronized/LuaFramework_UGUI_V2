using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using System.Reflection;
using System.IO;


namespace LuaFramework {
    public class GameManager {

        private static GameManager m_Instance;

        public static GameManager Instance {
            get {
                if (m_Instance == null) {
                    m_Instance = new GameManager();
                }
                return m_Instance;
            }
        }

        private static Dictionary<Type, IManager> m_Managers = new Dictionary<Type, IManager>();

        private GameManager() {
            this.AddManager<LuaManager>();
            this.AddManager<NetworkManager>();
            this.AddManager<ResourceManager>();
            this.AddManager<SoundManager>();
            this.AddManager<ThreadManager>();
        }

        /// <summary>
        /// 添加管理器
        /// </summary>
        public void AddManager(IManager mgr) {
            if (!m_Managers.ContainsKey(typeof(mgr))) {
                m_Managers.Add(typeof(mgr), mgr);
            }
        }

        /// <summary>
        /// 添加Unity对象
        /// </summary>
        public T AddManager<T>() where T : IManager, new() {
            IManager result = null;
            var tp = typeof(T);
            m_Managers.TryGetValue(tp, out result);
            if (result != null) {
                return (T)result;
            }
            result = new T();
            m_Managers.Add(tp, result);
            return result;
        }

        /// <summary>
        /// 获取系统管理器
        /// </summary>
        public T GetManager<T>() where T : IManager {
            var tp = typeof(T);
            if (!m_Managers.ContainsKey(tp)) {
                return default(T);
            }
            object manager = null;
            m_Managers.TryGetValue(tp, out manager);
            return (T)manager;
        }

        /// <summary>
        /// 删除管理器
        /// </summary>
        public void RemoveManager<T>() where T : IManager {
            var tp = typeof(T);
            m_Managers.Remove(tp);
        }

        void OnGameInit() {
            foreach (var manager in m_Managers.Values) {
                manager.OnGameInit();
            }
            Debug.Log("~GameManager was init");
        }

        void OnGameStart() {
            foreach (var manager in m_Managers.Values) {
                manager.OnGameStart();
            }
            Debug.Log("~GameManager was start");
        }

        void OnGameUpdate() {
            foreach (var manager in m_Managers.Values) {
                manager.OnGameUpdate();
            }
        }


        /// <summary>
        /// 析构函数
        /// </summary>
        void OnGameDestroy() {
            foreach (var manager in m_Managers.Values) {
                manager.OnGameDestroy();
            }
            Debug.Log("~GameManager was destroyed");
        }
    }
}