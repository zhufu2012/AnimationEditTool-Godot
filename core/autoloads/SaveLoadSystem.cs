using GameLog;
using Godot;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using FileAccess = Godot.FileAccess;

namespace AnimationEditTool_Core
{
    public partial class SaveLoadSystem : Node
    {
        //保存的数据
        public static AnimationData animationData = new AnimationData();
        public static SettingData settingData = new SettingData();
        public static string AnimationData_path = "res://save/AnimationData.json";
        public static string SettingData_path = "res://save/SettingData.json";
        
        //
        double SaveIntervalTime = 0;//保存间隔-距离上一次保存的时间
        private static double SaveInterval = 0;//保存间隔 单位秒


        static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs, UnicodeRanges.CjkSymbolsandPunctuation)
        };
		
		static SaveLoadSystem()
		{
			SaveInterval = DataConstant.SaveIntervalTime;
			FileAccess AnimationData_file = FileAccess.Open(AnimationData_path, FileAccess.ModeFlags.Read);
            FileAccess SettingData_file = FileAccess.Open(SettingData_path, FileAccess.ModeFlags.Read);
            //动画数据读取
            animationData = JsonSerializer.Deserialize<AnimationData>(AnimationData_file.GetAsText(), jsonSerializerOptions);
            AnimationData_file.Close();
            //配置数据读取
            settingData = JsonSerializer.Deserialize<SettingData>(SettingData_file.GetAsText(), jsonSerializerOptions);
            SettingData_file.Close();
		}

        public override void _PhysicsProcess(double delta)
        {
            SaveIntervalTime += delta;
            while (SaveIntervalTime >= SaveInterval)
            {
                Save();
                SaveIntervalTime -= SaveInterval;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public static void Save()
        {
            try
            {
                string animationData_json = JsonSerializer.Serialize(animationData, jsonSerializerOptions);
                //Log.Print(animationData_json);
                string settingData_json = JsonSerializer.Serialize(settingData, jsonSerializerOptions);
                //Log.Print(settingData_json);
                SaveFile(AnimationData_path, animationData_json);//保存动画数据
                SaveFile(SettingData_path, settingData_json);//保存项目数据
            }
            catch (Exception e)
            {
                //Log.Print(e.StackTrace);
            }
        }

        /// <summary>
        /// 保存单个文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="data">数据</param>
        public static void SaveFile(string Path, string data)
        {
            using var file = FileAccess.Open(Path, FileAccess.ModeFlags.Write);
            file.StoreString(data);
            file.Close();
        }


    }
}
