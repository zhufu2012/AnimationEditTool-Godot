using GameLog;
using Godot;

namespace AnimationEditTool_Core
{
    public class ValueTrack : KeyTrackType
    {

        /// <summary>
        /// 导出属性轨道数据
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
                //Log.Print(animation.AudioTrackGetKeyEndOffset(k, p));//动画名称
                //Log.Print(animation.AudioTrackGetKeyStartOffset(k, p));//动画名称
                //Log.Print(animation.AudioTrackGetKeyStream(k, p));//
            }
        }

    }
}
