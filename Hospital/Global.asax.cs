using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using Hospital.Models;

namespace Hospital
{
    public class Global : System.Web.HttpApplication
    {

        public override void Init()
        {
            this.BeginRequest += new EventHandler(Application_BeginRequest);
            base.Init();
        }
        //public static List<EntityLogin> UsersData { get { return _UserData; } set { _UserData = value; } }

        //static List<EntityLogin> _UserData = new List<EntityLogin>();
        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception exc = Server.GetLastError();
            Commons.FileLog(exc.InnerException.Message, exc);
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            BasePage page = new BasePage();
            page.AuthenticateUser();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            Commons.FileLog("Session Expired" + DateTime.Now.Date, new Exception());
        }


        
    }
}
