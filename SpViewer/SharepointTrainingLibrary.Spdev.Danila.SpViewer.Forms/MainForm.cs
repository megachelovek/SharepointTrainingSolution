//  SharePointTraining.Spdev 2018

namespace SharePointParametres
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using SharePointTraining.Spdev.Danila.Spviewer;

    public partial class MainForm : Form
    {
        private const char Separator = '|';

        public Dictionary<Guid, IModelSharePoint> MainDictionary = new Dictionary<Guid, IModelSharePoint>();

        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        ///     Загрузка и заполнение узлов дерева навигации по объектам SharePoint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MainListView.Columns.Add("Параметр", 200);
            this.MainListView.Columns.Add("Значение", 690);
            this.MainListView.Columns.Add("Класс", 250);
            var farmPresenter = new SpViewerFarmPresenter();
            this.mainTreeView.Nodes.Add(farmPresenter.GetFarmTreeNode(ref this.MainDictionary));
        }

        /// <summary>
        ///     Обработка нажатия на узел дерева и вывод значений параметров объекта SharePoint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = new TreeNode();
            TreeViewHitTestInfo hit = this.mainTreeView.HitTest(e.Location);
            if (hit.Location != TreeViewHitTestLocations.PlusMinus)
            {
                this.MainListView.Items.Clear();
                if (e.Node.Tag != null)
                {
                    List<Tuple<string, string, string>> data =
                        HierarchicalResult.GetParametresFromObject(e.Node, this.MainDictionary);
                    this.MainListView.View = View.Details;
                    if (data.Count != 0)
                    {
                        foreach (Tuple<string, string, string> tuple in data)
                        {
                            this.MainListView.Items.Add(new ListViewItem(new[]
                                {tuple.Item1, tuple.Item2, tuple.Item3}));
                        }
                    }

                    if (e.Node.Tag.ToString().Split(Separator)[0] != "Microsoft.SharePoint.Administration.SPFarm")
                    {
                        var temp = (IModelSharePoint) HierarchicalResult.GetObjectFromDictionary(e.Node,
                            this.MainDictionary);
                        if (temp.CategoryDictionary.Count != 0)
                        {
                            node = CreateChildrenNode.CreateNode(temp, ref this.MainDictionary);
                            if (node != null)
                            {
                                HierarchicalResult.ReplaceNode(e.Node, node);
                                node.Expand();
                                node.BackColor = Color.LightGoldenrodYellow;
                            }
                        }
                    }
                }
            }
        }
    }
}