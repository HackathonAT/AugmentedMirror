using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;

namespace LiveCameraSample 
{
    class HybrisRest
    {
        private const String URL_PREFIX = "https://my304160.crm.ondemand.com/sap/c4c/odata/v1/c4codata/";

        /**
         * TODO Hackathon: call this Method for the hybris connection
         * There are currently 6 different ads defined in Hybris: 
         * "MRO-M00-20" for males under 20
         * "MRO-M20-40" for males between 20 and 40
         * "MRO-M40-60" for males over 40
         * "MRO-F00-20" for females under 20
         * "MRO-F20-40" for females between 20 and 40
         * "MRO-F40-60" for females over 40
         * If you invoke the GetHybrisData("MRO-M20-40"), the method will return a String containing the URL 
         *    of an image of the ad for middle aged men.
         */
        public static String GetHybrisData(String targetGroup)
        {
            Console.WriteLine(targetGroup);
            String products = GetHybrisRequest(URL_PREFIX + "ProductCategoryAssignmentCollection?$expand=Product/ProductAttachment&$filter=ProductCategoryID eq '"+targetGroup+"'");
            List <int> indicesOfLinks = AllIndexesOf(products, "LinkWebURI");
            if (indicesOfLinks.Count >= 2)
            {
                String adImageURL = products.Substring(indicesOfLinks[0] + 11, indicesOfLinks[1] - indicesOfLinks[0] - 15);
                Console.WriteLine(adImageURL);
                return adImageURL;
            }
            else
            {
                return("Error: No Ad found or no such target group");
            }
        }

        public static List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        public static String GetHybrisRequest(String query)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(query);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            CredentialCache myCache = new CredentialCache();

            myCache.Add(new Uri("https://my304160.crm.ondemand.com"), "Basic", new NetworkCredential("CONNECTION_AZURE", "Initial02"));

            request.Credentials = myCache;

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.

            // Clean up the streams and the response.
            reader.Close();
            response.Close();

           // Console.WriteLine(responseFromServer);
            return responseFromServer;
        }
    }
}
