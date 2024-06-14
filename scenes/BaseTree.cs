using Godot;
namespace AnimationEditTool_Core
{
    /// <summary>
    /// 基础树
    /// </summary>
    public partial class BaseTree : Tree
    {
        private Texture2D PngPng;
        private Texture2D CombinationPng;
        TreeItem Root;//动画组合
        TreeItem CombinationItem;//动画组合
        TreeItem PngItem;//png部分


        public override void _Ready()
        {
            PngPng = GD.Load<Texture2D>("res://img/ico/PNG.png");
            CombinationPng = GD.Load<Texture2D>("res://img/ico/动画组合.png");
            Root = CreateItem();

            CombinationItem = CreateItem(Root);
            PngItem = CreateItem(Root);

            CombinationItem.SetText(0, "动画组合列表");//主节点
            CombinationItem.SetIcon(0, PngPng);
            CombinationItem.SetIconMaxWidth(0, 16);

            PngItem.SetText(0, "图片列表");//主节点
            PngItem.SetIcon(0, CombinationPng);
            PngItem.SetIconMaxWidth(0, 16);

        }

        public override void _Process(double delta)
        {
        }
    }
}
