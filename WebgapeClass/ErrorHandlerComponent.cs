using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebgapeClass
{
    public class ErrorHandlerComponent
    {
        public void HandleException()
        {
            Exception e = HttpContext.Current.Server.GetLastError();

            if (e == null)
                return;

            e = e.GetBaseException();

            if (e != null)
                HandleException(e);
        }

        public void HandleException(Exception e)
        {
            FormatExceptionDescription(e);
        }

        protected virtual void FormatExceptionDescription(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            if (e != null)
            {
                CommonDAC.ErrorLog(context.Request.Url.ToString(), e.Message, e.StackTrace);
                if (context.Request.Url.ToString().ToLower().IndexOf("/admin/") > -1)
                {
                    System.Web.HttpContext.Current.Response.Redirect("/admin/error.htm");
                    //   context.Response.Redirect("~/Rewriter.aspx");
                }
                else
                {
                    context.Response.Redirect("~/Rewriter.aspx");
                }
            }
        }
    }
}
