using Godot;

namespace AnimationEditTool_Core
{
    public partial class TrackEdit : Control
    {
        //是否显示
        public CheckBox IsUser;

        //轨道类型
        public Sprite2D track_type;

        //属性名称和路径
        public Label attr_name;

        /// <summary>
        /// 属性轨道
        /// </summary>
        public Control Track;

        public override void _Ready()
        {
            IsUser = GetNode<CheckBox>("IsUser");
            track_type = GetNode<Sprite2D>("track_type");
            attr_name = GetNode<Label>("attr_name");
            //Track = GetNode<Control>("Track");




        }


        public override void _Process(double delta)
        {
        }
    }
}