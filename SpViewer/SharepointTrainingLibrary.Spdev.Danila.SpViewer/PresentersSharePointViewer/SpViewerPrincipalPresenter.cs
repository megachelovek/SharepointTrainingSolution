//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Principal сущности.
    /// </summary>
    public class SpViewerPrincipalPresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerPrincipalModel"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNodeCollection GetNodeWithChildren(SpViewerPrincipalModel spViewerPrincipalModel,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerPrincipalModel == null)
            {
                throw new ArgumentNullException(nameof(spViewerPrincipalModel));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var spPrincipalMain = spViewerPrincipalModel.SharePointEntity as SPPrincipal;
            var treeNodePrincipal = new TreeNode(spViewerPrincipalModel.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerPrincipalModel);
            treeNodePrincipal.Tag =
                spViewerPrincipalModel.GetType() + Separator + spPrincipalMain.ID + Separator + this.g;
            treeNodePrincipal = GetCategoryNode(treeNodePrincipal, spViewerPrincipalModel);

            AddNodespSiteAdministrators(mainDictionary, spPrincipalMain, treeNodePrincipal);

            AddNodespSiteUsers(mainDictionary, spPrincipalMain, treeNodePrincipal);

            AddNodespUsers(mainDictionary, spPrincipalMain, treeNodePrincipal);

            AddNodespAllUsers(mainDictionary, spPrincipalMain, treeNodePrincipal);

            AddNodespSiteGroups(mainDictionary, spPrincipalMain, treeNodePrincipal);

            AddNodespGroups(mainDictionary, spPrincipalMain, treeNodePrincipal);

            return treeNodePrincipal.Nodes;
        }

        private static void AddNodespGroups(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPPrincipal spPrincipalMain, TreeNode treeNodePrincipal)
        {
            foreach (SPGroup spGroup in
                spPrincipalMain.ParentWeb.Groups)
            {
                var viewerPrincipal = new SpViewerPrincipalModel(spGroup);
                Guid g = Guid.NewGuid();
                mainDictionary.Add(g, viewerPrincipal);
                var newNode = new TreeNode(spGroup.Name);
                newNode.Tag = spGroup.GetType() + "|" + spGroup.ID + "|" + g;
                treeNodePrincipal.Nodes[5].Nodes.Add(newNode);
            }
        }

        private static void AddNodespSiteGroups(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPPrincipal spPrincipalMain,
            TreeNode treeNodePrincipal)
        {
            foreach (SPGroup spGroup in
                spPrincipalMain.ParentWeb.SiteGroups)
            {
                var viewerPrincipal = new SpViewerPrincipalModel(spGroup);
                Guid g = Guid.NewGuid();
                mainDictionary.Add(g, viewerPrincipal);
                var newNode = new TreeNode(spGroup.Name);
                newNode.Tag = spGroup.GetType() + "|" + spGroup.ID + "|" + g;
                treeNodePrincipal.Nodes[4].Nodes.Add(newNode);
            }
        }

        private static void AddNodespAllUsers(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPPrincipal spPrincipalMain,
            TreeNode treeNodePrincipal)
        {
            foreach (SPPrincipal spPrincipal in
                spPrincipalMain.ParentWeb.AllUsers)
            {
                var viewerPrincipal = new SpViewerPrincipalModel(spPrincipal);
                Guid g = Guid.NewGuid();
                mainDictionary.Add(g, viewerPrincipal);
                var newNode = new TreeNode(spPrincipal.Name);
                newNode.Tag = spPrincipal.GetType() + "|" + spPrincipal.ID + "|" + g;
                treeNodePrincipal.Nodes[3].Nodes.Add(newNode);
            }
        }

        private static void AddNodespUsers(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPPrincipal spPrincipalMain, TreeNode treeNodePrincipal)
        {
            foreach (SPPrincipal spPrincipal in
                spPrincipalMain.ParentWeb.Users)
            {
                var viewerPrincipal = new SpViewerPrincipalModel(spPrincipal);
                Guid g = Guid.NewGuid();
                mainDictionary.Add(g, viewerPrincipal);
                var newNode = new TreeNode(spPrincipal.Name);
                newNode.Tag = spPrincipal.GetType() + "|" + spPrincipal.ID + "|" + g;
                treeNodePrincipal.Nodes[2].Nodes.Add(newNode);
            }
        }

        private static void AddNodespSiteUsers(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPPrincipal spPrincipalMain,
            TreeNode treeNodePrincipal)
        {
            foreach (SPPrincipal spPrincipal in
                spPrincipalMain.ParentWeb.SiteUsers)
            {
                var viewerPrincipal = new SpViewerPrincipalModel(spPrincipal);
                Guid g = Guid.NewGuid();
                mainDictionary.Add(g, viewerPrincipal);
                var newNode = new TreeNode(spPrincipal.Name);
                newNode.Tag = spPrincipal.GetType() + "|" + spPrincipal.ID + "|" + g;
                treeNodePrincipal.Nodes[1].Nodes.Add(newNode);
            }
        }

        private static void AddNodespSiteAdministrators(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPPrincipal spPrincipalMain,
            TreeNode treeNodePrincipal)
        {
            foreach (SPPrincipal spPrincipal in
                spPrincipalMain.ParentWeb.SiteAdministrators)
            {
                var viewerPrincipal = new SpViewerPrincipalModel(spPrincipal);
                Guid g = Guid.NewGuid();
                mainDictionary.Add(g, viewerPrincipal);
                var newNode = new TreeNode(spPrincipal.Name);
                newNode.Tag = spPrincipal.GetType() + "|" + spPrincipal.ID + "|" + g;
                treeNodePrincipal.Nodes[0].Nodes.Add(newNode);
            }
        }
    }
}