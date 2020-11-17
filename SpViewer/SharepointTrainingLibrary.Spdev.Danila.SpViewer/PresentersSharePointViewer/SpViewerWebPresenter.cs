//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Web сущности.
    /// </summary>
    public class SpViewerWebPresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerWebModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerWebModel spViewerWebModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerWebModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerWebModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var spWeb = spViewerWebModel.SharePointEntity as SPWeb;
            var treeNodeWeb = new TreeNode(spViewerWebModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerWebModel);
            treeNodeWeb.Tag = spViewerWebModel.GetType() + Separator + spWeb.ID + Separator + this.g;
            treeNodeWeb = GetCategoryNode(treeNodeWeb, spViewerWebModel);
            this.AddNodespLists(mainDictionary, spWeb, treeNodeWeb);

            this.AddNodespRoleAssignment(mainDictionary, spWeb, treeNodeWeb);

            AddNodespPrincipal(ref mainDictionary, spWeb, treeNodeWeb);


            return treeNodeWeb;
        }

        private static void AddNodespPrincipal(ref Dictionary<Guid, IModelSharePoint> mainDictionary, SPWeb spWeb,
            TreeNode treeNodeWeb)
        {
            var presenterPrincipal = new SpViewerPrincipalPresenter();
            TreeNodeCollection treecollection =
                presenterPrincipal.GetNodeWithChildren(new SpViewerPrincipalModel(spWeb.AllUsers[0]),
                    ref mainDictionary);
            foreach (TreeNode node in treecollection)
            {
                treeNodeWeb.Nodes[2].Nodes.Add(node);
            }
        }

        private void AddNodespRoleAssignment(Dictionary<Guid, IModelSharePoint> mainDictionary, SPWeb spWeb,
            TreeNode treeNodeWeb)
        {
            foreach (SPRoleAssignment spRoleAssignment in spWeb.RoleAssignments)
            {
                var viewerRoleAssignment = new SpViewerRoleModel(spRoleAssignment);
                var treeNodeRoleAssignment = new TreeNode(viewerRoleAssignment.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerRoleAssignment);
                treeNodeRoleAssignment.Tag = viewerRoleAssignment.GetType() + "|" +
                                             spRoleAssignment.ParentSecurableObject + "|" + this.g;
                treeNodeWeb.Nodes[1].Nodes.Add(treeNodeRoleAssignment);
            }
        }

        private void AddNodespLists(Dictionary<Guid, IModelSharePoint> mainDictionary, SPWeb spWeb,
            TreeNode treeNodeWeb)
        {
            foreach (SPList spList in spWeb.Lists)
            {
                var viewerList = new SpViewerListModel(spList);
                var treeList = new TreeNode(spList.Title);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerList);
                treeList.Tag = spList.GetType() + "|" + spList.ID + "|" + this.g;
                treeNodeWeb.Nodes[0].Nodes.Add(treeList);
            }
        }
    }
}