//  SharePointTraining.Spdev 2019

namespace SharePointTraining.Spdev.Danila.SharePoint.StudentDictionary.StudentLibrary
{
    using Microsoft.SharePoint;

    public sealed class StudentReceivers : SPItemEventReceiver
    {
        /// <summary>
        ///     Ресивер среднего значения и запись в поле
        /// </summary>
        /// <param name="properties"></param>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            UpdateStudentItem.ItemUpdateAverageField(properties.ListItem);
            this.EventFiringEnabled = true;
        }
    }
}