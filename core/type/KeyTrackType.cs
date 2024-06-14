using Godot;

namespace AnimationEditTool_Core
{
    public class KeyTrackType
    {
       /* //轨道类型
        enum Type : long
        {
            //
            // 摘要:
            //     值轨迹在节点属性中设置值，但仅限于可以插值的值。
            //对于3D位置/旋转/缩放，使用专用的Godot。Animation.TrackType.Position3D，
            //戈多。动画。轨道类型。旋转3D和戈多。动画。轨迹类型。缩放3D轨迹
            //类型而不是Godot。为了提高性能，建议使用Animation.TrackType.Value
            //原因。
            Value,//属性轨道
            //
            // 摘要:
            //    3D位置轨迹（值存储在Godot中。向量3s）。
            Position3D,///不使用
            //
            // 摘要:
            //    3D旋转轨迹（值存储在Godot中。四元数）。.
            Rotation3D,///不使用
            //
            // 摘要:
            //     3D缩放轨迹(值存储在Godot中。Vector3s)。
            Scale3D,///不使用
            //
            // 摘要:
            //     混合形状轨迹。
            BlendShape,///不使用
            //
            // 摘要:
            //     方法跟踪每个键具有给定参数的调用函数。
            Method,
            //
            // 摘要:
            //     贝塞尔曲线用于使用自定义曲线插值。他们也可以
            //用于制作矢量和颜色的子属性动画（例如
            //一个戈多。颜色）。
            Bezier,
            //
            // 摘要:
            //     音轨用于通过任何一种类型的Godot.AudioStreamPlayer播放音频流。
            //可以在动画中修剪和预览流。
            Audio,
            //
            // 摘要:
            //     动画轨道在其他Godot中播放动画。动画播放器节点。
            Animation///不使用
        }
       */
        public virtual void ExportTrack(AnimationLibrary library, Animation animation,int index) { }













    }
}
