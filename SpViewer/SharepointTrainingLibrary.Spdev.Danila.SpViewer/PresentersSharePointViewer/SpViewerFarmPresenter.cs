//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.SharePoint.Administration;

    using static HierarchicalResult;

    /// <summary>
    ///     Presenter для Farm сущности.
    /// </summary>
    public class SpViewerFarmPresenter
    {
        private const string Separator = "|";

        private Guid g;

        /// <summary>
        ///     Возвращает узел фермы SharePoint c подузлами.
        /// </summary>
        /// <param name="spFarm">Ферма.</param>
        /// <returns>Узел фермы с подузлами</returns>
        public TreeNode GetFarmTreeNode(ref Dictionary<Guid, IModelSharePoint> mainDictionary)
        {
            if (mainDictionary == null)
            {
                throw new ArgumentNullException(nameof(mainDictionary));
            }

            var viewerFarm = new SpViewerFarmModel();
            var spFarm = viewerFarm.SharePointEntity as SPFarm;
            var treeNodeFarm = new TreeNode(viewerFarm.Name);
            this.g = Guid.NewGuid();
            mainDictionary.Add(this.g, viewerFarm);
            treeNodeFarm.Tag = spFarm.GetType() + Separator + spFarm.Id + Separator + this.g;
            treeNodeFarm = GetCategoryNode(treeNodeFarm, viewerFarm);
            var service = spFarm.Services.GetValue<SPWebService>(string.Empty);
            this.AddNodespWebApplication(mainDictionary, service, treeNodeFarm);

            this.AddNodespService(mainDictionary, spFarm, treeNodeFarm);

            this.AddNodespServer(mainDictionary, spFarm, treeNodeFarm);

            return treeNodeFarm;
        }

        private void AddNodespServer(Dictionary<Guid, IModelSharePoint> mainDictionary, SPFarm spFarm,
            TreeNode treeNodeFarm)
        {
            foreach (SPServer spServer in spFarm.Servers)
            {
                var viewerServer = new SpViewerServerModel(spServer);
                var treeNodeServer = new TreeNode(spServer.Name);
                this.g = Guid.NewGuid();
                treeNodeServer.Tag = spServer.GetType() + Separator + spServer.Id + Separator + this.g;
                mainDictionary.Add(this.g, viewerServer);
                treeNodeFarm.Nodes[2].Nodes.Add(treeNodeServer);
            }
        }

        private void AddNodespService(Dictionary<Guid, IModelSharePoint> mainDictionary, SPFarm spFarm,
            TreeNode treeNodeFarm)
        {
            foreach (SPService spService in spFarm.Services)
            {
                var viewerService = new SpViewerServiceModel(spService);
                var treeNodeService = new TreeNode(spService.Name == "" ? spService.TypeName : spService.DisplayName);
                this.g = Guid.NewGuid();
                treeNodeService.Tag = SpEntityTypeName.TYPE_SERVICE + Separator + spService.Id +
                                      Separator + this.g;
                mainDictionary.Add(this.g, viewerService);
                treeNodeFarm.Nodes[1].Nodes.Add(treeNodeService);
            }
        }

        private void AddNodespWebApplication(Dictionary<Guid, IModelSharePoint> mainDictionary, SPWebService service,
            TreeNode treeNodeFarm)
        {
            foreach (SPWebApplication spWebApplication in service.WebApplications)
            {
                var viewerWebApplication = new SpViewerWebApplicationModel(spWebApplication);
                var treeNodeWebApplication = new TreeNode(spWebApplication.Name);
                this.g = Guid.NewGuid();
                treeNodeWebApplication.Tag =
                    spWebApplication.GetType() + Separator + spWebApplication.Id + Separator + this.g;
                mainDictionary.Add(this.g, viewerWebApplication);
                treeNodeFarm.Nodes[0].Nodes.Add(treeNodeWebApplication);
            }
        }
    }
}