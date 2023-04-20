using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.IO;

public class MainClass
{

    public static void Main()
    {

        WebClient client = new WebClient();
        string s = client.DownloadString("https://coderbyte.com/api/challenges/json/json-cleaning");
        using (Stream data = new MemoryStream(Encoding.UTF8.GetBytes(s)))
        {
            StreamReader read = new StreamReader(data);
            string text = read.ReadToEnd();
            dynamic json = JObject.Parse(text);
            RemoveJson(json["name"]);
            RemoveJson(json["age"]);
            RemoveJson(json["DOB"]);
            RemoveJson(json["hobbies"]);
            RemoveJson(json["education"]);
            Console.WriteLine(json);
        }

    }
    static string RemoveJson(dynamic node)
    {
        bool removed = true;
        while (removed)
        {
            try
            {
                foreach (var item in node)
                {
                    if (item.Value.ToString() == "" || item.Value.ToString() == "N/A")
                    {
                        item.Remove();
                        removed = true;
                        break;
                    }
                    else
                        removed = false;
                }
            }
            catch (Exception)
            {
                removed = false;
            }
        }
        return "";
    }
}