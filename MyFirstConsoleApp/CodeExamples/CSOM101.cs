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

        public static void CreateTaskList(ClientContext ctx)
        {
            ListCreationInformation info = new ListCreationInformation();
            info.Title = "My Task";
            info.TemplateType = 107;
            // or like this info.TemplateType = (int)ListTemplateType.Tasks;
            info.Url = "list/mytask";
            ctx.Web.Lists.Add(info);

            ctx.ExecuteQuery();

        }

        public static void GetAllListsNotHidden(ClientContext ctx)
        {
            ListCollection lists = ctx.Web.Lists;

            //ctx.Load(lists); //takes longer to get everything
            ctx.Load(lists, lsts => lsts.Include(
                l => l.Title,
                l => l.DefaultViewUrl).Where(l => l.Hidden != true));

            ctx.ExecuteQuery();


            foreach (var list in lists)
            {
                    Console.WriteLine(list.Title);
            }

        }

        public static void AddGoogleToNav(ClientContext ctx)
        {

            Web web = ctx.Web;
            NavigationNodeCollection QuickLanchcoll = web.Navigation.QuickLaunch;
            //ctx.Load(QuickLanchcoll);
            //ctx.ExecuteQuery();

            NavigationNodeCreationInformation NewNode = new NavigationNodeCreationInformation();
            NewNode.Title = "Google";
            NewNode.Url = "https://google.com";

            QuickLanchcoll.Add(NewNode);
            //ctx.Load(QuickLanchcoll);
            ctx.ExecuteQuery();


            

        }

        public static void CreateGeneraricList2(ClientContext ctx)
        {

            var lists = ctx.Web.Lists;
            var results = ctx.LoadQuery(lists.Where(list => list.Title == "Custom List"));
            ctx.Web.Context.ExecuteQuery();

            if (!results.Any())
            {
                ListCreationInformation info = new ListCreationInformation();
                info.Title = "My List";
                info.TemplateType = 100;
                info.Url = "lists/mylist";
                ctx.Web.Lists.Add(info);
                ctx.ExecuteQuery();
            }
            else
            {
                Console.WriteLine("List already exists. Give it another url and name");
            }
        }

        public static void ReadListItems(ClientContext ctx, string listname)
        {
            List myList = ctx.Web.Lists.GetByTitle(listname);
            ctx.Load(myList);
            ctx.ExecuteQuery();

            CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
            ListItemCollection items = myList.GetItems(query);

            // Retrieve all items in the ListItemCollection from List.GetItems(Query). 
            ctx.Load(items);
            ctx.ExecuteQuery();

            Console.WriteLine("My list items displayed below");
            foreach (ListItem listItem in items)
            {
                Console.WriteLine(listItem["Title"]);
            }

        }

        public static void AddItemToMylist(ClientContext ctx)
        {
            List myList = ctx.Web.Lists.GetByTitle("My List");

            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem newListItem = myList.AddItem(itemCreateInfo);
            newListItem["Title"] = "Test Item!";

            newListItem.Update();

            ctx.ExecuteQuery();


        }


    }
}
