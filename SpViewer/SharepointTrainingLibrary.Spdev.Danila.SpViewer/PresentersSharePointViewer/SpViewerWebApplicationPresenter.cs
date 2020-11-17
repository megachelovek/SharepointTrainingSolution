//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для WebApplication сущности.
    /// </summary>
    public class SpViewerWebApplicationPresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел сущности SharePoint c подузлами.
        /// </summary>
        /// <param name="spViewerWebApplication"></param>
        /// <returns>Узел сущности с дочерними подузлами</returns>
        public TreeNode GetNodeWithChildren(SpViewerWebApplicationModel spViewerWebApplication,
            ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (spViewerWebApplication == null)
            {
                throw new ArgumentNullException(nameof(spViewerWebApplication));
            }

            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var spWebApplication = spViewerWebApplication.SharePointEntity as SPWebApplication;
            var treeNodeWebApplication = new TreeNode(spWebApplication.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, spViewerWebApplication);
            treeNodeWebApplication.Tag =
                spWebApplication.GetType() + Separator + spWebApplication.Id + Separator + this.g;
            treeNodeWebApplication = GetCategoryNode(treeNodeWebApplication, spViewerWebApplication);

            this.AddNodespSites(mainDictionary, spWebApplication, treeNodeWebApplication);

            this.AddNodespFeatures(mainDictionary, spWebApplication, treeNodeWebApplication);

            this.AddNodespContentDatabase(mainDictionary, spWebApplication, treeNodeWebApplication);

            return treeNodeWebApplication;
        }

        private void AddNodespContentDatabase(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPWebApplication spWebApplication,
            TreeNode treeNodeWebApplication)
        {
            foreach (SPContentDatabase spContentDatabase in spWebApplication.ContentDatabases)
            {
                var viewerContentDatabase = new SpViewerContentDatabaseModel(spContentDatabase);
                var treeNodeContentDatabase = new TreeNode(viewerContentDatabase.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerContentDatabase);
                treeNodeContentDatabase.Tag = spContentDatabase.GetType() + "|" + spContentDatabase.Id + "|" + this.g;
                treeNodeWebApplication.Nodes[2].Nodes.Add(treeNodeContentDatabase);
            }
        }

        private void AddNodespFeatures(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPWebApplication spWebApplication,
            TreeNode treeNodeWebApplication)
        {
            foreach (SPFeature spFeature in spWebApplication.Features)
            {
                var viewerFeature = new SpViewerFeatureModel(spFeature);
                var treeNodeFeature = new TreeNode(viewerFeature.Name);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerFeature);
                treeNodeFeature.Tag = spFeature.GetType() + "|" + spFeature.DefinitionId + "|" + this.g;
                treeNodeWebApplication.Nodes[1].Nodes.Add(treeNodeFeature);
            }
        }

        private void AddNodespSites(Dictionary<Guid, IModelSharePoint> mainDictionary,
            SPWebApplication spWebApplication,
            TreeNode treeNodeWebApplication)
        {
            foreach (SPSite spSite in spWebApplication.Sites)
            {
                var viewerSite = new SpViewerSiteModel(spSite);
                var treeSite = new TreeNode(spSite.Url);
                this.g = Guid.NewGuid();
                mainDictionary.Add(this.g, viewerSite);
                treeSite.Tag = spSite.GetType() + "|" + spSite.ID + "|" + this.g;
                treeNodeWebApplication.Nodes[0].Nodes.Add(treeSite);
            }
        }
    }
}