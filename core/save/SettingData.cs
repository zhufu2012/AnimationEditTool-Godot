using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AnimationEditTool_Core
{
	//编辑参数  描述 :  编辑框(当前参数)
	public class EditParam
	{
		public string key = ""; //键
		public object value;	//当前参数
		public string Describe = "";//描述文字
	}
	
	
	
	//项目设置数据,一个项目的数据，每个项目都有一个项目设置数据
	public class SettingData
	{
		
		//参数列表
		[JsonInclude]
		List<EditParam> ParamList = new List<EditParam>();
		
	}
}