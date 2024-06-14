using GameLog;
using Godot;

namespace AnimationEditTool_Core
{
    public class BezierTrack : KeyTrackType
    {

        /// <summary>
        /// 导出贝塞尔曲线轨道数据
        /// </summary>
        public override void ExportTrack(AnimationLibrary library, Animation animation, int index)
        {
            NodePath path = animation.TrackGetPath(index);
            animation.TrackGetType(index);
            Log.Print(path);
            //Log.PrintArray(animation.GetPropertyList());//
            //Log.PrintArray(animation.GetSignalList());//信号


            for (int p = 0; p < animation.TrackGetKeyCount(index); p++)//属性轨道
            {
                Log.Print(animation.TrackGetKeyTime(index, p));//关键帧时间
                Log.Print(animation.TrackGetKeyTransition(index, p));//关键帧 过渡曲线
                Log.Print(animation.TrackGetKeyValue(index, p));//关键帧值
            }
        }
    }
}
