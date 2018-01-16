using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace MyFirstConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //using statement will dispose your object when its done
            using (ClientContext ctx = Helpers.ContextHelper.GetClientContext("https://folkis2017.sharepoint.com/sites/Tim"))
            {
                CodeExamples.CSOM101.GetWebTitle(ctx);
            }
            

        }

       

    }
}
