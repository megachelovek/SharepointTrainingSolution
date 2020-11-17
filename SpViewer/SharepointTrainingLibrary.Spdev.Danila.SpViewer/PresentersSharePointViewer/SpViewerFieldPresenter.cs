//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Field сущности.
    /// </summary>
    public class SpViewerFieldPresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerContentTypeModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerFieldModel spViewerFieldModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerFieldModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerFieldModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerField = spViewerFieldModel.SharePointEntity as SPField;
            var treeNodeField = new TreeNode(spViewerFieldModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerFieldModel);
            treeNodeField.Tag = spViewerFieldModel.GetType() + Separator + viewerField.Id + Separator + this.g;
            treeNodeField = GetCategoryNode(treeNodeField, spViewerFieldModel);
            return treeNodeField;
        }
    }
}