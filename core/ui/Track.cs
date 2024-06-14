using Godot;
using static Godot.Animation;

namespace AnimationEditTool_Core
{
    public partial class Track : Control
    {
        public override void _Ready()
        {
            base._Ready();
        }


        public override void _Notification(int what)
        {
            switch (what)
            {
                case (int)NotificationThemeChanged://样式改变
                    break;
                case (int)NotificationDraw://请求CanvasItem进行绘制
                    break;
                case (int)NotificationMouseEnter://鼠标进入了该控件的区域。
                    break;
                case (int)NotificationMouseExit://鼠标退出了该控件的区域。
                    break;
                case (int)NotificationDragEnd://拖拽操作介绍时，检查是否拖动成功
                    break;
            }
        }

        public override bool _CanDropData(Vector2 atPosition, Variant data)
        {
            return base._CanDropData(atPosition, data);
        }

        public override void _DropData(Vector2 atPosition, Variant data)
        {
            base._DropData(atPosition, data);
        }




    }
}
