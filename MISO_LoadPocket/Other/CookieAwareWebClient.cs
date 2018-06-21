using System.Net;
using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using MISO_LoadPocket.Other;
using System.IO.Compression;

public class CookieAwareWebClient : WebClient
{
    public CookieAwareWebClient(CookieContainer container)
    {
        CookieContainer = container;
    }

    public CookieAwareWebClient()
     : this(new CookieContainer())
    {   
    }

    public CookieContainer CookieContainer { get; private set; }

    protected override WebRequest GetWebRequest(Uri address)
    {
        var request = (HttpWebRequest)base.GetWebRequest(address);
        request.CookieContainer = CookieContainer;
        return request;
    }
    public string GenerateQueryString(NameValueCollection collection)
    {
        var array = (from key in collection.AllKeys
                     from value in collection.GetValues(key)
                     select string.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value))).ToArray();
        return string.Join("&", array);
    }

    public string PostPage(string url, string json)
    {
        Uri absoluteUri = new Uri(url);
        var cookies = CookieContainer.GetCookies(absoluteUri);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.CookieContainer = new CookieContainer();
        foreach (Cookie cookie in cookies)
        {
            request.CookieContainer.Add(cookie);
        }
        request.Method = "POST";
        request.ContentType = "application/json";

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        var stream = response.GetResponseStream();

        using (var reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();

        }
    }

    public void GetFileAndMove(string url, string filename, string fileLocation, string finalLocation)
    {
        Uri absoluteUri = new Uri(url);
        var cookies = CookieContainer.GetCookies(absoluteUri);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.CookieContainer = new CookieContainer();
        foreach (Cookie cookie in cookies)
        {
            request.CookieContainer.Add(cookie);
        }
        request.Method = "GET";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        var stream = response.GetResponseStream();

        //Download location
        using (var location = File.OpenWrite(fileLocation + filename))
        {
            stream.CopyTo(location);
            stream.Close();
        }
            //Extract to shared filesystem
            using (ZipArchive archive = ZipFile.OpenRead(fileLocation + filename))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        entry.ExtractToFile(Path.Combine(finalLocation, entry.FullName), true);
                    }
                }
            }
        //Removing original zip
        File.Delete(fileLocation + filename);
        
    }

    public void Login(string loginPageAddress, NameValueCollection loginData)
    {
        //Get verification token
        CookieContainer container;
        var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);
        request.Method = "GET";
        container = request.CookieContainer = new CookieContainer();
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        var stream = response.GetResponseStream();
        var token = "";
        using (var reader = new StreamReader(stream))
        {
            //Parsing stream to find appropriate request verification token
            string patternRegion = "<\\s*input\\s*.*name\\s*=\\s*\"__RequestVerificationToken\"\\s*.*value\\s*=\\s*\"(?<value>[\\w-]{108,108})\"\\s*/>";
            RegexOptions regexOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled;
            Regex reg = new Regex(patternRegion, regexOptions);
            MatchCollection mc = reg.Matches(reader.ReadToEnd());

            foreach (Match m in mc)
            {
                token = m.Groups["value"].Value;
            }
        }

        loginData.Add("__RequestVerificationToken", token);
        loginData.Add("returnURL", "");


        //Login request 
        var request2 = (HttpWebRequest)WebRequest.Create(loginPageAddress);
        request2.CookieContainer = new CookieContainer();
        foreach (Cookie cookie in response.Cookies)
        {
            request2.CookieContainer.Add(cookie);
        }

        request2.Method = "POST";
        request2.ContentType = "application/x-www-form-urlencoded";
        var buffer = Encoding.ASCII.GetBytes(GenerateQueryString(loginData));
        request2.ContentLength = buffer.Length;
        var requestStream = request2.GetRequestStream();
        requestStream.Write(buffer, 0, buffer.Length);
        requestStream.Close();

        var response2 = request2.GetResponse();
        response2.Close();

        //Keep login cookies for future calls
        CookieContainer = request2.CookieContainer;
    }
}