/////////////////////////////////////////
//
//    ClassName:     WeatherHelper.cs
//    ClassWriter:   by weikety
//    CreateTime:          2014-12-30 16:55:17
//    ChangeTime:          2014-12-30 16:55:17
//    Action:        天气操作类
//
/////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;

/// <summary>
/// WeatherHelper 的摘要说明
/// </summary>
public class WeatherHelper
{
    #region 获取网站根目录下面的七天天气Json数据
    /// <summary>
    /// 7天天气预报
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns></returns>
    public static Dictionary<string, object> WeatherInfo(int futureday)
    {
        Dictionary<string, object> val = new Dictionary<string, object>();
        if (futureday>6)
        {
            val.Add("weaid", "未知");
            val.Add("days", DateTime.Now.Date.AddDays(futureday).ToString("yyyy-MM-dd"));
            val.Add("week", TimeHelper.GetFormatWeek1(DateTime.Now.Date.AddDays(futureday)));
            val.Add("cityno", "未知");
            val.Add("citynm", "未知");
            val.Add("cityid", "未知");
            val.Add("temperature", "未知");
            val.Add("humidity", "未知");
            val.Add("weather", "未知");
            val.Add("weather_icon", "Images/Weather/d/0.png");
            val.Add("weather_icon1", "Images/Weather/n/0.png");
            val.Add("wind", "未知");
            val.Add("winp", "未知");
            val.Add("temp_high", "?");
            val.Add("temp_low", "?");
            val.Add("humi_high", "?");
            val.Add("humi_low", "?");
            val.Add("weatid", "0");
            val.Add("weatid1", "0");
            val.Add("windid", "0");
            val.Add("winpid", "0");
        } 
        else
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/WeatherJson.txt");
            string jsonData = File.ReadAllText(path, Encoding.Default);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(jsonData);
            object[] result = (object[])json["result"];           
            val = (Dictionary<string, object>)result[futureday];           
        }
        return val;
    }
    #endregion

    #region 未来天气
    /// <summary>
    /// 获取日期
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>日期</returns>
    public static string get_days(int futureday)
    {
        return WeatherInfo(futureday)["days"].ToString();
    }

    /// <summary>
    /// 获取星期
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>星期</returns>
    public static string get_week(int futureday)
    {
        return WeatherInfo(futureday)["week"].ToString();
    }

    /// <summary>
    /// 获取城市名称拼音
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>城市名称拼音</returns>
    public static string get_cityno(int futureday)
    {
        return WeatherInfo(futureday)["cityno"].ToString();
    }

    /// <summary>
    /// 获取城市名称
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>城市名称</returns>
    public static string get_citynm(int futureday)
    {
        return WeatherInfo(futureday)["citynm"].ToString();
    }

    /// <summary>
    /// 获取城市气象编号
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>城市气象编号</returns>
    public static string get_cityid(int futureday)
    {
        return WeatherInfo(futureday)["cityid"].ToString();
    }

    /// <summary>
    /// 获取温度
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>温度</returns>
    public static string get_temperature(int futureday)
    {
        return WeatherInfo(futureday)["temperature"].ToString();
    }

    /// <summary>
    /// 获取华氏摄氏度
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>华氏摄氏度</returns>
    public static string get_humidity(int futureday)
    {
        return WeatherInfo(futureday)["humidity"].ToString();
    }

    /// <summary>
    /// 获取天气(白天至夜间)
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>天气</returns>
    public static string get_weather(int futureday)
    {
        return WeatherInfo(futureday)["weather"].ToString();
    }

    /// <summary>
    /// 获取天气(白天)
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>天气</returns>
    public static string get_weather_d(int futureday)
    {
        return Get_WeatherByID(Convert.ToInt32(get_weatid_d(futureday)));
    }

    /// <summary>
    /// 获取天气(夜间)
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>天气</returns>
    public static string get_weather_n(int futureday)
    {
        return Get_WeatherByID(Convert.ToInt32(get_weatid_n(futureday)));
    }

    /// <summary>
    /// 获取白天天气图标(本地版)
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>白天天气图标（相对根目录）</returns>
    public static string get_weather_icon_d_l(int futureday)
    {
        //return WeatherInfo(futureday)["weather_icon"].ToString();

        return "Images/Weather/d/" + get_weatid_d(futureday) + ".png";

    }

    /// <summary>
    /// 获取夜间天气图标(本地版)
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>夜间天气图标（相对根目录）</returns>
    public static string get_weather_icon_n_l(int futureday)
    {
        //return WeatherInfo(futureday)["weather_icon1"].ToString();

        return "Images/Weather/n/" + get_weatid_n(futureday) + ".png";
    }

    /// <summary>
    /// 获取白天天气图标(网络版)
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>白天天气图标全路径</returns>
    public static string get_weather_icon_d_w(int futureday)
    {
        return WeatherInfo(futureday)["weather_icon"].ToString();
    }

    /// <summary>
    /// 获取夜间天气图标(网络版)
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>夜间天气图标全路径</returns>
    public static string get_weather_icon_n_w(int futureday)
    {
        return WeatherInfo(futureday)["weather_icon1"].ToString();
    }

    /// <summary>
    /// 获取风向
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>风向</returns>
    public static string get_wind(int futureday)
    {
        return WeatherInfo(futureday)["wind"].ToString();
    }

    /// <summary>
    /// 获取风力
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>风力</returns>
    public static string get_winp(int futureday)
    {
        return WeatherInfo(futureday)["winp"].ToString();
    }

    /// <summary>
    /// 获取最高温度
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>最高温度</returns>
    public static string get_temp_high(int futureday)
    {
        return WeatherInfo(futureday)["temp_high"].ToString();
    }

    /// <summary>
    /// 获取最低温度
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>最低温度</returns>
    public static string get_temp_low(int futureday)
    {
        return WeatherInfo(futureday)["temp_low"].ToString();
    }

    /// <summary>
    /// 获取最高湿度
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>最高湿度</returns>
    public static string get_humi_high(int futureday)
    {
        return WeatherInfo(futureday)["humi_high"].ToString();
    }

    /// <summary>
    /// 获取最低湿度
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>最低湿度</returns>
    public static string get_humi_low(int futureday)
    {
        return WeatherInfo(futureday)["humi_low"].ToString();
    }

    /// <summary>
    /// 获取白天天气ID
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>天气ID</returns>
    public static string get_weatid_d(int futureday)
    {
        return WeatherInfo(futureday)["weatid"].ToString();
    }

    /// <summary>
    /// 获取夜间天气ID
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>夜间天气ID</returns>
    public static string get_weatid_n(int futureday)
    {
        return WeatherInfo(futureday)["weatid1"].ToString();
    }

    /// <summary>
    /// 获取风向ID
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>风向ID</returns>
    public static string get_windid(int futureday)
    {
        return WeatherInfo(futureday)["windid"].ToString();
    }

    /// <summary>
    /// 获取风力ID
    /// </summary>
    /// <param name="futureday">未来天数0-6</param>
    /// <returns>风力ID</returns>
    public static string get_winpid(int futureday)
    {
        return WeatherInfo(futureday)["winpid"].ToString();
    }

    #endregion

    #region 天气ID获取天气描述
    /// <summary>
    /// 天气ID获取天气描述
    /// </summary>
    /// <param name="WeatherID">天气ID</param>
    /// <returns>天气描述</returns>
    /// 相应的天气图片使用 天气ID命名（ID.png），变天放d文件夹，夜间放n文件夹
    public static string Get_WeatherByID(int WeatherID)
    {
        string WeatherInfo = "未知";
        switch (WeatherID)
        {
            case 1:
                WeatherInfo = "晴";
                break;
            case 2:
                WeatherInfo = "多云";
                break;
            case 3:
                WeatherInfo = "阴";
                break;
            case 4:
                WeatherInfo = "阵雨";
                break;
            case 5:
                WeatherInfo = "雷阵雨";
                break;
            case 6:
                WeatherInfo = "雷阵雨有冰雹";
                break;
            case 7:
                WeatherInfo = "雨夹雪";
                break;
            case 8:
                WeatherInfo = "小雨";
                break;
            case 9:
                WeatherInfo = "中雨";
                break;
            case 10:
                WeatherInfo = "大雨";
                break;
            case 11:
                WeatherInfo = "暴雨";
                break;
            case 12:
                WeatherInfo = "大暴雨";
                break;
            case 13:
                WeatherInfo = "特大暴雨";
                break;
            case 14:
                WeatherInfo = "阵雪";
                break;
            case 15:
                WeatherInfo = "小雪";
                break;
            case 16:
                WeatherInfo = "中雪";
                break;
            case 17:
                WeatherInfo = "大雪";
                break;
            case 18:
                WeatherInfo = "暴雪";
                break;
            case 19:
                WeatherInfo = "雾";
                break;
            case 20:
                WeatherInfo = "冻雨";
                break;
            case 21:
                WeatherInfo = "沙尘暴";
                break;
            case 22:
                WeatherInfo = "小雨-中雨";
                break;
            case 23:
                WeatherInfo = "中雨-大雨";
                break;
            case 24:
                WeatherInfo = "大雨-暴雨";
                break;
            case 25:
                WeatherInfo = "暴雨-大暴雨";
                break;
            case 26:
                WeatherInfo = "大暴雨-特大暴雨";
                break;
            case 27:
                WeatherInfo = "小雪-中雪";
                break;
            case 28:
                WeatherInfo = "中雪-大雪";
                break;
            case 29:
                WeatherInfo = "大雪-暴雪";
                break;
            case 30:
                WeatherInfo = "浮尘";
                break;
            case 31:
                WeatherInfo = "扬沙";
                break;
            case 32:
                WeatherInfo = "强沙尘暴";
                break;
            case 33:
                WeatherInfo = "霾";
                break;
            default:
                WeatherInfo = "未知";
                break;

        }

        return WeatherInfo;
    }

    #endregion

}