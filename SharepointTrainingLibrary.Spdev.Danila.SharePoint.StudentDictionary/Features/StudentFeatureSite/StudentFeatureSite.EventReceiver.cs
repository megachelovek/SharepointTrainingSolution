// Copyright © iSys.Spdev 2019 All rights reserved.

namespace iSys.Spdev.Danila.SharePoint.StudentDictionary.Features.StudentFeatureSite
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
    [Guid("817ac20e-86df-4ff8-8acd-3bcdd6c554d2")]
    public class StudentFeatureSiteEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var site = (SPSite) properties.Feature.Parent;
            var studentContentType = new StudentContentType(site.RootWeb);
            studentContentType.EnsureStudentContentType();
        }
    }
}