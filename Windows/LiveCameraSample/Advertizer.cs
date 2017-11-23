using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace LiveCameraSample
{

    static class Advertizer
    {

        private static async Task<string> GetTargetGroupTask(String targetGroup)
        {
            return await Task.Run(() => HybrisRest.GetHybrisData(targetGroup));
            
        }

        internal static void ShowHybrisAdvertizing(Microsoft.ProjectOxford.Face.Contract.FaceAttributes faceAttributes, System.Windows.Controls.Image viewImage)
        {
            //Hackathon: call this method with the information from Azure Face API, create a Request to Hybris and show the ad.
            String targetGroup = GetTargetGroup(faceAttributes.Age, faceAttributes.Gender);

            //String hybrisResponseImageURL =  HybrisRest.GetHybrisData(targetGroup);

            //new Thread(() =>
            //{
            //    String hybrisResponseImageURL = HybrisRest.GetHybrisData(targetGroup);
            //    Console.WriteLine("Showing Ad from URL: " + hybrisResponseImageURL);
            //    ShowAd(hybrisResponseImageURL, viewImage);
            //}).Start();
            String hybrisResponseImageURL = HybrisRest.GetHybrisData(targetGroup);
            Console.WriteLine("Showing Ad from URL: " + hybrisResponseImageURL);
            ShowAd(hybrisResponseImageURL, viewImage);


            //Task<String> hybrisResponseImageURL = GetTargetGroupTask(targetGroup);
            //Console.WriteLine("Showing Ad from URL: " + hybrisResponseImageURL);
            //ShowAd(hybrisResponseImageURL., viewImage);

        }

        internal static String GetTargetGroup(double exactAge, String gender)
        {
            //Hackathon: prepare the Target-Group-String for the Hybris REST call: HybrisRest.GetHybrisData(Target-Group-String)
            String prefix = "MRO-";
            String genderSyllable = gender.ToLower().StartsWith("m") ? "M" : "F";
            String ageGroup = "";

            if (exactAge < 20)
            {
                ageGroup = "00-20";
            }
            else if (exactAge < 40)
            {
                ageGroup = "20-40";
            }
            else if (exactAge < 60)
            {
                ageGroup = "40-60";
            } else
            {
                Console.WriteLine("Error regarding Agegroup: ", exactAge, " is bigger than 60");
            }
            Console.WriteLine("Result of GetTargetGroup: ", prefix + genderSyllable + ageGroup);
            return prefix + genderSyllable + ageGroup;
        }

        internal static void ShowAd(String hybrisResponse, System.Windows.Controls.Image viewImage)
        {
            //TODO Hackathon: show the ad in the user interface.
            //System.Net.WebClient client = new System.Net.WebClient();
            //Stream stream = client.OpenRead(hybrisResponse);
            //Bitmap bitmap; bitmap = new Bitmap(stream);
            //BitmapSource bitmapSource = null;


            //if (bitmap != null)
            //{
            //    Console.WriteLine("Bitmap not null! " + bitmap.Size.ToString());
            //    BitmapData bitmapData = bitmap.LockBits(
            //        new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            //        System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            //    bitmapSource = BitmapSource.Create(
            //        bitmapData.Width, bitmapData.Height,
            //        bitmap.HorizontalResolution, bitmap.VerticalResolution,
            //        PixelFormats.Bgr24, null,
            //        bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            //    bitmap.UnlockBits(bitmapData);

                //TODO Hackathon display image
                Visualization.DrawAds(viewImage, new BitmapImage(new Uri(hybrisResponse)));
                //viewImage.Source = ;

                


            //}


            //stream.Flush();
            //stream.Close();
            //client.Dispose();
        }
    }
}
