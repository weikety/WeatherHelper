<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Timers" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Threading" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        System.Timers.Timer aTimer = new System.Timers.Timer();
        aTimer.Elapsed += new ElapsedEventHandler(TimeEvent);
        // 设置引发时间的时间间隔 此处设置为 3600000 秒(1小时)
        aTimer.Interval = 100000;
        aTimer.Enabled = true;  
    }

    void TimeEvent(object source, ElapsedEventArgs e)
    {
        UpWeatherJson();
    }  

    //下载天气 Json
    void UpWeatherJson()
    {
        string url = ConfigurationManager.AppSettings["WeatherUrl"];
        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
        System.IO.Stream responseStream = response.GetResponseStream();
        System.IO.StreamReader sr = new System.IO.StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
        string responseText = sr.ReadToEnd();
        sr.Close();
        sr.Dispose();
        responseStream.Close();
        //写文件流
        System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "WeatherJson.txt", responseText, Encoding.UTF8);
    }
    
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。

    }
   
       
</script>
