using AnimationEditTool_Core;
using GameLog;
using Godot;
using Godot.Collections;
using System;

public partial class AnimationTest : Node2D
{
    AnimationPlayer animationplayer { get; set; }

    public override void _Ready()
    {
        animationplayer = GetNode<AnimationPlayer>("AnimationPlayer");
        Array<StringName> library_array = animationplayer.GetAnimationLibraryList();
        for (int i = 0; i < library_array.Count; i++)
        {
            AnimationLibrary library = animationplayer.GetAnimationLibrary(library_array[i]);
            Array<StringName> animation_array = library.GetAnimationList();
            for (int j = 0; j < animation_array.Count; j++)
            {
                Animation animation = library.GetAnimation(animation_array[j]);
                for (int k = 0; k < animation.GetTrackCount(); k++)//轨道不是全部
                {

                    KeyTrackType trackType;
                    switch (animation.TrackGetType(k))
                    {
                        case Animation.TrackType.Value:
                            trackType = new ValueTrack();
                            trackType.ExportTrack(library, animation, k);
                            break;
                        case Animation.TrackType.Method:
                            trackType = new MethodTrack();
                            trackType.ExportTrack(library, animation, k);
                            break;
                        case Animation.TrackType.Bezier:
                            trackType = new BezierTrack();
                            trackType.ExportTrack(library, animation, k);
                            break;
                        case Animation.TrackType.Audio:
                            trackType = new AudioTrack();
                            trackType.ExportTrack(library, animation, k);
                            break;
                        default:
                            continue;
                    }
                    //animation.GetPropertyList()
                }


            }

        }

        GD.Print(animationplayer.ToString());
    }

    public static void key()
    {
        GD.Print("函数运行！！！");
    }


    public override void _Process(double delta)
    {
    }
}
