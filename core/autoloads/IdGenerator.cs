using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using GameLog;
namespace AnimationEditTool_Core
{
    public class IdData
    {
        [JsonInclude]
        public Dictionary<int, long> dict = new Dictionary<int, long>();

        public IdData(Dictionary<int, long> dict)
        {
            this.dict = dict;
        }
    }


    //id生成器
    public partial class IdGenerator : Node
    {

        double SaveIntervalTime = 0;//保存间隔-距离上一次保存的时间
        double SaveInterval = 0;//保存间隔 单位秒


        static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs, UnicodeRanges.CjkSymbolsandPunctuation)
        };

        //是否可使用
        private static bool is_use = true;
		
		//这次保存间隔时间中是否有修改
		private static bool is_modify = false;
		
        //表名
        private static string id_file_path = "res://save/IdData.json";
        //键值对
        private static Dictionary<int, long> dict = new Dictionary<int, long>();

        private static object dictLock = new object();

        public void InitData()
        {
            SaveInterval = IdConstant.SaveIdGeneration;
        }

        //初始化
        public override void _Ready()
        {
            InitData();
            FileAccess Id_file = FileAccess.Open(id_file_path, FileAccess.ModeFlags.Read);
            //动画数据读取
            IdData iddata = JsonSerializer.Deserialize<IdData>(Id_file.GetAsText(), jsonSerializerOptions);
            dict = iddata.dict;
            Id_file.Close();
            base._Ready();
        }

        //物理帧
        public override void _PhysicsProcess(double delta)
        {
            SaveIntervalTime += delta;
            while (SaveIntervalTime >= SaveInterval)
            {
                SaveSchedule();
                SaveIntervalTime -= SaveInterval;
            }
        }

        //离开场景树
        public override void _ExitTree()
        {
            SaveSchedule();
            is_use = false;
        }

        //保存id最大值
        public void SaveSchedule()
        {
			if(is_modify)//间隔时间内是否有修改
			{
				Dictionary<int, long> dict_updata = new Dictionary<int, long>();
				for (int i = 1; i < IdConstant.ID_TYPE_DATABASE_MAX; i++)
				{
					long new_value;
					if (dict.TryGetValue(i, out new_value))//能查到，
					{
						dict_updata[i] = new_value;
					}
				}
				if (dict_updata.Count > 0)
				{
					SaveData(dict_updata);
				}
				is_modify = false;
			}
        }

        //获取id的最大值，对应id加1
        public static long get_id_max(int type, long define)
        {
            long id = 0;
            lock (dictLock)
            {
                if (dict.ContainsKey(type))
                {
                    id = dict[type];
                    dict[type] = Interlocked.Increment(ref id);
                }
                else
                {
                    dict[type] = define;
                    id = define;
                }
            }
            return id;
        }


        //生成对应类型的唯一id
        public static long Generate(int type)
        {
            if (is_use)
            {
                long id_max = get_id_max(type, 1);
				is_modify = true;
                return makeObjectID(type, id_max);
            }
            else
            {
                throw new Exception("IdGenerator is no user");
            }
        }

        //获取id的类型
        public static int GetType(long id)
        {
            int typeID = (int)(id & 0x7E0000000000);
            return typeID >> 41;
        }

        //构造id
        private static long makeObjectID(int type, long IdMax)
        {
            return ((type & 63) << 41) | (IdMax & 0x1FFFFFFFFFF);
        }


        //保存id使用数据
        public void SaveData(Dictionary<int, long> dict_updata)
        {
            try
            {
                string data = JsonSerializer.Serialize(new IdData(dict_updata), jsonSerializerOptions);
                using var file = FileAccess.Open(id_file_path, FileAccess.ModeFlags.Write);
                file.StoreString(data);
				file.Close();
            }
            catch (Exception e)
            {
                //Log.Print(e.StackTrace);
            }
        }
    }
}