namespace SharePointTraining.Spdev.Danila.SharePointSolution
{
    using System.Collections.Generic;

    using Microsoft.SharePoint.Administration;

    public class Logger : SPDiagnosticsServiceBase
    {
        public static string DiagnosticAreaName = "Danila's SharepointLog Service";
        private static Logger _current;
        public static Logger Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Logger();
                }

                return _current;
            }
        }

        public Logger() : base("Danila's SharepointLog Service", SPFarm.Local)
        {

        }

        public enum Category
        {
            Unexpected,
            High,
            Medium,
            Information
        }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            var areas = new List<SPDiagnosticsArea>
            {
                new SPDiagnosticsArea(DiagnosticAreaName, new List<SPDiagnosticsCategory>
                {
                    new SPDiagnosticsCategory("Unexpected", TraceSeverity.Unexpected, EventSeverity.Error),
                    new SPDiagnosticsCategory("High", TraceSeverity.High, EventSeverity.Warning),
                    new SPDiagnosticsCategory("Medium", TraceSeverity.Medium, EventSeverity.Information),
                    new SPDiagnosticsCategory("Information", TraceSeverity.Verbose, EventSeverity.Information)
                })
            };

            return areas;
        }

        public static void WriteLog(Category categoryName, string source, string errorMessage)
        {
            SPDiagnosticsCategory category = Logger.Current.Areas[DiagnosticAreaName].Categories[categoryName.ToString()];
            Logger.Current.WriteTrace(0, category, category.TraceSeverity, string.Concat(source, ": ", errorMessage));
        }
    }
}
