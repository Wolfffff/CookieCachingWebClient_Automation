using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using MISO_LoadPocket.Other;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        try{
            
            String[] configs = File.ReadAllText(Directory.GetCurrentDirectory() + @"/configs.csv").Split(',');
            var email = configs[0];
            var password = configs[1];
            var finalLocation = configs[2];
            var smtpUser = configs[3];
            var smtpPass = configs[4];
            var alertEmail = configs[5];
            var phone = configs[6];

            var location = Directory.GetCurrentDirectory() + "/";

            //Json copy pasted from a "get all" request to MISO's api
            var json = "{\"QueryValues\":{\"rctype\":\"Load Pocket Report\"},\"NavigationItem\":{\"ParentPropertyValue\":\"Load Pocket Report\",\"ChildPropertySystemName\":null,\"DisplayResultInGrid\":false,\"GridMetadataColumns\":[],\"ResultSortField\":null,\"ResultSortDirection\":null,\"UseDynamicChoiceMode\":null,\"UseDynamicDateNavigation\":false,\"UseDynamicYearNavigation\":false,\"UseDynamicQuarterNavigation\":false,\"UseDynamicMonthNavigation\":false,\"UseDynamicDayNavigation\":false,\"ChildDocumentNavigationItems\":null,\"Title\":\"\",\"Summary\":\"<p><a title=\\\"MISO Load Pocket Report Readers Guide\\\" href=\\\"/api/documents/getbyguid/c35f6b50-fdc2-5742-bd3c-cf185c23ee57\\\">MISO Load Pocket Report Readers Guide</a></p>\",\"LoadedNavigableDocuments\":[{\"FileName\":\"Load_Pocket_20171121.zip\",\"Name\":\"Load Pocket 20171121\",\"Url\":\"/api/documents/getbymediaid/129294\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129294,\"Uploaded\":\"2018-02-26T19:42:50Z\",\"UploadedBy\":34,\"ObjectId\":137504,\"Created\":\"2018-02-26T19:43:06Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:43:10Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"221e5d05-c71b-5d66-996c-d9c22d10437c\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171122.zip\",\"Name\":\"Load Pocket 20171122\",\"Url\":\"/api/documents/getbymediaid/129295\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129295,\"Uploaded\":\"2018-02-26T19:42:56Z\",\"UploadedBy\":34,\"ObjectId\":137505,\"Created\":\"2018-02-26T19:43:12Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:43:15Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"e3aaf099-2679-5c51-a1d3-5d309a0447be\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171123.zip\",\"Name\":\"Load Pocket 20171123\",\"Url\":\"/api/documents/getbymediaid/129233\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129233,\"Uploaded\":\"2018-02-26T19:27:55Z\",\"UploadedBy\":34,\"ObjectId\":137443,\"Created\":\"2018-02-26T19:28:11Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:28:17Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"018ab122-2ac6-52cc-a241-9f033d9b5143\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171124.zip\",\"Name\":\"Load Pocket 20171124\",\"Url\":\"/api/documents/getbymediaid/129234\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129234,\"Uploaded\":\"2018-02-26T19:28:05Z\",\"UploadedBy\":34,\"ObjectId\":137444,\"Created\":\"2018-02-26T19:28:21Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:28:27Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"f026d520-5e27-51c1-9858-a08fea7fefdd\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171125.zip\",\"Name\":\"Load Pocket 20171125\",\"Url\":\"/api/documents/getbymediaid/129235\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129235,\"Uploaded\":\"2018-02-26T19:28:16Z\",\"UploadedBy\":34,\"ObjectId\":137445,\"Created\":\"2018-02-26T19:28:31Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:28:38Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"2f4ae535-4c20-57b1-9060-c63a5298229b\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171126.zip\",\"Name\":\"Load Pocket 20171126\",\"Url\":\"/api/documents/getbymediaid/129296\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129296,\"Uploaded\":\"2018-02-26T19:43:02Z\",\"UploadedBy\":34,\"ObjectId\":137506,\"Created\":\"2018-02-26T19:43:18Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:43:21Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"7763eb39-3bd5-53da-a29a-91be0327d7e2\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171127.zip\",\"Name\":\"Load Pocket 20171127\",\"Url\":\"/api/documents/getbymediaid/129297\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129297,\"Uploaded\":\"2018-02-26T19:43:08Z\",\"UploadedBy\":34,\"ObjectId\":137507,\"Created\":\"2018-02-26T19:43:24Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:43:27Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"eb27d73c-aa3a-5820-98c8-23fd308d5d18\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171128.zip\",\"Name\":\"Load Pocket 20171128\",\"Url\":\"/api/documents/getbymediaid/129236\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129236,\"Uploaded\":\"2018-02-26T19:28:26Z\",\"UploadedBy\":34,\"ObjectId\":137446,\"Created\":\"2018-02-26T19:28:42Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:28:47Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"1e6b4040-2c1b-538f-8091-82f51514dcd4\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171129.zip\",\"Name\":\"Load Pocket 20171129\",\"Url\":\"/api/documents/getbymediaid/129237\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129237,\"Uploaded\":\"2018-02-26T19:28:36Z\",\"UploadedBy\":34,\"ObjectId\":137447,\"Created\":\"2018-02-26T19:28:51Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:28:57Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"c9e3df76-6555-5ba6-8f1a-a1476c7a5a1f\",\"accesstype\":\"Visitors-RA\"}},{\"FileName\":\"Load_Pocket_20171130.zip\",\"Name\":\"Load Pocket 20171130\",\"Url\":\"/api/documents/getbymediaid/129298\",\"MimeType\":\"application/zip\",\"MimeTypeName\":\"Compressed archive\",\"MediaId\":129298,\"Uploaded\":\"2018-02-26T19:43:14Z\",\"UploadedBy\":34,\"ObjectId\":137508,\"Created\":\"2018-02-26T19:43:30Z\",\"CreatedBy\":34,\"Updated\":\"2018-02-26T19:43:33Z\",\"UpdatedBy\":34,\"IsFollowing\":false,\"CategoryId\":26,\"AuthorizedRoles\":[\"Visitors-RA\"],\"Metadata\":{\"guid\":\"48c75229-1268-5621-8353-57cc4e89cad6\",\"accesstype\":\"Visitors-RA\"}}],\"TotalAvailableResults\":98},\"Filters\":[{\"PropertyName\":\"rctype\",\"Value\":\"\"}],\"EnableFollowDocuments\":null,\"Top\":null,\"Skip\":10}";

            //Stops from running if files for the current date already exist
            if (FileCheck.Check(finalLocation))
            {
                return;
            }

            var webClient = new CookieAwareWebClient();


            var collection = new NameValueCollection();
            collection.Add("Email", email);
            collection.Add("Password", password);



            Console.Write("Attempting to log in to MISO website... \n");
            webClient.Login("https://www.misoenergy.org/Account/Login", collection);

            //Post JSON using cookie authenticated client and JSON from standard request
            Console.Write("Acquiring document list from MISO... \n");
            var response = webClient.PostPage("https://www.misoenergy.org/api/documents/getnavigabledocuments", json);

            //Parse the response JSON
            JObject parsedResponse = JObject.Parse(response);
            var list = parsedResponse["documents"].OrderByDescending(t => t["FileName"]).ToList();
            var single = list.FirstOrDefault();
            var url = "https://www.misoenergy.org" + single["Url"];
            var filename = single["FileName"].ToString();

            //Make sure that the load pocket file that is the "latest" is from the current date.
            if (filename != "Load_Pocket_" + DateTime.Now.ToString("yyyyMMdd") + ".zip")
            {
                throw new Exception("The current load pocket file is not from the current date.");
            }

            //Download zip file, store temporarily in location and the extract to finalLocation before deleting zip from location
            webClient.GetFileAndMove(url, filename, location, finalLocation);


            //Used to send alert on working instance
            if (FileCheck.Check(finalLocation))
            {
                Console.Write("Attempting to send email notifications... \n");
                MailHelper.SendAlert(smtpUser, smtpPass, alertEmail, phone);
            }

            Console.Write(filename + " was extracted to " + finalLocation + "");
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }

    }
}



