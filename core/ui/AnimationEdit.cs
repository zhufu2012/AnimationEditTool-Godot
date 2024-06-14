using GameLog;
using Godot;
using System.Collections.Generic;

namespace AnimationEditTool_Core
{
    public partial class AnimationEdit : Control
    {
        /// <summary>
        /// AnimComponent 动画组件
        /// </summary>
        public long data_id;

        public AnimCombination animCombination;

        public ScrollContainer ScrollTrack;

        public ScrollContainer ScrollButton;

        public Button AddTrackbutton;
        public Button DelTrackbutton;
        private HBoxContainer EditPanel;//轨道树画布


        /// <summary>
        /// 设置树数据，相当于选中一个动画组件，显示这个动画组件的数据
        /// </summary>
        /// <param name="id"></param>
        public void InitData(AnimCombination animCombination)
        {
            this.animCombination = animCombination;
            this.data_id = animCombination.data_id;
        }


        public override void _Ready()
        {
            ScrollTrack = GetNode<ScrollContainer>("Panel/ScrollTrack");
            ScrollButton = GetNode<ScrollContainer>("Panel/ScrollButton");
            ScrollTrack.FocusEntered += SetFocus;

            EditPanel = GetNode<HBoxContainer>("Panel/ScrollTrack/HBoxContainer");
            //按钮
            AddTrackbutton = GetNode<Button>("Panel/ScrollButton/VBoxContainer/AddTrackButton");
            DelTrackbutton = GetNode<Button>("Panel/ScrollButton/VBoxContainer/DelTrackButton");
            AddTrackbutton.ButtonDown += AddTrackButtonDown;
            DelTrackbutton.ButtonDown += DelTrackButtonDown;
        }


        public override void _Process(double delta)
        {
            QueueRedraw();
        }


        public void AddTrackTree(TrackTree track_tree)
        {
            EditPanel.AddChild(track_tree);
        }

        public void SetFocus()
        {

        }

        /// <summary>
        /// 按钮按下-新增动画树
        /// </summary>
        public void AddTrackButtonDown()
        {
            TrackTree track_tree = new TrackTree();
            AddTrackTree(track_tree);
        }

        /// <summary>
        /// 删除动画树
        /// </summary>
        public void DelTrackButtonDown()
        {
            Log.Print(ScrollTrack.ScrollHorizontal);
        }
    }
}