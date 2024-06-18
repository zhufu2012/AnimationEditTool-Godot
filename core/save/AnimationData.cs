
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AnimationEditTool_Core
{
    /// <summary>
    /// 轨道关键点，关键帧数据
    /// </summary>
    public class TrackKeyPoint
    {
        [JsonInclude]
        public long data_id;//data_id
        //时间
        [JsonInclude]
        public double time;
        //值
        [JsonInclude]
        public float Value;
        //过渡曲线
        [JsonInclude]
        public float Transition;
        public TrackKeyPoint()
        {
            data_id = IdGenerator.Generate(IdConstant.ID_TYPE_TrackKeyPoint);
        }
    }


    /// <summary>
    /// 动画轨道，每个动画轨道都有多个关键帧，次节点是各个轨道，次节点下面是多个帧
    /// </summary>
    public class AnimTrack
    {
        [JsonInclude]
        public long data_id;//data_id
		
		public string title;//显示标题
		
		
		
        //关键帧列表
        [JsonInclude]
        public List<TrackKeyPoint> KeyList = new List<TrackKeyPoint>();
        public AnimTrack()
        {
            data_id = IdGenerator.Generate(IdConstant.ID_TYPE_AnimTrack);
        }
    }


    /// <summary>
    /// 动画，每个动画都是一张图片，  下面多个轨道节点，本身是一个树，主节点是动画名称等，次节点是各种类型的轨道，次节点下面是多个帧---------关键---------------------
    /// </summary>
    public class AnimSingle
    {
        [JsonInclude]
        public long data_id;//data_id
        [JsonInclude]
        public string png_name;//图片名称
        [JsonInclude]
        public string png_path;//图片路径
        //轨道列表
        [JsonInclude]
        public List<AnimTrack> AnimTrackList = new List<AnimTrack>();
        public AnimSingle()
        {
            data_id = IdGenerator.Generate(IdConstant.ID_TYPE_AnimSingle);
        }
    }

    /// <summary>
    /// 动画组件，每个动画组件都是兵种的一部分的，显示为一个下拉框下的多个动画树，每个树显示的是一张图片的多个属性沿着轨道的变化，对应的是一个动画
    /// </summary>
    public class AnimComponent
    {
        [JsonInclude]
        public long data_id;//data_id
        //动画名称
        [JsonInclude]
        public string component_name = "";

        //图片列表
        [JsonInclude]
        public List<AnimSingle> AnimSingleList = new List<AnimSingle>();
        
        public AnimComponent()
        {
            data_id = IdGenerator.Generate(IdConstant.ID_TYPE_AnimComponent);
        }

    }


    /// <summary>
    /// 动画组合，每个动画组合表示一个兵种的动画，下面是多个动画组件
    /// </summary>
    public class AnimCombination
    {
        [JsonInclude]
        public long data_id;//data_id
        //多个动画组件
        [JsonInclude]
        public List<AnimComponent> AnimComponentList = new List<AnimComponent>();

        //动画组合
        public AnimCombination()
        {
            data_id = IdGenerator.Generate(IdConstant.ID_TYPE_AnimCombination);
        }
		
		//添加动画组件
        public void AddComponent(AnimComponent component)
        {
            AnimComponentList.Add(component);
        }
    }




    //动画数据,一个项目的数据，每个项目都有一个动画数据
    public class AnimationData
    {
        //动画组合唯一id,当前选中的动画组合id,
        [JsonInclude]
        public long selete_id;//这些要改

        [JsonInclude]
        public List<AnimCombination> AnimCombinationList = new List<AnimCombination>();

		
		//获取当前选中的动画组合数据
		public AnimCombination GetSelectCombination()
		{
			for(int i=0;i<AnimCombinationList.Count;i++)
			{
				if (AnimCombinationList[i].data_id == selete_id)
                {
					return AnimCombinationList[i];
				}
			}
			return null;
		}
		
		// 通过唯一数据id,直接获取对应数据
		public object GetDataIdObject(long data_id)
		{
			
			
			
			foreach (AnimCombination combination in AnimCombinationList)
			{
				if (combination.data_id == data_id)
					return combination;
				foreach (AnimComponent component in combination.AnimComponentList)
				{
					if (component.data_id == data_id)
						return component;
					foreach (AnimSingle single in component.AnimSingleList)
					{
						if (single.data_id == data_id)
							return single;
						foreach (AnimTrack track in single.AnimTrackList)
						{
							if (track.data_id == data_id)
								return track;
							foreach (TrackKeyPoint keyPoint in track.KeyList)
							{
								if (keyPoint.data_id == data_id)
									return keyPoint;
							}
						}
					}
				}
			}
			return null;
		}


        //更新对应id的动画组合数据AnimCombination
        public bool UpdatObject(long data_id, AnimCombination newData)
        {
            for (int i = 0; i < AnimCombinationList.Count; i++)
            {
                if (AnimCombinationList[i].data_id == data_id)
                {
                    AnimCombinationList[i] = newData;
                    return true;
                }
            }
            return false;
        }

        //更新对应id的动画组件数据AnimComponent
        public bool UpdatObject(long data_id, AnimComponent newData)
        {
            for (int i = 0; i < AnimCombinationList.Count; i++)
            {
                for (int j = 0; j < AnimCombinationList[i].AnimComponentList.Count; j++)
                {
                    if (AnimCombinationList[i].AnimComponentList[j].data_id == data_id)
                    {
                        AnimCombinationList[i].AnimComponentList[j] = newData;
                        return true;
                    }
                }
            }
            return false;
        }

        //更新对应id的动画数据AnimSingle
        public bool UpdatObject(long data_id, AnimSingle newData)
        {
            for (int i = 0; i < AnimCombinationList.Count; i++)
            {
                for (int j = 0; j < AnimCombinationList[i].AnimComponentList.Count; j++)
                {
                    for (int q = 0; q < AnimCombinationList[i].AnimComponentList[j].AnimSingleList.Count; q++)
                    {
                        if (AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q].data_id == data_id)
                        {
                            AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q] = newData;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //更新对应id的动画轨道AnimTrack
        public bool UpdatObject(long data_id, AnimTrack newData)
        {
            for (int i = 0; i < AnimCombinationList.Count; i++)
            {
                for (int j = 0; j < AnimCombinationList[i].AnimComponentList.Count; j++)
                {
                    for (int q = 0; q < AnimCombinationList[i].AnimComponentList[j].AnimSingleList.Count; q++)
                    {
                        for (int w = 0; q < AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q].AnimTrackList.Count; q++)
                        {
                            if (AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q].AnimTrackList[w].data_id == data_id)
                            {
                                AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q].AnimTrackList[w] = newData;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //更新对应id的关键帧数据TrackKeyPoint
        public bool UpdatObject(long data_id, TrackKeyPoint newData)
        {
            for (int i = 0; i < AnimCombinationList.Count; i++)
            {
                for (int j = 0; j < AnimCombinationList[i].AnimComponentList.Count; j++)
                {
                    for (int q = 0; q < AnimCombinationList[i].AnimComponentList[j].AnimSingleList.Count; q++)
                    {
                        for (int w = 0; w < AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q].AnimTrackList.Count; w++)
                        {
                            for (int e = 0; e < AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q].AnimTrackList[w].KeyList.Count; e++)
                            {
                                if (AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q].AnimTrackList[w].KeyList[e].data_id == data_id)
                                {
                                    AnimCombinationList[i].AnimComponentList[j].AnimSingleList[q].AnimTrackList[w].KeyList[e] = newData;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }


        public override string ToString()
        {
            string str = "";
            str += "AnimCombinationList:\n";
            for (int i = 0; i < AnimCombinationList.Count; i++)
            {
                str += AnimCombinationList[i].ToString() + "/n";
            }
            str += "\n";
            return str;
        }
    }

}