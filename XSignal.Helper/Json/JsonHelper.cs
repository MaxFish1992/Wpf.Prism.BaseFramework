using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

namespace XSignal.Helper.Json
{
    /// <summary>
    /// 功能描述    ：JsonHelper  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019/3/14 9:13:43 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019/3/14 9:13:43 
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 读取JSON文件
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns>JSON文件中的value值</returns>
        public static JObject ReadJson(string jsonFile)
        {
            try
            {
                using (System.IO.StreamReader file = System.IO.File.OpenText(jsonFile))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject o = (JObject)JToken.ReadFrom(reader);
                        return o;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        /// <summary>
        /// 对象转json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            return obj != null ? JsonConvert.SerializeObject(obj) : null;
        }
        /// <summary>
        /// json文本转实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
