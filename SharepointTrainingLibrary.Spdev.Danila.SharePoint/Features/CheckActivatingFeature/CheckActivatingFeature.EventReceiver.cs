using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace SharePointTraining.Spdev.Danila.SharePointSolution.Features.CheckActivatingFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("09192857-0086-4d19-a237-e9b433b522fe")]
    public class CheckActivatingFeatureEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            Logger.WriteLog(Logger.Category.High, "Have a nice day)))", "NO Error message, just trying FeatureActivated");
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            Logger.WriteLog(Logger.Category.High, "Have a nice day)))", "NO Error message, just trying FeatureDeactivating");
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
            Logger.WriteLog(Logger.Category.High, "Have a nice day)))", "NO Error message, just trying FeatureInstalled");
        }


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
            Logger.WriteLog(Logger.Category.High, "Have a nice day)))", "NO Error message, just trying FeatureUninstalling");
        }

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        {
            Logger.WriteLog(Logger.Category.High, "Have a nice day)))", "NO Error message, just trying FeatureUpgrading");
        }
    }
}
