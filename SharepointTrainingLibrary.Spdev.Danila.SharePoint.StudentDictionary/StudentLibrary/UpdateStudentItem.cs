//  SharePointTraining.Spdev 2019

namespace SharePointTraining.Spdev.Danila.SharePoint.StudentDictionary.StudentLibrary
{
    using Microsoft.SharePoint;

    internal static class UpdateStudentItem
    {
        /// <summary>
        ///     Обновление поля результата с подсчетом среднего значения
        /// </summary>
        /// <param name="spItem"></param>
        public static void ItemUpdateAverageField(SPItem spItem)
        {
            int perseverance, codeQuality, skills;
            int.TryParse(spItem[Student.Perseverance.Title].ToString(), out perseverance);
            int.TryParse(spItem[Student.CodeQuality.Title].ToString(), out codeQuality);
            int.TryParse(spItem[Student.Skills.Title].ToString(), out skills);
            spItem[Student.Result.Title] = Average(perseverance, codeQuality, skills);
            spItem[Student.StudentName.Title] =
                Average(perseverance, codeQuality, skills) + Student.StudentName.Title;
            spItem.Update();
        }

        /// <summary>
        ///     Среднее арифметическое
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double Average(params int[] data)
        {
            double result = 0;
            foreach (int value in data)
            {
                result += value;
            }

            return result / data.Length;
        }
    }
}