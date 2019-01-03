using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using Newtonsoft.Json;


namespace CustomAction
{
    public class BaseJSON
    {
        public string name;
        public string description;
        public string path;
        public string type;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
    public class ChromeJSON : BaseJSON
    {
        public List<string> allowed_origins;
        public static ChromeJSON Derive(BaseJSON b, string ao)
        {
            List<string> allowed_origins = new List<string>(ao.Split(';'));
            return new ChromeJSON
            {
                name = b.name,
                description = b.description,
                path = b.path,
                type = b.type,
                allowed_origins = allowed_origins
            };
        }

    }
    public class FirefoxJSON : BaseJSON
    {
        public List<string> allowed_extensions;
        public static FirefoxJSON Derive(BaseJSON b, string ae)
        {
            List<string> allowed_extensions = new List<string>(ae.Split(';'));
            return new FirefoxJSON
            {
                name = b.name,
                description = b.description,
                path = b.path,
                type = b.type,
                allowed_extensions = allowed_extensions
            };
        }
    }

    public class CustomActions
    {
        [CustomAction]
        public static ActionResult MakeChromeManifest(Session session)
        {
            string dest = session["destpath"];

            BaseJSON b = new BaseJSON
            {
                name = session["name"],
                description = session["description"],
                path = session["path"],
                type = session["type"]
            };

            ChromeJSON c = ChromeJSON.Derive(b, session["allowed_origins"]);
            string jsonData = c.ToJSON();
            try
            {
                System.IO.File.WriteAllText(dest, jsonData);
            }
            catch (Exception ex)
            {
                session.Log("ERROR Writing File");
                session.Log(ex.ToString());
                return ActionResult.Failure;
            }

            session.Log("DONE");

            return ActionResult.Success;
        }


    }
}
