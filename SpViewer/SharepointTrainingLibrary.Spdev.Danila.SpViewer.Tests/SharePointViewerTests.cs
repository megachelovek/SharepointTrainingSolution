//  SharePointTraining.Spdev 2018

namespace SharePointTraining.Spdev.Danila.SpViewer.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Spviewer;

    [TestClass]
    public class SharePointViewerTests
    {
        [TestMethod]
        public void GetFarmNodeFromPresenter()
        {
            var dictionary = new Dictionary<Guid, IModelSharePoint>();
            var presenter = new SpViewerFarmPresenter();
            TreeNode treeFarmNode = presenter.GetFarmTreeNode(ref dictionary);
            Assert.IsNotNull(treeFarmNode);
        }
    }
}