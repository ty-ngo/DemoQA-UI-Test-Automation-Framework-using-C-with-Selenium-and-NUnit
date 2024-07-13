using AventStack.ExtentReports.Utils;
using final.Models;
using Newtonsoft.Json;

namespace final.Core.Helper
{
    public class JsonHelper
    {
        public static IEnumerable<T> LoadJsonData<T>(string fileLocation)
        {
            //string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            //string jsonFilePath = Path.Combine(projectDirectory, fileLocation);
            var jsonString = File.ReadAllText(fileLocation);
            var dataList = JsonConvert.DeserializeObject<List<T>>(jsonString);

            if (dataList.IsNullOrEmpty())
            {
                throw new InvalidOperationException("File name is not exist");
            }

            return dataList;
        }

        public static Student LoadStudentJson(string fileLocation)
        {
            var jsonString = File.ReadAllText(fileLocation);
            return JsonConvert.DeserializeObject<Student>(jsonString);
        }

    }
}
