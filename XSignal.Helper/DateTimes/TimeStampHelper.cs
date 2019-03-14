using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSignal.Helper.DateTimes
{
    /// <summary>
    /// 功能描述    ：时间戳帮助类  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019/1/24 16:08:04 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019/1/24 16:08:04 
    /// </summary>
    public class TimeStampHelper
    {
        /// <summary>
        /// 生成时间戳 
        /// </summary>
        /// <returns>当前时间减去 1970-01-01 00.00.00 得到的毫秒数</returns>
        public string GetTimeStamp()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = DateTime.Now;
            long unixTime = (long)System.Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
            return unixTime.ToString();
        }
        /// <summary>
        /// 日期转时间戳
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToTimestamp(DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return (double)span.TotalSeconds;
        }
        /// <summary>
        /// 时间戳转日期
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertTimestamp(double timestamp)
        {
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime newDateTime = converted.AddSeconds(timestamp);
            return newDateTime.ToLocalTime();
        }
    }
}
