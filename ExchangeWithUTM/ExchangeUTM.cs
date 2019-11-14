using System;
using System.Threading;
using System.Configuration;
using UTM_Interchange;

namespace ExchangeUTM
{
    public class ExchangeUTM
    {
        bool Enabled { get; set; }
        int Timeout { get; set; }

        public ExchangeUTM()
        {
            Enabled = true;

            try
            {
                Timeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ServiceTimeout"));

                if (Timeout == 0)
                {
                    Timeout = 20000;
                }
            }
            catch(Exception ex)
            {
                Log log = new Log(ex);
            }
        }
        public void Start()
        {
            Log log = new Log(ConfigurationManager.AppSettings.Get("StartWork") + " - Service timeout: " + Timeout + " ms");

            while (Enabled)
            {
                Transport.GetXMLFromUTM();
                Transport.UploadXMLToUTM();
                Thread.Sleep(Timeout);
            }
        }
        public void Stop()
        {
            Log log = new Log(ConfigurationManager.AppSettings.Get("StopWork"));
            Enabled = false;
        }
        public void Pause()
        {
            Log log = new Log(ConfigurationManager.AppSettings.Get("PauseWork"));
            Enabled = false;
        }
        public void Continue()
        {
            Log log = new Log(ConfigurationManager.AppSettings.Get("ContinueWork"));
            Enabled = true;
        }
        public void Shutdown()
        {
            Log log = new Log(ConfigurationManager.AppSettings.Get("ShutDownWindows"));
            Enabled = false;
        }
    }
}
