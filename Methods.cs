using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazNNBot
{
    public static class Methods
    {
        public static string PathToAppFiles { get; set; }
        public static string PathToUpdates { get; set; }
        public static string PathToUsers { get; set; }
        public static string PathToAnkets { get; set; }
        public static string PathToAnketaUserProgress { get; set; }
        public static string PathToQuestions { get; set; }
        public static string PathToQuestionItems { get; set; }
        public static string PathToAnswerChoices { get; set; }
        public static string PathToAnswerTexts { get; set; }
        public static string PathToSendAnketsToUsers { get; set; }
        public static string PathToAppeals { get; set; }
        public static string PathToAppealSigns { get; set; }
        public static string PathToSendAppealsToUser { get; set; }

        public static bool CheckAndUpdateDb(List<string> currentCols, Type needType)
        {
            List<string> needCols = needType.GetProperties().Select(p => p.Name).ToList();
            var firstNotSecond = currentCols.Except(needCols).ToList();
            var secondNotFirst = needCols.Except(currentCols).ToList();
            if (!firstNotSecond.Any() && !secondNotFirst.Any())
            {
                return true;
            }
            return false;
        }
        public static void SaveDbSqlLite<T>(IEnumerable<T> list, string path, string name)
        {
            string fullName = name + ".db";
            if (File.Exists(Path.Combine(path, fullName)))
            {
                File.Delete(Path.Combine(path, fullName));
            }
            var conn = new SQLiteConnection(Path.Combine(path, fullName));
            conn.CreateTable<T>();
            if (list.Count() > 0)
            {
                conn.InsertAll(list.ToArray());
            }
            conn.Close();
        }
    }
}
