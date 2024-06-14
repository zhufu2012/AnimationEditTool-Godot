using GameLog;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
namespace AnimationEditTool_Core
{
    /// <summary>
    /// 轨道树，展示为一张图片的多种属性轨道
    /// </summary>
    public partial class TrackTree : Tree
    {
        //节点类型
        public enum ItemType
        {
            Root,//主节点,图片
            Treak,//轨道
            Key//关键帧
        }

        //唯一id
        public long data_id;
        //节点菜单
        private PopupMenu root_menu;
        //轨道菜单
        private PopupMenu track_menu;
        //关键帧菜单
        private PopupMenu key_menu;

        //动画树数据
        public AnimSingle anim;

        //保存间隔
        int SaveTime = 0;



        private List<Texture2D> ico_list = new List<Texture2D>();

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void InitMenu()
        {
            track_menu = new PopupMenu();
            track_menu.AddItem("增加关键帧", 0);
            track_menu.AddItem("修改轨道属性", 1);
            track_menu.AddItem("修改轨道类型", 2);
            track_menu.AddItem("删除该轨道", 3);
            track_menu.Hide();//隐藏
            track_menu.Connect(PopupMenu.SignalName.IdPressed, new Callable(this, MethodName.OnTrackMenu));//选项
            AddChild(track_menu);
            root_menu = new PopupMenu();
            root_menu.AddItem("增加属性轨道", 0);
            root_menu.AddItem("增加方法轨道", 0);
            root_menu.AddItem("增加贝塞斯曲线轨道", 0);
            root_menu.Hide();//隐藏
            root_menu.Connect(PopupMenu.SignalName.IdPressed, new Callable(this, MethodName.OnRootMenu));//选项
            AddChild(root_menu);
            key_menu = new PopupMenu();
            key_menu.AddItem("设置为当前图片属性", 0);
            key_menu.AddItem("删除该关键帧", 1);
            key_menu.Hide();//隐藏
            key_menu.Connect(PopupMenu.SignalName.IdPressed, new Callable(this, MethodName.OnKeyMenu));//选项
            AddChild(key_menu);
        }


        public TrackTree()
        {
            anim = new AnimSingle();
            data_id = anim.data_id;
            Columns = 4;
            AllowSearch = true;
            AllowRmbSelect = true;
            EnableRecursiveFolding = true;
            DropModeFlags = (int)(DropModeFlagsEnum.OnItem | DropModeFlagsEnum.Inbetween);
            SelectMode = SelectModeEnum.Single;
            ScrollVerticalEnabled = true;
            CustomMinimumSize = new Vector2(230, 365);

            Connect(Tree.SignalName.ItemMouseSelected, new Callable(this, MethodName.MouseItemSelected));
            ItemActivated += TreeItemActivated;//双击某一项,设置可编辑
            ItemEdited += TreeItemEdited;//编辑后修改，标签名称

            InitMenu();
            IniView(false);
        }

        public override void _PhysicsProcess(double delta)
        {
            SaveTime += 1;
            if (SaveTime > 120)
            {
                SaveTime = 0;
                //SaveData();
            }
            base._PhysicsProcess(delta);
        }

        /// <summary>
        /// 双击编辑功能
        /// </summary>
        public void TreeItemActivated()
        {
            TreeItem treeitem = GetSelected();//当前选中
            if (treeitem != null && GetItemType(treeitem) == ItemType.Key)//关键帧可以直接修改
            {
                EditSelected();
            }
        }

        /// <summary>
        /// 编辑后处理，比如更新整颗树的节点
        /// </summary>
        public void TreeItemEdited()
        {
            TreeItem treeitem = GetEdited();
            if (treeitem != null)
            {

            }
        }



        /// <summary>
        /// 初始化界面
        /// </summary>
        public void IniView(bool IsRefresh)
        {
            if (IsRefresh)
            {
                // 清空子节点
                //DeselectAll();
                //Clear();

            }
            InitTree();
        }

        /// <summary>
        /// 初始化树图形
        /// </summary>
        public void InitTree()
        {
            TreeItem root = CreateItem();
            root.SetText(0, anim.png_name);//图片名称
            root.SetText(1, "           ");
            root.SetText(2, "           ");
            SetItemType(root, ItemType.Root);//设置为主节点
            root.SetIconMaxWidth(0, 16);
            root.AddButton(0, Main.GetImg("没启用动画"));
            for (int i = 0; i < anim.AnimTrackList.Count; i++)//轨道标签数据
            {
                AddTrackItem(anim.AnimTrackList[i], root);
            }
            /*
            TreeItem root = CreateItem();
            root.SetText(0, "动画名称");//主节点  动画名称
            root.SetText(1, "           ");//主节点  动画名称
            root.SetText(2, "           ");//主节点  动画名称
            //root.SetMetadata(3, "root");
            SetItemType(root, ItemType.Root);//设置为主节点
            root.SetIconMaxWidth(0, 16);
            //root.add_button(0, DIR)   # 为根节点0列的单元格创建按钮
            root.AddButton(0, Main.GetImg("没启用动画"));
            TreeItem ValueTrack = CreateItem(root);
            ValueTrack.SetText(0, "Position:X");//主节点
            ValueTrack.SetMetadata(3, "1");
            ValueTrack.SetIcon(0, Main.GetImg("属性轨道图标"));

            TreeItem TrackKey = CreateItem(ValueTrack);//关键帧
            TrackKey.SetText(0, "0.6");//时间
            TrackKey.SetEditable(0, true);
            TrackKey.SetText(1, "4");//值 
            TrackKey.SetEditable(1, true);
            TrackKey.SetText(2, "0.4");//过渡曲线 
            TrackKey.SetEditable(2, true);
            TrackKey.SetMetadata(3, "2");

            TreeItem TrackKey2 = CreateItem(ValueTrack);//关键帧
            TrackKey2.SetText(0, "0.6");//时间
            TrackKey2.SetEditable(0, true);
            TrackKey2.SetText(1, "4");//值 
            TrackKey2.SetEditable(1, true);
            TrackKey2.SetText(2, "0.4");//过渡曲线 
            TrackKey2.SetEditable(2, true);
            TrackKey2.SetMetadata(3, "2");

            TreeItem TrackKey3 = CreateItem(ValueTrack);//关键帧
            TrackKey3.SetText(0, "0.6");//时间
            TrackKey3.SetEditable(0, true);
            TrackKey3.SetText(1, "4");//值 
            TrackKey3.SetEditable(1, true);
            TrackKey3.SetText(2, "0.4");//过渡曲线 
            TrackKey3.SetEditable(2, true);
            TrackKey3.SetMetadata(3, "2");*/

            //TrackKey.SetIcon(0, ico_list[2]);
        }

