using Godot;
using System.Collections.Generic;

public partial class Common_Edit
{
    public static Dictionary<string, Texture2D> type_icons = new Dictionary<string, Texture2D>()
                {
        {"KeyValue", GD.Load<Texture2D>("res://img/ico/KeyValue.svg")},
        {"KeyTrackPosition", GD.Load<Texture2D>("res://img/ico/KeyTrackPosition.svg")},
        {"KeyTrackRotation", GD.Load<Texture2D>("res://img/ico/KeyTrackRotation.svg")},
        {"KeyTrackScale", GD.Load<Texture2D>("res://img/ico/KeyTrackScale.svg")},
        {"KeyTrackBlendShape", GD.Load<Texture2D>("res://img/ico/KeyTrackBlendShape.svg")},
        {"KeyCall", GD.Load<Texture2D>("res://img/ico/KeyCall.svg")},
        {"KeyBezier", GD.Load<Texture2D>("res://img/ico/KeyBezier.svg")},
        {"KeyAudio", GD.Load<Texture2D>("res://img/ico/KeyAudio.svg")},
        {"KeyAnimation", GD.Load<Texture2D>("res://img/ico/KeyAnimation.svg")}
                };

    /// <summary>
    /// 通过ico名称获取对应的 纹理
    /// </summary>
    /// <param name="ico_name"></param>
    /// <returns></returns>
    public static Texture2D get_editor_theme_icon(string ico_name)
    {
        return type_icons[ico_name];
    }


}