using UnityEngine;
using Game.Base;

namespace Game.Battle
{
    public sealed class TimelineManager : Singleton<TimelineManager>
    {
        // 事件
        public delegate void EventStepHandler();
        public event EventStepHandler EventStep = null;

        private bool isRunning = false;
        public bool IsRunning
        {
            get { return isRunning; }
        }

        // 目标帧率
        private float targetFrameRate = 30;
        public float TargetFrameRate
        {
            get { return this.targetFrameRate; }
        }
        // 帧率系数
        private float frameRateFactor = 0;
        public float FrameRateFactor
        {
            get { return this.frameRateFactor; }
        }
        // 距离上一帧的时间
        private float framePassTime = 0;
        public float FramePassTime
        {
            get { return this.framePassTime; }
        }
        // 单帧时间
        private float oneFrameTime = 0;
        public float OneFrameTime
        {
            get { return this.oneFrameTime; }
        }

        // 空帧
       // private BattleFrameInput emptyFrameInput = null;

        public TimelineManager()
        {
        }

        public bool Init()
        {
         //   this.emptyFrameInput = new BattleFrameInput();

            return true;
        }

        public void Dispose()
        {
         //   if (this.emptyFrameInput != null) {
         //       this.emptyFrameInput = null;
         //   }
            if (this.EventStep != null) {
                this.EventStep = null;
            }
        }

        public void Start()
        {
            this.isRunning = true;

            this.frameRateFactor = 1.0f;
            this.framePassTime = 0;
            UpdateOneFrameTime();

        //    BattleLogicModel.Instance.LockStepScene.Start();
        }

        public void Suspend()
        {
            if (this.isRunning == false) {
                return;
            }

            this.isRunning = false;
        }

        public void Resume()
        {
            if (this.isRunning) {
                return;
            }

            this.isRunning = true;
        }

        public void Update()
        {
            if (this.isRunning == false) {
                return;
            }

            this.framePassTime += Time.deltaTime;

            // 游戏是否已结束
            bool isGameEnd = false;
          //      BattleLogicModel.Instance.LockStepScene.CheckGameEnd();

            // 设置帧率系数
            if (isGameEnd == false) {
                int pendingInputCount = 0;
                 //   InputManager.Instance.InputSource.GetPendingInputCount();
                if (pendingInputCount <= 2) {
                    this.frameRateFactor = 1f;
                } else if (pendingInputCount <= 4) {
                    this.frameRateFactor = 2f;
                } else if (pendingInputCount <= 8) {
                    this.frameRateFactor = 4f;
                } else if (pendingInputCount <= 16) {
                    this.frameRateFactor = 8f;
                } else if (pendingInputCount <= 32) {
                    this.frameRateFactor = 16f;
                } else {
                    this.frameRateFactor = 32f;
                }
            } else {
                this.frameRateFactor = 1f;
            }
            UpdateOneFrameTime();

            for (; ; ) {
                if (this.framePassTime < this.oneFrameTime) {
                    return;
                }
                this.framePassTime -= this.oneFrameTime;

                // 更新时间轴速度
                //TimelineSpeedManager.Instance.UpdateSpeedTime(this.oneFrameTime);
                //if (TimelineSpeedManager.Instance.Block) {
                //    continue;
                //}
                // 触发Step事件
                if (this.EventStep != null) {
                    this.EventStep();
                }

             //   BattleFrameInput frameInput = null;
                if (isGameEnd) {
                    // 游戏结束后使用空帧作为输入
              //      frameInput = this.emptyFrameInput;
                } else {
                    // 获取输入帧
                //    frameInput = InputManager.Instance.InputSource.GetNextInput();
                //    if (frameInput == null) {
                //        continue;
               //     }
                    // 记录录像
               //     BattleModel.Instance.BattleReplay.Record(frameInput);
                }

            //    BattleLogicModel.Instance.LockStepScene.Simulate(frameInput);
            }
        }

        private void UpdateOneFrameTime()
        {
            this.oneFrameTime = 1f /
                (this.targetFrameRate * this.frameRateFactor);
        }
    }
}