        /// <summary>
        /// 根据数据添加轨道
        /// 轨道数据，父节点
        /// </summary>
        public void AddTrackItem(AnimTrack track_data, TreeItem parent_item)
        {
            TreeItem ValueTrack = CreateItem(parent_item);
            ValueTrack.SetText(0, track_data.title);//主节点
            ValueTrack.SetMetadata(3, "1");
            ValueTrack.SetIcon(0, Main.GetImg("属性轨道图标"));
            for (int i = 0; i < track_data.KeyList.Count; i++)
            {
                TrackKeyPoint trackKeyPoint = track_data.KeyList[i];
                TreeItem TrackKey = CreateItem(ValueTrack);//关键帧
                TrackKey.SetText(0, "" + trackKeyPoint.time);//时间
                TrackKey.SetEditable(0, true);
                TrackKey.SetText(1, "" + trackKeyPoint.Value);//值 
                TrackKey.SetEditable(1, true);
                TrackKey.SetText(2, "" + trackKeyPoint.Transition);//过渡曲线 
                TrackKey.SetEditable(2, true);
                TrackKey.SetMetadata(3, "2");
            }
        }

        /// <summary>
        /// 更新数据，将动画数据中的AnimSingle这一级的数据更新
        /// </summary>
        public void UpdataData()
        {
            TreeItem root = GetRoot();
            Array<TreeItem> array = root.GetChildren();
            //Log.Print(root.GetText(0));
            for (int i = 0; i < array.Count; i++)
            {
                TreeItem treeItem = array[i];
                //Log.Print("----" + treeItem.GetText(0));
                for (int j = 0; j < treeItem.GetChildCount(); j++)
                {
                    TreeItem treeItem2 = treeItem.GetChild(j);
                    //Log.Print("--------" + treeItem2.GetText(0));
                }
            }
        }


        /// <summary>
        ///鼠标选中某选项
        /// </summary>
        public void MouseItemSelected(Vector2 position, int mouse_button_index)
        {

            if (mouse_button_index == 2)//鼠标左键
            {

                TreeItem treeitem = GetSelected();//当前选中
                if (treeitem != null)
                {
                    if (GetItemType(treeitem) == ItemType.Root)
                    {
                        SetShow(root_menu, true, position);//显示菜单
                    }
                    else if (GetItemType(treeitem) == ItemType.Treak)
                    {
                        SetShow(track_menu, true, position);//显示菜单
                    }
                    else
                    {
                        SetShow(key_menu, true, position);//显示菜单
                    }
                }
            }
        }

        /// <summary>
        ///设置选项菜单是否显示
        /// </summary>
        public void SetShow(PopupMenu menu, bool IsShow, Vector2 position)
        {
            if (IsShow)
            {
                menu.Position = (Vector2I)GetGlobalMousePosition();
                menu.Show();
            }
            else
                menu.Hide();
        }

        //主节点选项菜单

        public void OnRootMenu(int id)
        {

        }


        /// <summary>
        ///轨道选项菜单按钮
        /// </summary>
        public void OnTrackMenu(int id)
        {
            TreeItem treeitem = GetSelected();
            if (treeitem != null)//有选中
            {
                if (id == 0)//增加关键帧
                {
                    Log.Print(0);
                }
                else if (id == 1)//修改轨道类型
                {
                    Log.Print(1);
                }
                else if (id == 2)//修改轨道属性
                {
                    Log.Print(2);
                }
                else
                {
                    Log.Print(3);
                }
            }
        }

        /// <summary>
        /// 关键帧菜单按钮
        /// </summary>
        /// <param name="id"></param>
        public void OnKeyMenu(int id)
        {

        }

        /// <summary>
        /// 增加关键帧
        /// </summary>
        /// <param name="treeitem"></param>
        public void AddKeyTrack(TreeItem treeitem)
        {

        }


        public static void SetItemType(TreeItem root, ItemType type)
        {
            root.SetMetadata(3, type + "");
        }

        public static ItemType GetItemType(TreeItem root)
        {
            return (ItemType)(int)root.GetMetadata(3);
        }
    }
}