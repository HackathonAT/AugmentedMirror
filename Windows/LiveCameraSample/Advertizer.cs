using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveCameraSample
{
    static class Advertizer
    {
        internal static void ShowHybrisAdvertizing(Microsoft.ProjectOxford.Face.Contract.FaceAttributes faceAttributes)
        {
            //TODO Hackathon: call this method with the information from Azure Face API, create a Request to Hybris and show the ad.
        }

        internal static String getTargetGroup(double exactAge, String gender)
        {
            //TODO Hackathon: prepare the Target-Group-String for the Hybris REST call: HybrisRest.GetHybrisData(Target-Group-String)
            return "";
        }

        internal static void showAd(String hybrisResponse)
        {
            //TODO Hackathon: show the ad in the user interface.
        }
    }
}
