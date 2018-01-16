using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstConsoleApp.CodeExamples
{
    class CSOM101
    {

        public static void GetWebTitle(ClientContext context)
        {

            //Web w1 = context.Web;


            ////Take your order(this loads everything)
            //context.Load(context.Web);
            ////goes and gets it. takes about 400ms or longer
            //context.ExecuteQuery();
            //Console.WriteLine(context.Web.Title);

            Web w2 = context.Web;

            //only get the properties you need(This is a faster way of loading, you can speficify what you want to get)
            context.Load(w2, w => w.Title);
            //goes and gets it. takes about 400ms or longer
            context.ExecuteQuery();
            Console.WriteLine(w2.Title);



            Console.WriteLine("Press enter to continue:");
            Console.ReadLine();
        }


    }
}
