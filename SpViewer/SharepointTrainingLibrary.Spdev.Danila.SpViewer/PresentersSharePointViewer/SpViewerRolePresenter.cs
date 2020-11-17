//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Role сущности.
    /// </summary>
    public class SpViewerRolePresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerRoleModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerRoleModel spViewerRoleModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerRoleModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerRoleModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerRole = spViewerRoleModel.SharePointEntity as SPRoleAssignment;
            var treeNodeRole = new TreeNode(spViewerRoleModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerRoleModel);
            treeNodeRole.Tag = spViewerRoleModel.GetType() + Separator + viewerRole.ParentSecurableObject + Separator +
                               this.g;
            treeNodeRole = GetCategoryNode(treeNodeRole, spViewerRoleModel);
            return treeNodeRole;
        }
    }
}