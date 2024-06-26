using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LuaFramework {
    public class TimerInfo {
        public long tick;
        public bool stop;
        public bool delete;
        public Object target;
        public string className;

        public TimerInfo(string className, Object target) {
            this.className = className;
            this.target = target;
            delete = false;
        }
    }


    public class TimerManager : Manager {
        private float interval = 0;
        private List<TimerInfo> objects = new List<TimerInfo>();

        public float Interval {
            get { return interval; }
            set { interval = value; }
        }

        // Use this for initialization
        void Start() {
            StartTimer(AppConst.TimerInterval);
        }

        /// <summary>
        /// 开始一个定时器
        /// </summary>
        /// <param name="interval"></param>
        public void StartTimer(float value) {
            interval = value;
            InvokeRepeating("Run", 0, interval);
        }

        /// <summary>
        /// 结束定时器
        /// </summary>
        public void StopTimer() {
            CancelInvoke("Run");
        }

        /// <summary>
        /// 添加定时器事件
        /// </summary>
        /// <param name="info"></param>
        public void AddTimerEvent(TimerInfo info) {
            if (!objects.Contains(info)) {
                objects.Add(info);
            }
        }

        /// <summary>
        /// 移除定时器事件
        /// </summary>
        /// <param name="name"></param>
        public void RemoveTimerEvent(TimerInfo info) {
            if (objects.Contains(info) && info != null) {
                info.delete = true;
            }
        }

        /// <summary>
        /// 停止定时器事件
        /// </summary>
        /// <param name="info"></param>
        public void StopTimerEvent(TimerInfo info) {
            if (objects.Contains(info) && info != null) {
                info.stop = true;
            }
        }

        /// <summary>
        /// 回复定时器事件
        /// </summary>
        /// <param name="info"></param>
        public void ResumeTimerEvent(TimerInfo info) {
            if (objects.Contains(info) && info != null) {
                info.delete = false;
            }
        }

        /// <summary>
        /// 启动定时器
        /// </summary>
        void Run() {
            if (objects.Count == 0) return;
            for (int i = 0; i < objects.Count; i++) {
                TimerInfo o = objects[i];
                if (o.delete || o.stop) { continue; }
                ITimerBehaviour timer = o.target as ITimerBehaviour;
                timer.TimerUpdate();
                o.tick++;
            }
            /////////////////////////移除定需要删除的时器///////////////////////////
            for (int i = objects.Count - 1; i >= 0; i--) {
                if (objects[i].delete) { objects.Remove(objects[i]); }
            }
        }
    }
}