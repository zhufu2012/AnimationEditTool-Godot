namespace AnimationEditTool_Core
{
    public static class IdConstant
    {
        /// <summary>
        /// 保存唯一id数据，间隔时间s
        /// </summary>
        public static float SaveIdGeneration = 1;
		
		
		
        /// <summary>
        /// 动画组合 AnimCombination 类型
        /// </summary>
        public static int ID_TYPE_AnimCombination = 2;
        /// <summary>
        /// 动画组件 AnimComponent 类型
        /// </summary>
        public static int ID_TYPE_AnimComponent = 3;
        /// <summary>
        /// 动画 AnimSingle 类型
        /// </summary>
        public static int ID_TYPE_AnimSingle = 4;
        /// <summary>
        /// 动画轨道 AnimTrack 类型
        /// </summary>
        public static int ID_TYPE_AnimTrack = 5;
        /// <summary>
        /// 关键帧 TrackKeyPoint 类型
        /// </summary>
        public static int ID_TYPE_TrackKeyPoint = 6;
        //最大的需要保存的id类型,超过这个id类型的，不会保存
        public static int ID_TYPE_DATABASE_MAX = 40;


    }
}