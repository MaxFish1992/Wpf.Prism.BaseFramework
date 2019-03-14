using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSignal.Helper.Calculate
{
    /// <summary>
    /// 功能描述    ：FrequencyHelper  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019/1/5 14:16:54 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019/1/5 14:16:54 
    /// </summary>
    public class FrequencyHelper
    {
        /// <summary>
        /// 计算当前频点所在的位置
        /// </summary>
        /// <param name="startFreq">起始频点</param>
        /// <param name="currentFreq">当前频点</param>
        /// <param name="resolution">分辨率</param>
        /// <returns></returns>
        public static double FreqPosition(double startFreq, double currentFreq, double resolution)
        {
            double freqPosition = (currentFreq - startFreq) / resolution;
            return freqPosition;
        }
        /// <summary>
        /// 根据位置计算当前频点值
        /// </summary>
        /// <param name="startFreq">起始频点</param>
        /// <param name="position">位置</param>
        /// <param name="resolution">分辨率</param>
        /// <returns></returns>
        public static double FreqValue(double startFreq, double position, double resolution)
        {
            double freqValue = startFreq + resolution * position;
            return freqValue;
        }
    }
}
