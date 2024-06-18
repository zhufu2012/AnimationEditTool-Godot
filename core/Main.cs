using GameLog;
using Godot;
using System.Collections.Generic;

namespace AnimationEditTool_Core
{
    public partial class Main : Node
    {
        //测试组件数量
        //[Export] int TrackTreeNum = 0;

        private BaseTree tree;//组件树
        private AnimationEdit AnimationEdit;//动画编辑器部分

        //图片动画树列表
        public List<TrackTree> TrackTreeList = new List<TrackTree>();

        //图片数据字典
        public static Dictionary<string, Texture2D> ico_dict = new Dictionary<string, Texture2D>();
		
		//////////////////////////动画播放数据//////////////////////////////////////
		//动画播放器-通过这个播放器，将动画展示出来
		AnimationPlayer AnimaPlayer;
		//所选择的动画组合中，所有的图片列表
		List<Sprite2D> SpriteList;
		
		//////////////////////////动画播放数据//////////////////////////////////////
		
        public Main() {
            InitIco();
        }

        /// <summary>
        /// 初始化图标
        /// </summary>
        public void InitIco()
        {
            ico_dict["贝塞尔曲线轨道图标"] = GD.Load<Texture2D>("res://img/ico/KeyBezier.svg");//贝塞尔曲线轨道
            ico_dict["方法轨道图标"] = GD.Load<Texture2D>("res://img/ico/KeyCall.svg");//方法轨道
            ico_dict["属性轨道图标"] = GD.Load<Texture2D>("res://img/ico/KeyValue.svg");//属性轨道
            ico_dict["没启用动画"] = GD.Load<Texture2D>("res://img/ico/没启用动画.svg");//没启用动画
            ico_dict["启用动画"] = GD.Load<Texture2D>("res://img/ico/启用动画.svg");//启用动画
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            tree = GetNode<BaseTree>("CanvasLayer/BaseTree");
            AnimationEdit = GetNode<AnimationEdit>("CanvasLayer/AnimationEdit");
            AnimationEdit.Position = new Vector2(0, 400);
			
			//按照当前选择的动画数据 组织动画播放器节点
			AnimaPlayer = GetNode<AnimationPlayer>("AnimaPlayer");
        }


        public override void _Ready()
        {
            InitData();//初始化数据


        }

        public override void _Process(double delta)
        {
            for (int i = 0; i < TrackTreeList.Count; i++)
            {
                TrackTreeList[i].UpdataData();//更新轨道数据
            }
			
        }



        public static Texture2D GetImg(string name)
        {
            if (ico_dict.ContainsKey(name))
                return ico_dict[name];
            return GD.Load<Texture2D>("res://img/ico/PNG.png");//默认图片
        }
    }
}
