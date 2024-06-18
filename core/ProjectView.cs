using GameLog;
using Godot;
using System;
using System.IO;

public partial class ProjectView : Control
{
    FileDialog FileFrame;
    public void DirSelect(string paths)
    {
        GD.Print(paths);
    }


    #region 项目管理界面 ProjectManagePanel
    Panel ProjectManagePanel;
    ItemList ProjectItemList;
    Button OpenRecentlyBut;
    Button CreatProjectBut;
    //选择双击功能
    OptionButton SelectType;

    /// <summary>
    /// 场景界面初始化
    /// </summary>
    public void InitManagePanel()
    {
        ProjectManagePanel = GetNode<Panel>("ProjectManagePanel");
        ProjectItemList = GetNode<ItemList>("ProjectManagePanel/ProjectItemList");
        OpenRecentlyBut = GetNode<Button>("ProjectManagePanel/OpenRecentlyBut");
        CreatProjectBut = GetNode<Button>("ProjectManagePanel/CreatProjectBut");
        SelectType = GetNode<OptionButton>("ProjectManagePanel/SelectType");
        OpenRecentlyBut.ButtonDown += OpenRecentlyButDown;
        CreatProjectBut.ButtonDown += CreatProjectButDown;
    }

    /// <summary>
    /// 点击切换到项目创建界面
    /// </summary>
    public void CreatProjectButDown()
    {
        ProjectManagePanel.Visible = false;
        ProjectCreatePanel.Visible = true;
    }

    /// <summary>
    /// 点击开启动画项目
    /// </summary>
    public void OpenRecentlyButDown()
    {

    }

    #endregion 

    #region 项目创建界面 ProjectCreatePanel
    /// <summary>
    /// 项目创建界面
    /// </summary>
    Panel ProjectCreatePanel;

    Button CreateBut;
    Button ReturnBut;

    LineEdit LineEditName;
    LineEdit LineFilePath;
    LineEdit LineSavePath;

    public void InitCreatePanel()
    {
        ProjectCreatePanel = GetNode<Panel>("ProjectCreatePanel");
        LineEditName = GetNode<LineEdit>("ProjectCreatePanel/LabelName/LineEditName");
        LineFilePath = GetNode<LineEdit>("ProjectCreatePanel/LabelPath/LineEditPath");
        LineSavePath = GetNode<LineEdit>("ProjectCreatePanel/LabelSavePath/LineEditSavePath");
        CreateBut = GetNode<Button>("ProjectCreatePanel/CreatButton");
        ReturnBut = GetNode<Button>("ProjectCreatePanel/ReturnButton");
        CreateBut.ButtonDown += CreatProject;
        ReturnBut.ButtonDown += ReturnProjectManagePanel;
    }

    /// <summary>
    /// 返回管理器界面
    /// </summary>
    public void ReturnProjectManagePanel()
    {
        ProjectManagePanel.Visible = true;
        ProjectCreatePanel.Visible = false;
    }

    /// <summary>
    /// 根据当前填写的参数 创建项目
    /// </summary>
    public void CreatProject()
    {

    }
    #endregion


    public override void _Ready()
    {
        InitManagePanel();
        InitCreatePanel();

        FileFrame = GetNode<FileDialog>("FileFrame");
        FileFrame.DirSelected += DirSelect;

    }







}
