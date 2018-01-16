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
                //CodeExamples.CSOM101.GetWebTitle(ctx);
                //CodeExamples.CSOM101.UpdateTitelOfWeb(ctx, "Tims Sharepoint Site");
                //CodeExamples.CSOM101.ListAllLists(ctx);
              ////  CodeExamples.CSOM101.CreateDocumentLibrary(ctx);
              //  CodeExamples.CSOM101.CreateGenericList(ctx);
                //CodeExamples.CSOM101.CreateTaskList(ctx);
                CodeExamples.CSOM101.GetAllListsNotHidden(ctx);
                // CodeExamples.CSOM101.AddGoogleToNav(ctx);
               // CodeExamples.CSOM101.CreateGeneraricList2(ctx);
                CodeExamples.CSOM101.ReadListItems(ctx, "My List");
                CodeExamples.CSOM101.AddItemToMylist(ctx);
            }


            Console.WriteLine("Press enter to continue");
            Console.ReadKey();






        }

       

    }
}
