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
                //// take your order
                //context.Load(w1);
                //// Goes and gets it. Takes about 400 ms or longer
                //context.ExecuteQuery();
                //Console.WriteLine(w1.Title);
                //Console.WriteLine(w1.Url);
                // the below code is faster as we are only requesting 
                // the properties we want from the web object
                Web w2 = context.Web;
                // only get the propeties you need
                context.Load(w2, w => w.Title, w => w.Url);
                // Goes and gets it. 
                context.ExecuteQuery();
                Console.WriteLine(w2.Title);
                Console.WriteLine(w2.Url);

        }

        public static void UpdateTitelOfWeb(ClientContext ctx, string NewTitle)
        {
            Web web = ctx.Web;

            web.Title = NewTitle;
            web.Update();

            //must executeQuery to be able to update the sharepoint
            ctx.ExecuteQuery();


        }

        public static void ListAllLists(ClientContext ctx)
        {
            ListCollection lists = ctx.Web.Lists;
            ctx.Load(lists); //takes longer to get everything
            ctx.Load(lists, lsts => lsts.Include(
                l => l.Title,
                l => l.DefaultViewUrl));

            ctx.ExecuteQuery();

            foreach (var list in lists)
            {
                Console.WriteLine(list.Title);
                Console.WriteLine(list.DefaultViewUrl);
            }


        }

        public static void CreateDocumentLibrary(ClientContext ctx)
        {

            ListCreationInformation info = new ListCreationInformation();
            info.Title = "New Doc Library";
            info.TemplateType = 101;
            info.Description = "Tims New library";
            info.Url = "NewDocLib";
            ctx.Web.Lists.Add(info);

            ctx.ExecuteQuery();
        }

        public static void CreateGenericList(ClientContext ctx)
        {
            // ctx.Web.Lists.Where(l => l.Title == "Custom List");

            //List list = ctx.Web.Lists.GetByTitle("Custom List");
            //ctx.Load(list);
            //ctx.ExecuteQuery();

            var lists = ctx.Web.Lists;
            var results = ctx.LoadQuery(lists.Where(list => list.Title == "Custom List"));
            ctx.Web.Context.ExecuteQuery();

            if (!results.Any())
            {
                ListCreationInformation info = new ListCreationInformation();
                info.Title = "Custom List";
                info.TemplateType = 100;
                info.Description = "Custom List";
                info.Url = "lists/customlist";
                ctx.Web.Lists.Add(info);
                ctx.ExecuteQuery();
            }
            else
            {
                Console.WriteLine("List already exists. Give it another url and name");
            }

        }

    }
}
