// Copyright © iSys.Spdev 2019 All rights reserved.

namespace iSys.Spdev.Danila.SharePoint.StudentDictionary.Features.Feature1
{
    using System.Runtime.InteropServices;

    using Microsoft.SharePoint;

    using StudentLibrary;

    /// <summary>
    ///     This class handles events raised during feature activation, deactivation, installation, uninstallation, and
    ///     upgrade.
    /// </summary>
    /// <remarks>
    ///     The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>
    [Guid("1d5bbcaa-55d6-4dd3-ba2e-224ee5f15b08")]
    public class StudentEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var web = (SPWeb) properties.Feature.Parent;
            var studentList = new StudentList(web);
            studentList.Provision();
        }
    }
}