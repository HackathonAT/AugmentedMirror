using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.ProjectOxford.Face.Contract;
using System.Windows.Controls;
using Microsoft.ProjectOxford.Common.Contract;

namespace LiveCameraSample
{
    /**
         * TODO Hackathon: call this Method for the hybris connection
         * There are currently 6 different ads defined in Hybris: 
         * "MRO-M00-20" for males under 20
         * "MRO-M20-40" for males between 20 and 40
         * "MRO-M40-60" for males over 40
         * "MRO-F00-20" for females under 20
         * "MRO-F20-40" for females between 20 and 40
         * "MRO-F40-60" for females over 40
         * 
         * "MRO-M-RG--" Male with Reading Glasses
         * "MRO-F-RG--" Female with Reading Glasses
         * "MRO-M-SG--" Male with Sunglasses
         * "MRO-F-SG--" Female with Sunglasses
         * If you invoke the GetHybrisData("MRO-M20-40"), the method will return a String containing the URL 
         *    of an image of the ad for middle aged men.
         *    
         * "MRO-M-FH-B" facial hair bearded
         * 
         * "MRO-X-EM-A" Angry Face
         * "MRO-X-EM-S" Smiling Face
         * 
         */
    static class Advertizer
    {
        internal static void ShowHybrisAdvertizing(Microsoft.ProjectOxford.Face.Contract.FaceAttributes faceAttributes, System.Windows.Controls.Image rightImage)
        {
            //Hackathon: call this method with the information from Azure Face API, create a Request to Hybris and show the ad.
            String targetGroup = GetTargetGroup(faceAttributes.Age, faceAttributes.Gender, faceAttributes.Glasses, faceAttributes.FacialHair, faceAttributes.Emotion, faceAttributes.Smile);
            String hybrisResponseImageURL = HybrisRest.GetHybrisData(targetGroup);
            Console.WriteLine("Showing Ad from URL: " + hybrisResponseImageURL);
            ShowAd(hybrisResponseImageURL, rightImage);
        }

        internal static String GetTargetGroup(double exactAge, String gender, Microsoft.ProjectOxford.Face.Contract.Glasses glasses, FacialHair facialHair, EmotionScores emotion, double smile)
        {
            //Hackathon: prepare the Target-Group-String for the Hybris REST call: HybrisRest.GetHybrisData(Target-Group-String)
            String prefix = "MRO-";
            String genderSyllable = gender.ToLower().StartsWith("m") ? "M" : "F";
            String ageGroup = "";

            Console.WriteLine("Smile: " + smile);

            if (smile > 0.99)
            {
                return prefix + "X-EM-S";
            }

            Console.WriteLine("Anger: " + emotion.Anger);
            Console.WriteLine("Contempt: " + emotion.Contempt);
            Console.WriteLine("Disgust: " + emotion.Disgust);
            Console.WriteLine("Fear: " + emotion.Fear);
            Console.WriteLine("Happiness: " + emotion.Happiness);
            Console.WriteLine("Neutral: " + emotion.Neutral);
            Console.WriteLine("Sadness: " + emotion.Sadness);
            Console.WriteLine("Surprise: " + emotion.Surprise);

            if (emotion.Anger > 0.5)
            {
                return prefix + "X-EM-A";
            }

            if ((int)glasses == 1)
            {
                return prefix + genderSyllable + "-SG--";
            }
            else if ((int)glasses == 2)
            {
                return prefix + genderSyllable + "-RG--";
            }

            Console.WriteLine("Beard: " +  facialHair.Beard);
            Console.WriteLine("Moustache: " + facialHair.Moustache);
            Console.WriteLine("Sideburns: " + facialHair.Sideburns);
            if (genderSyllable.Equals("M") && facialHair.Beard > 0.5)
            {
                return prefix + genderSyllable + "-FH-B";
            }

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
                ageGroup = "40-60";
                Console.WriteLine("Error regarding Agegroup: " +  exactAge + " is bigger than 60");
                Console.WriteLine("Error regarding Agegroup: Changing to agegroup 40-60");
            }
            Console.WriteLine("Result of GetTargetGroup: ", prefix + genderSyllable + ageGroup);
            return prefix + genderSyllable + ageGroup;
        }

        internal static void ShowAd(String hybrisResponse, System.Windows.Controls.Image rightImage)
        {
            //Hackathon: show the ad in the user interface.
            Visualization.DrawAds(rightImage, new BitmapImage(new Uri(hybrisResponse)));
    }
    }
}
