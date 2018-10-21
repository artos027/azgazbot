using Microsoft.Owin.Hosting;
using Owin;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace GazNNBot
{
    public static class Bot
    {
        public static readonly TelegramBotClient Api = new TelegramBotClient("598668740:AAHZKKLeh_vb1cjyLtpq2l5RvhsOJNUBI8A");
    }

    public static class Program
    {
        public static Timer sendTimer = new Timer();

        static void Main(string[] args)
        {
            sendTimer.Interval = 5000;
            sendTimer.AutoReset = true;
            sendTimer.Elapsed += SendTimer_Elapsed;
            sendTimer.Start();

            Methods.PathToAppFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "GazBotFiles");
            Methods.PathToUsers = Path.Combine(Methods.PathToAppFiles, "users.db");
            Methods.PathToUpdates = Path.Combine(Methods.PathToAppFiles, "updates.db");
            Methods.PathToAnkets = Path.Combine(Methods.PathToAppFiles, "ankets.db");
            Methods.PathToAnketaUserProgress = Path.Combine(Methods.PathToAppFiles, "anketaUserProgress.db");
            Methods.PathToQuestions = Path.Combine(Methods.PathToAppFiles, "questions.db");
            Methods.PathToQuestionItems = Path.Combine(Methods.PathToAppFiles, "questionItems.db");
            Methods.PathToAnswerChoices = Path.Combine(Methods.PathToAppFiles, "answerChoices.db");
            Methods.PathToAnswerTexts = Path.Combine(Methods.PathToAppFiles, "answerTexts.db");
            Methods.PathToSendAnketsToUsers = Path.Combine(Methods.PathToAppFiles, "sendAnkets.db");
            Methods.PathToAppeals = Path.Combine(Methods.PathToAppFiles, "appeals.db");
            Methods.PathToAppealSigns = Path.Combine(Methods.PathToAppFiles, "appealSigns.db");
            Methods.PathToSendAppealsToUser = Path.Combine(Methods.PathToAppFiles, "sendAppeals.db");

            if (!Directory.Exists(Methods.PathToAppFiles))
            {
                Directory.CreateDirectory(Methods.PathToAppFiles);
            }
            if (!System.IO.File.Exists(Methods.PathToUpdates))
            {
                Methods.SaveDbSqlLite(new List<UpdateMessage>(), Methods.PathToAppFiles, "updates");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToUpdates);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("UpdateMessage").Select(p => p.Name).ToList(), typeof(UpdateMessage)) == false)
                {
                    conn.CreateTable<UpdateMessage>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToUsers))
            {
                Methods.SaveDbSqlLite(new List<User>(), Methods.PathToAppFiles, "users");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToUsers);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("User").Select(p => p.Name).ToList(), typeof(User)) == false)
                {
                    conn.CreateTable<User>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToAnkets))
            {
                Methods.SaveDbSqlLite(new List<Anketa>(), Methods.PathToAppFiles, "ankets");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToAnkets);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("Anketa").Select(p => p.Name).ToList(), typeof(Anketa)) == false)
                {
                    conn.CreateTable<Anketa>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToAnketaUserProgress))
            {
                Methods.SaveDbSqlLite(new List<AnketaUserProgress>(), Methods.PathToAppFiles, "anketaUserProgress");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToAnketaUserProgress);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("AnketaUserProgress").Select(p => p.Name).ToList(), typeof(AnketaUserProgress)) == false)
                {
                    conn.CreateTable<AnketaUserProgress>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToQuestions))
            {
                Methods.SaveDbSqlLite(new List<Question>(), Methods.PathToAppFiles, "questions");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToQuestions);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("Question").Select(p => p.Name).ToList(), typeof(Question)) == false)
                {
                    conn.CreateTable<Question>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToQuestionItems))
            {
                Methods.SaveDbSqlLite(new List<QuestionItem>(), Methods.PathToAppFiles, "questionItems");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToQuestionItems);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("QuestionItem").Select(p => p.Name).ToList(), typeof(QuestionItem)) == false)
                {
                    conn.CreateTable<QuestionItem>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToAnswerChoices))
            {
                Methods.SaveDbSqlLite(new List<AnswerChoice>(), Methods.PathToAppFiles, "answerChoices");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToAnswerChoices);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("AnswerChoice").Select(p => p.Name).ToList(), typeof(AnswerChoice)) == false)
                {
                    conn.CreateTable<AnswerChoice>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToAnswerTexts))
            {
                Methods.SaveDbSqlLite(new List<AnswerText>(), Methods.PathToAppFiles, "answerTexts");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToAnswerTexts);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("AnswerText").Select(p => p.Name).ToList(), typeof(AnswerText)) == false)
                {
                    conn.CreateTable<AnswerText>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToSendAnketsToUsers))
            {
                Methods.SaveDbSqlLite(new List<SendAnketsToUser>(), Methods.PathToAppFiles, "sendAnkets");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToSendAnketsToUsers);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("SendAnketsToUser").Select(p => p.Name).ToList(), typeof(SendAnketsToUser)) == false)
                {
                    conn.CreateTable<SendAnketsToUser>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToSendAnketsToUsers))
            {
                Methods.SaveDbSqlLite(new List<SendAnketsToUser>(), Methods.PathToAppFiles, "sendAnkets");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToSendAnketsToUsers);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("SendAnketsToUser").Select(p => p.Name).ToList(), typeof(SendAnketsToUser)) == false)
                {
                    conn.CreateTable<SendAnketsToUser>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToAppeals))
            {
                Methods.SaveDbSqlLite(new List<Appeal>(), Methods.PathToAppFiles, "appeals");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToAppeals);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("Appeal").Select(p => p.Name).ToList(), typeof(Appeal)) == false)
                {
                    conn.CreateTable<Appeal>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToAppealSigns))
            {
                Methods.SaveDbSqlLite(new List<AppealSigns>(), Methods.PathToAppFiles, "appealSigns");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToAppealSigns);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("AppealSigns").Select(p => p.Name).ToList(), typeof(AppealSigns)) == false)
                {
                    conn.CreateTable<AppealSigns>();
                }
                conn.Close();
            }
            if (!System.IO.File.Exists(Methods.PathToSendAppealsToUser))
            {
                Methods.SaveDbSqlLite(new List<SendAppealsToUser>(), Methods.PathToAppFiles, "sendAppeals");
            }
            else
            {
                var conn = new SQLiteConnection(Methods.PathToSendAppealsToUser);
                if (Methods.CheckAndUpdateDb(conn.GetTableInfo("SendAppealsToUser").Select(p => p.Name).ToList(), typeof(SendAppealsToUser)) == false)
                {
                    conn.CreateTable<SendAppealsToUser>();
                }
                conn.Close();
            }

            using (WebApp.Start<Startup>("http://194.58.69.80:8445"))
            {
                Bot.Api.SetWebhookAsync("https://tradebotnow.com/gazbot/WebHook").Wait();
                //Bot.Api.OnReceiveError += Api_OnReceiveError;
                //Bot.Api.OnReceiveGeneralError += Api_OnReceiveGeneralError;
                //Bot.Api

                Console.WriteLine("Старт работы телеграмма!");

                Console.ReadLine();
            }
        }

        private static async Task SendNewAppeal(ChatId chatId, string text, string appealId)
        {
            try
            {
                var inlines = new List<InlineKeyboardButton[]>();
                var connAppealSigns = new SQLiteConnection(Methods.PathToAppealSigns);
                bool isSigned = false;
                long chat = chatId.Identifier;
                var userSign = connAppealSigns.Table<AppealSigns>().FirstOrDefault(p => p.ChatId == chat && p.AppealID == appealId);
                int countSigns = connAppealSigns.Table<AppealSigns>().Count(p => p.AppealID == appealId);
                if (userSign != null)
                {
                    isSigned = true;
                }
                connAppealSigns.Close();
                if (isSigned == false)
                {
                    inlines.Add(new[]
                          {
                                                InlineKeyboardButton.WithCallbackData("⚡️Подписать обращение", "signAppeal:" + appealId)
                                            });
                }
                var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" + text + "\n" +
                    "Количество подписей: " + countSigns, ParseMode.Html, true, replyMarkup: inlineKeyboard);
            }
            catch (Exception e)
            {

            }
        }
        private async static void SendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var connUser = new SQLiteConnection(Methods.PathToUsers);
                var connSendAppeals = new SQLiteConnection(Methods.PathToSendAppealsToUser);
                var connAppeals = new SQLiteConnection(Methods.PathToAppeals);

                var sendNewAppeals = connSendAppeals.Table<SendAppealsToUser>().Where(p => p.IsNew == true).ToList();
                if (sendNewAppeals.Count > 0)
                {
                    int needCount = sendNewAppeals.Count;
                    if (sendNewAppeals.Count > 30)
                    {
                        needCount = 30;
                    }
                    for (int i = 0; i < needCount; i++)
                    {
                        string appealId = sendNewAppeals[i].AppealID;
                        var findAppeal = connAppeals.Table<Appeal>().FirstOrDefault(p => p.ID == appealId);
                        if (findAppeal != null)
                        {
                            await SendNewAppeal(sendNewAppeals[i].ChatId, findAppeal.Text, appealId);
                            sendNewAppeals[i].IsNew = false;
                            connSendAppeals.Update(sendNewAppeals[i]);
                        }
                    }
                }
                connSendAppeals.Close();
                connAppeals.Close();
                connUser.Close();
            }
            catch(Exception ex)
            {

            }
        }

        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                var configuration = new HttpConfiguration();

                configuration.Routes.MapHttpRoute("WebHook", "{controller}");

                app.UseWebApi(configuration);
            }
        }
        public class WebHookController : ApiController
        {
            public async Task<IHttpActionResult> Post(Update update)
            {
                if (update.Type == UpdateType.Message)
                {
                    var db = new SQLiteConnection(Methods.PathToUpdates);
                    var needUpdate = db.Table<UpdateMessage>().FirstOrDefault(p => p.UpdateId == update.Id);
                    var message = update.Message;
                    DateTime today = DateTime.Today;
                    if (needUpdate == null && message != null)
                    {
                        db.Insert(new UpdateMessage { UpdateId = update.Id });
                        db.Close();

                        long chatId = message.Chat.Id;
                        var connUser = new SQLiteConnection(Methods.PathToUsers);
                        var currentUser = connUser.Table<User>().FirstOrDefault(p => p.ChatId == chatId);
                        if (currentUser == null)
                        {
                            var newUser = new User();
                            newUser.ChatId = chatId;
                            newUser.IsRegistered = false;
                            connUser.Insert(newUser);
                            currentUser = newUser;
                        }
                        if (message.Type == MessageType.Text)
                        {
                            if (message.Text.ToLower().StartsWith("/start"))
                            {
                                if (currentUser.IsRegistered == false)
                                {
                                    var inlines = new List<InlineKeyboardButton[]>();
                                    inlines.Add(new[]
                                          {
                                                InlineKeyboardButton.WithCallbackData("🔑Регистрация", "register")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                    await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "Добро пожаловать, для того чтобы продолжить нужно пройти простую регистрацию, указав " +
                                        "Код подразделения и табельный номер, чтобы мы знали что ты в команде Газа.\n",
                                        ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                }
                                else
                                {
                                    var inlines = new List<InlineKeyboardButton[]>();
                                    inlines.Add(new[]
                                          {
                                                InlineKeyboardButton.WithCallbackData("📝Пройти анкетирование", "getAnkets"),
                                            });
                                    inlines.Add(new[]
                                          {
                                                InlineKeyboardButton.WithCallbackData("✉️Создать обращение", "appeal")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                    await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "Ваш ID  в системе: " + chatId + "\n" +
                                        "Код подразделения: " + currentUser.Subdivision + "\n" +
                                        "Табельный номер: " + currentUser.PersonNumber,
                                        ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                }
                            }
                            else if (message.Text.ToLower().StartsWith("/home"))
                            {
                                if (currentUser.IsRegistered == false)
                                {
                                    var inlines = new List<InlineKeyboardButton[]>();
                                    inlines.Add(new[]
                                          {
                                                InlineKeyboardButton.WithCallbackData("🔑Регистрация", "register")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                    await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "Чтобы продолжить работу нужно пройти простую регистрацию, указав " +
                                        "Код подразделения и табельный номер, чтобы мы знали что ты в команде Газа.\n",
                                        ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                }
                                else
                                {
                                    var inlines = new List<InlineKeyboardButton[]>();
                                    inlines.Add(new[]
                                         {
                                                InlineKeyboardButton.WithCallbackData("📝Пройти анкетирование", "getAnkets"),
                                            });
                                    inlines.Add(new[]
                                          {
                                                InlineKeyboardButton.WithCallbackData("✉️Создать обращение", "appeal")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                    await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "Ваш ID  в системе: " + chatId + "\n" +
                                        "Код подразделения: " + currentUser.Subdivision + "\n" +
                                        "Табельный номер: " + currentUser.PersonNumber,
                                        ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                }
                            }
                            else if (message.Text.ToLower().StartsWith("/ankets"))
                            {
                                if (currentUser.IsRegistered == false)
                                {
                                    var inlines = new List<InlineKeyboardButton[]>();
                                    inlines.Add(new[]
                                          {
                                                InlineKeyboardButton.WithCallbackData("🔑Регистрация", "register")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                    await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "Чтобы продолжить работу нужно пройти простую регистрацию, указав " +
                                        "Код подразделения и табельный номер, чтобы мы знали что ты в команде Газа.\n",
                                        ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                }
                                else
                                {
                                    var connAnkets = new SQLiteConnection(Methods.PathToAnkets);
                                    var currentAnkets = connAnkets.Table<Anketa>().Where(p => p.IsActive == true).ToList();
                                    currentAnkets = currentAnkets.OrderByDescending(p => p.Date).ToList();
                                    connAnkets.Close();

                                    if (currentAnkets.Count > 0)
                                    {
                                        var inlines = new List<InlineKeyboardButton[]>();
                                        foreach (var anketa in currentAnkets)
                                        {
                                            string idAnket = anketa.ID;

                                            string isFinished = string.Empty;
                                            var connAnketProgress = new SQLiteConnection(Methods.PathToAnketaUserProgress);
                                            var userProgress = connAnketProgress.Table<AnketaUserProgress>().FirstOrDefault(p => p.AnketaId == idAnket);
                                            connAnketProgress.Close();
                                            if (userProgress != null && userProgress.IsSend == true)
                                            {
                                                isFinished += "✅";
                                            }
                                            inlines.Add(new[]
                                            {
                                                InlineKeyboardButton.WithCallbackData(isFinished + anketa.Name, "showAnket:" + idAnket)
                                            });
                                        }
                                        inlines.Add(new[]
                                           {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                                        var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                        await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                            "📝Текущие анкеты:",
                                            ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                    }
                                    else
                                    {
                                        await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                       "Пока активных анкет для прохождения нет...скоро будет",
                                       ParseMode.Html, true);
                                    }
                                }
                            }
                            else if (message.Text.ToLower().StartsWith("/appeal"))
                            {
                                if (currentUser.IsRegistered == false)
                                {
                                    var inlines = new List<InlineKeyboardButton[]>();
                                    inlines.Add(new[]
                                          {
                                                InlineKeyboardButton.WithCallbackData("🔑Регистрация", "register")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                    await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "Чтобы продолжить работу нужно пройти простую регистрацию, указав " +
                                        "Код подразделения и табельный номер, чтобы мы знали что ты в команде Газа.\n",
                                        ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                }
                                else
                                {
                                    var inlines = new List<InlineKeyboardButton[]>();
                                    inlines.Add(new[]
                                       {
                                               InlineKeyboardButton.WithCallbackData("👤Личное", "createPrivateAppeal")
                                            });
                                    inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("👥Публичное", "createPublicAppeal"),
                                            });
                                    inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                    await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "✉️Вы можете создать обращение к руководству, будь то вопрос, предложение или жалоба, мы все рассмотрим, и обязательно с вами свяжемся!\n" +
                                        "Выберите тип обращения\n" +
                                        "👤Личное - его увидит только руководство\n" +
                                        "👥Публичное - его увидят все сотрудники, и смогут вас поддержать",
                                        ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                }
                            }
                            else
                            {
                                if (!String.IsNullOrWhiteSpace(currentUser.State))
                                {
                                    if (currentUser.State == "enterSubdivision")
                                    {
                                        currentUser.Subdivision = message.Text;
                                        currentUser.State = "enterPersonNumber";
                                        connUser.Update(currentUser);

                                        var inlines = new List<InlineKeyboardButton[]>();
                                        inlines.Add(new[]
                                              {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                                        var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                        if (currentUser.LastMessageId > 0)
                                        {
                                            await Bot.Api.EditMessageTextAsync(message.Chat.Id, currentUser.LastMessageId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                                "Введите свой табельный номер (Например 100101)", ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                        }
                                        else
                                        {
                                            await Bot.Api.SendTextMessageAsync(message.Chat.Id, "➖➖➖➖➖➖➖➖➖➖\n" +
                                                "Введите свой табельный номер (Например 100101)", ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                        }
                                    }
                                    else if (currentUser.State == "enterPersonNumber")
                                    {
                                        currentUser.PersonNumber = message.Text;
                                        currentUser.DateRegister = DateTime.Now;
                                        currentUser.IsRegistered = true;
                                        currentUser.State = string.Empty;
                                        currentUser.LastMessageId = 0;
                                        connUser.Update(currentUser);

                                        var inlines = new List<InlineKeyboardButton[]>();
                                        inlines.Add(new[]
                                              {
                                                InlineKeyboardButton.WithCallbackData("📝Пройти анкетирование", "getAnkets"),
                                            });
                                        inlines.Add(new[]
                                              {
                                                InlineKeyboardButton.WithCallbackData("✉️Создать обращение", "appeal")
                                            });
                                        var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                        await Bot.Api.SendTextMessageAsync(chatId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                            "Вы успешно прошли регистрацию👍\n" +
                                            "Ваш ID  в системе: " + chatId + "\n" +
                                            "Код подразделения: " + currentUser.Subdivision + "\n" +
                                            "Табельный номер: " + currentUser.PersonNumber,
                                            ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                    }
                                    else if (currentUser.State == "createPrivateAppeal")
                                    {
                                        var connAppeal = new SQLiteConnection(Methods.PathToAppeals);
                                        Appeal newApp = new Appeal();
                                        newApp.ChatId = chatId;
                                        newApp.ID = Guid.NewGuid().ToString();
                                        newApp.Text = message.Text;
                                        newApp.IsPublic = false;
                                        newApp.IsNew = true;
                                        connAppeal.Insert(newApp);
                                        connAppeal.Close();

                                        currentUser.State = string.Empty;
                                        currentUser.LastMessageId = 0;
                                        connUser.Update(currentUser);

                                        var inlines = new List<InlineKeyboardButton[]>();
                                        inlines.Add(new[]
                                              {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                                        var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                        await Bot.Api.SendTextMessageAsync(message.Chat.Id, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "Ваше обращение успешно отправлено, мы с вами свяжемся в ближайшее время✅", ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                    }
                                    else if (currentUser.State == "createPublicAppeal")
                                    {
                                        var connAppeal = new SQLiteConnection(Methods.PathToAppeals);
                                        Appeal newApp = new Appeal();
                                        newApp.ChatId = chatId;
                                        newApp.ID = Guid.NewGuid().ToString();
                                        newApp.Text = message.Text;
                                        newApp.IsPublic = true;
                                        newApp.IsNew = true;
                                        connAppeal.Insert(newApp);
                                        connAppeal.Close();

                                        if (newApp.IsPublic == true)
                                        {
                                            var allUsers = connUser.Table<User>().Where(p => p.IsRegistered == true).ToList();
                                            if (allUsers.Count > 0)
                                            {
                                                var connSendAppeal = new SQLiteConnection(Methods.PathToSendAppealsToUser);
                                                allUsers.ForEach(p =>
                                                {
                                                    connSendAppeal.Insert(new SendAppealsToUser
                                                    {
                                                        ID = Guid.NewGuid().ToString(),
                                                        AppealID = newApp.ID,
                                                        ChatId = p.ChatId,
                                                        IsNew = true
                                                    });
                                                });
                                                connSendAppeal.Close();
                                            }
                                            var connAppealSigns = new SQLiteConnection(Methods.PathToAppealSigns);
                                            connAppealSigns.Insert(new AppealSigns { AppealID = newApp.ID, ID = Guid.NewGuid().ToString(), ChatId = chatId });
                                            connAppealSigns.Close();
                                        }

                                        currentUser.State = string.Empty;
                                        currentUser.LastMessageId = 0;
                                        connUser.Update(currentUser);

                                        var inlines = new List<InlineKeyboardButton[]>();
                                        inlines.Add(new[]
                                              {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                                        var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                        await Bot.Api.SendTextMessageAsync(message.Chat.Id, "➖➖➖➖➖➖➖➖➖➖\n" +
                                        "Ваше обращение успешно отправлено✅", ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                    }
                                }
                            }
                        }
                        connUser.Close();
                    }
                }
                else if (update.Type == UpdateType.CallbackQuery)
                {
                    var db = new SQLiteConnection(Methods.PathToUpdates);
                    var needUpdate = db.Table<UpdateMessage>().FirstOrDefault(p => p.UpdateId == update.Id);
                    var message = update.CallbackQuery;
                    if (needUpdate == null && message != null)
                    {
                        db.Insert(new UpdateMessage { UpdateId = update.Id });
                        db.Close();

                        long chatId = message.Message.Chat.Id;
                        var connUser = new SQLiteConnection(Methods.PathToUsers);
                        var currentUser = connUser.Table<User>().FirstOrDefault(p => p.ChatId == chatId);
                        if (message.Data == "register")
                        {
                            await Bot.Api.AnswerCallbackQueryAsync(message.Id);
                            if (currentUser.IsRegistered == false)
                            {
                                currentUser.LastMessageId = message.Message.MessageId;
                                currentUser.State = "enterSubdivision";
                                connUser.Update(currentUser);

                                var inlines = new List<InlineKeyboardButton[]>();
                                inlines.Add(new[]
                                      {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                                var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                await Bot.Api.EditMessageTextAsync(message.Message.Chat.Id, message.Message.MessageId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                    "Введите код подразделения, в котором работаете(Например 34-001-001)", ParseMode.Html, true, replyMarkup: inlineKeyboard);
                            }
                        }
                        else if (message.Data == "home")
                        {
                            await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                            currentUser.LastMessageId = 0;
                            currentUser.State = string.Empty;
                            connUser.Update(currentUser);

                            if (currentUser.IsRegistered == false)
                            {
                                var inlines = new List<InlineKeyboardButton[]>();
                                inlines.Add(new[]
                                      {
                                                InlineKeyboardButton.WithCallbackData("🔑Регистрация", "register")
                                            });
                                var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                    "Чтобы продолжить работу нужно пройти простую регистрацию, указав " +
                                    "Код подразделения и табельный номер, чтобы мы знали что ты в команде Газа.\n",
                                    ParseMode.Html, true, replyMarkup: inlineKeyboard);
                            }
                            else
                            {
                                var inlines = new List<InlineKeyboardButton[]>();
                                inlines.Add(new[]
                                      {
                                                InlineKeyboardButton.WithCallbackData("📝Пройти анкетирование", "getAnkets")
                                            });
                                inlines.Add(new[]
                                      {
                                                InlineKeyboardButton.WithCallbackData("✉️Создать обращение", "appeal")
                                            });
                                var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                    "Ваш ID  в системе: " + chatId + "\n" +
                                    "Код подразделения: " + currentUser.Subdivision + "\n" +
                                    "Табельный номер: " + currentUser.PersonNumber,
                                    ParseMode.Html, true, replyMarkup: inlineKeyboard);
                            }
                        }
                        else if (message.Data == "getAnkets")
                        {
                            var connAnkets = new SQLiteConnection(Methods.PathToAnkets);
                            var currentAnkets = connAnkets.Table<Anketa>().Where(p => p.IsActive == true).ToList();
                            currentAnkets = currentAnkets.OrderByDescending(p => p.Date).ToList();
                            connAnkets.Close();

                            if (currentAnkets.Count > 0)
                            {
                                await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                                var inlines = new List<InlineKeyboardButton[]>();
                                foreach (var anketa in currentAnkets)
                                {
                                    string idAnket = anketa.ID;

                                    string isFinished = string.Empty;
                                    var connAnketProgress = new SQLiteConnection(Methods.PathToAnketaUserProgress);
                                    var userProgress = connAnketProgress.Table<AnketaUserProgress>().FirstOrDefault(p => p.AnketaId == idAnket && p.ChatId == chatId);
                                    connAnketProgress.Close();
                                    if (userProgress != null && userProgress.IsSend == true)
                                    {
                                        isFinished += "✅";
                                    }
                                    inlines.Add(new[]
                                    {
                                                InlineKeyboardButton.WithCallbackData(isFinished + anketa.Name, "showAnket:" + idAnket)
                                            });
                                }
                                inlines.Add(new[]
                                   {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                                var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, "➖➖➖➖➖➖➖➖➖➖\n" +
                                    "📝Текущие анкеты:",
                                    ParseMode.Html, true, replyMarkup: inlineKeyboard);
                            }
                            else
                            {
                                await Bot.Api.AnswerCallbackQueryAsync(message.Id, "Активных анкет нет");
                            }
                        }
                        else if (message.Data == "appeal")
                        {
                            await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                            var inlines = new List<InlineKeyboardButton[]>();
                            inlines.Add(new[]
                               {
                                               InlineKeyboardButton.WithCallbackData("👤Личное", "createPrivateAppeal")
                                            });
                            inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData("👥Публичное", "createPublicAppeal")
                                            });
                            inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                            var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                            await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, "➖➖➖➖➖➖➖➖➖➖\n" +
                            "✉️Вы можете создать обращение к руководству, будь то вопрос, предложение или жалоба, мы все рассмотрим, и обязательно с вами свяжемся!\n" +
                                "Выберите тип обращения\n" +
                                "👤Личное - его увидит только руководство\n" +
                                "👥Публичное - его увидят все сотрудники, и смогут вас поддержать",
                                ParseMode.Html, true, replyMarkup: inlineKeyboard);
                        }
                        else if (message.Data == "createPrivateAppeal" || message.Data == "createPublicAppeal")
                        {
                            await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                            currentUser.LastMessageId = message.Message.MessageId;
                            currentUser.State = message.Data;
                            connUser.Update(currentUser);

                            var inlines = new List<InlineKeyboardButton[]>();
                            inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData("❌Отменить", "cancelCreateAppeal")
                                            });
                            inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "home")
                                            });
                            var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                            await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, "➖➖➖➖➖➖➖➖➖➖\n" +
                            "Введите текст своего обращения...", ParseMode.Html, true, replyMarkup: inlineKeyboard);
                        }
                        else if (message.Data == "cancelCreateAppeal")
                        {
                            await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                            currentUser.LastMessageId = 0;
                            currentUser.State = string.Empty;
                            connUser.Update(currentUser);

                            await Bot.Api.DeleteMessageAsync(chatId, message.Message.MessageId);
                        }
                        else if (message.Data.StartsWith("signAppeal"))
                        {
                            string appealId = message.Data.Split(':')[1].Trim();
                            var connAppeals = new SQLiteConnection(Methods.PathToAppeals);
                            var findAppeal = connAppeals.Table<Appeal>().FirstOrDefault(p => p.ID == appealId);
                            connAppeals.Close();

                            if (findAppeal != null)
                            {
                                await Bot.Api.AnswerCallbackQueryAsync(message.Id, "Вы подписали обращение!");

                                var connAppealSigns = new SQLiteConnection(Methods.PathToAppealSigns);
                                connAppealSigns.Insert(new AppealSigns { ID = Guid.NewGuid().ToString(), AppealID = appealId, ChatId = chatId });
                                var userSign = connAppealSigns.Table<AppealSigns>().FirstOrDefault(p => p.ChatId == chatId);
                                int countSigns = connAppealSigns.Table<AppealSigns>().Count(p => p.AppealID == appealId);
                                connAppealSigns.Close();

                                await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, "➖➖➖➖➖➖➖➖➖➖\n" + findAppeal.Text + "\n" +
                                    "Количество подписей: " + countSigns, ParseMode.Html, true);
                            }
                        }
                        else if (message.Data.StartsWith("showAnket"))
                        {
                            string anketId = message.Data.Split(':')[1].Trim();
                            var connAnkets = new SQLiteConnection(Methods.PathToAnkets);
                            var findAnket = connAnkets.Table<Anketa>().FirstOrDefault(p => p.ID == anketId);
                            connAnkets.Close();

                            if (findAnket != null)
                            {
                                var connAnketQs = new SQLiteConnection(Methods.PathToQuestions);
                                var ques = connAnketQs.Table<Question>().Where(p => p.AnketaId == anketId).ToList();
                                connAnketQs.Close();

                                if (ques.Count > 0)
                                {
                                    await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                                    var connProgress = new SQLiteConnection(Methods.PathToAnketaUserProgress);
                                    var progress = connProgress.Table<AnketaUserProgress>().FirstOrDefault(p => p.AnketaId == anketId && p.ChatId == chatId);
                                    connProgress.Close();

                                    bool isStarted = false;
                                    //bool isFinished = false;
                                    bool isSend = false;
                                    if (progress != null)
                                    {
                                        isStarted = true;
                                        //isFinished = progress.IsFinished;
                                        isSend = progress.IsSend;
                                    }

                                    var inlines = new List<InlineKeyboardButton[]>();
                                    string textMsg = string.Empty;
                                    textMsg += "➖➖➖➖➖➖➖➖➖➖\n" + findAnket.Name + "\n" +
                                    findAnket.Definition + "\nКоличество вопросов: " + ques.Count;
                                    if (isSend == true)
                                    {
                                        textMsg += "\nВы уже проходили эту анкету, спасибо!";
                                    }
                                    else
                                    {
                                        if (isStarted == false)
                                        {
                                            inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("🚩Заполнить анкету", "continueAnket:" + anketId)
                                            });
                                        }
                                        else
                                        {
                                            inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("⏩Продолжить прохождение", "continueAnket:" + anketId)
                                            });
                                        }
                                    }
                                    inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "getAnkets")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());

                                    await Bot.Api.SendTextMessageAsync(chatId, textMsg, ParseMode.Html, true, replyMarkup: inlineKeyboard);
                                }
                                else
                                {
                                    await Bot.Api.AnswerCallbackQueryAsync(message.Id, "Анкета пока не заполнена");

                                }
                            }
                        }
                        else if (message.Data.StartsWith("continueAnket"))
                        {
                            await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                            string anketId = message.Data.Split(':')[1].Trim();
                            var connAnkets = new SQLiteConnection(Methods.PathToAnkets);
                            var findAnket = connAnkets.Table<Anketa>().FirstOrDefault(p => p.ID == anketId);
                            connAnkets.Close();

                            if (findAnket != null)
                            {
                                var connProgress = new SQLiteConnection(Methods.PathToAnketaUserProgress);
                                var progress = connProgress.Table<AnketaUserProgress>().FirstOrDefault(p => p.AnketaId == anketId && p.ChatId == chatId);
                                if (progress == null)
                                {
                                    connProgress.Insert(new AnketaUserProgress
                                    {
                                        IsSend = false,
                                        ID = Guid.NewGuid().ToString(),
                                        AnketaId = anketId,
                                        ChatId = chatId,
                                        Date = DateTime.Now
                                    });
                                }
                                connProgress.Close();

                                var connAnketQs = new SQLiteConnection(Methods.PathToQuestions);
                                var ques = connAnketQs.Table<Question>().Where(p => p.AnketaId == anketId).ToList();
                                connAnketQs.Close();

                                var inlines = new List<InlineKeyboardButton[]>();
                                var connAnswers = new SQLiteConnection(Methods.PathToAnswerChoices);
                                var userAnswers = connAnswers.Table<AnswerChoice>().Where(p => p.ChatId == chatId && p.AnketaId == anketId).ToList();
                                int numberQue = 0;
                                string textMsg = string.Empty;
                                textMsg += "➖➖➖➖➖➖➖➖➖➖\n";
                                if (userAnswers.Count > 0)
                                {
                                    var lastQ = userAnswers.Select(p => p.QuestionId).Last();
                                    int indexOfQ = ques.FindIndex(p => p.ID == lastQ);
                                    if (indexOfQ != -1)
                                    {
                                        numberQue = indexOfQ + 1;
                                    }
                                }
                                if (numberQue <= ques.Count - 1)
                                {
                                    var needQue = ques[numberQue];
                                    textMsg += "Вопрос " + (numberQue + 1) + " из " + ques.Count + "\n";
                                    textMsg += needQue.Name;
                                    var connAnketQsIts = new SQLiteConnection(Methods.PathToQuestionItems);
                                    var queItems = connAnketQsIts.Table<QuestionItem>().Where(p => p.AnketaId == anketId && p.QuestionId == needQue.ID).ToList();
                                    connAnketQsIts.Close();
                                    if (queItems.Count > 0)
                                    {
                                        queItems.ForEach(p =>
                                        {
                                            inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData(p.Name, "chooseAnswer:" + p.ID)
                                            });
                                        });
                                    }
                                }
                                else
                                {
                                    textMsg += "Вы успешно заполнили анкету, осталось только отправить ее на обработку или пройти еще раз, если есть сомнения.\n" +
                                        "Спасибо";
                                    inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData("🔄Пройти еще раз", "againStartAnket:" + anketId)
                                            });
                                    inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData("📩Отправить результаты", "sendAnket:" + anketId)
                                            });
                                }
                                inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "getAnkets")
                                            });
                                var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());

                                await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, textMsg, ParseMode.Html, true, replyMarkup: inlineKeyboard);

                                connAnswers.Close();
                            }
                        }
                        else if (message.Data.StartsWith("chooseAnswer"))
                        {

                            string queItem = message.Data.Split(':')[1].Trim();
                            var connQueIts = new SQLiteConnection(Methods.PathToQuestionItems);
                            var findQueItem = connQueIts.Table<QuestionItem>().FirstOrDefault(p => p.ID == queItem);
                            connQueIts.Close();

                            if (findQueItem != null)
                            {
                                string anketId = findQueItem.AnketaId;
                                var connAnkets = new SQLiteConnection(Methods.PathToAnkets);
                                var findAnket = connAnkets.Table<Anketa>().FirstOrDefault(p => p.ID == anketId);
                                connAnkets.Close();

                                string queId = findQueItem.QuestionId;
                                var connQues = new SQLiteConnection(Methods.PathToQuestions);
                                var ques = connQues.Table<Question>().Where(p => p.AnketaId == anketId).ToList();
                                var findQue = ques.FirstOrDefault(p => p.ID == queId);
                                connQues.Close();

                                if (findAnket != null && findQue != null)
                                {
                                    await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                                    int numberQue = ques.IndexOf(findQue);
                                    string textMsg = string.Empty;
                                    textMsg += "➖➖➖➖➖➖➖➖➖➖\n";
                                    textMsg += "Вопрос " + (numberQue + 1) + " из " + ques.Count + "\n";
                                    textMsg += findQue.Name;

                                    var connAnswers = new SQLiteConnection(Methods.PathToAnswerChoices);
                                    var findAnswer = connAnswers.Table<AnswerChoice>().FirstOrDefault(p => p.ChatId == chatId && p.QuestionItemId == queItem);
                                    if (findAnswer == null)
                                    {
                                        int value = 1;
                                        if (findQueItem.Name == "Скорее, НЕ согласен (-а)")
                                        {
                                            value = 2;
                                        }
                                        if (findQueItem.Name == "Скорее, согласен(-а)")
                                        {
                                            value = 3;
                                        }
                                        if (findQueItem.Name == "Полностью согласен (-а)")
                                        {
                                            value = 4;
                                        }
                                        connAnswers.Insert(new AnswerChoice { QuestionItemId = queItem, AnketaId = anketId, ChatId = chatId,
                                            ID = Guid.NewGuid().ToString(), QuestionId = queId, Value = value, Accessory = findQue.Accessory
                                        });
                                    }
                                    else
                                    {
                                        connAnswers.Delete(findAnswer);
                                    }
                                    findAnswer = connAnswers.Table<AnswerChoice>().FirstOrDefault(p => p.ChatId == chatId && p.QuestionItemId == queItem);
                                    var connAnketQsIts = new SQLiteConnection(Methods.PathToQuestionItems);
                                    var queItems = connAnketQsIts.Table<QuestionItem>().Where(p => p.AnketaId == anketId && p.QuestionId == queId).ToList();
                                    connAnketQsIts.Close();
                                    var inlines = new List<InlineKeyboardButton[]>();
                                    if (queItems.Count > 0)
                                    {
                                        queItems.ForEach(p =>
                                        {
                                            string choice = string.Empty;
                                            if (findAnswer != null && p.ID == findAnswer.QuestionItemId)
                                            {
                                                choice = "☑️";
                                            }
                                            inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData(choice + p.Name, "chooseAnswer:" + p.ID)
                                            });
                                        });
                                    }
                                    if (findAnswer != null)
                                    {
                                        inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("▶️Далее", "continueAnket:" + anketId)
                                            });
                                    }
                                    inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "getAnkets")
                                            });
                                    var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());
                                    await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, textMsg, ParseMode.Html, true, replyMarkup: inlineKeyboard);

                                    connAnswers.Close();
                                }
                                else
                                {
                                    await Bot.Api.AnswerCallbackQueryAsync(message.Id, "Ошибка");
                                }
                            }
                            else
                            {
                                await Bot.Api.AnswerCallbackQueryAsync(message.Id, "Ошибка");
                            }
                        }
                        else if (message.Data.StartsWith("againStartAnket"))
                        {
                            await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                            string anketId = message.Data.Split(':')[1].Trim();
                            var connAnkets = new SQLiteConnection(Methods.PathToAnkets);
                            var findAnket = connAnkets.Table<Anketa>().FirstOrDefault(p => p.ID == anketId);
                            connAnkets.Close();

                            if (findAnket != null)
                            {
                                var conn4 = new SQLiteConnection(Methods.PathToAnswerChoices);
                                var answers = conn4.Table<AnswerChoice>().Where(p => p.AnketaId == anketId &&  p.ChatId == chatId).ToList();
                                if (answers.Count > 0)
                                {
                                    answers.ForEach(p => conn4.Delete(p));
                                }
                                conn4.Close();

                                var conn5 = new SQLiteConnection(Methods.PathToAnswerTexts);
                                var texts = conn5.Table<AnswerText>().Where(p => p.AnketaId == anketId && p.ChatId == chatId).ToList();
                                if (texts.Count > 0)
                                {
                                    texts.ForEach(p => conn5.Delete(p));
                                }
                                conn5.Close();

                                var connProgress = new SQLiteConnection(Methods.PathToAnketaUserProgress);
                                var progress = connProgress.Table<AnketaUserProgress>().FirstOrDefault(p => p.AnketaId == anketId && p.ChatId == chatId);
                                if (progress == null)
                                {
                                    connProgress.Insert(new AnketaUserProgress
                                    {
                                        IsSend = false,
                                        ID = Guid.NewGuid().ToString(),
                                        AnketaId = anketId,
                                        ChatId = chatId,
                                        Date = DateTime.Now
                                    });
                                }
                                connProgress.Close();

                                var connAnketQs = new SQLiteConnection(Methods.PathToQuestions);
                                var ques = connAnketQs.Table<Question>().Where(p => p.AnketaId == anketId).ToList();
                                connAnketQs.Close();

                                var inlines = new List<InlineKeyboardButton[]>();
                                var connAnswers = new SQLiteConnection(Methods.PathToAnswerChoices);
                                var userAnswers = connAnswers.Table<AnswerChoice>().Where(p => p.ChatId == chatId && p.AnketaId == anketId).ToList();
                                int numberQue = 0;
                                string textMsg = string.Empty;
                                textMsg += "➖➖➖➖➖➖➖➖➖➖\n";
                                if (userAnswers.Count > 0)
                                {
                                    var lastQ = userAnswers.Select(p => p.QuestionId).Last();
                                    int indexOfQ = ques.FindIndex(p => p.ID == lastQ);
                                    if (indexOfQ != -1)
                                    {
                                        numberQue = indexOfQ + 1;
                                    }
                                }
                                if (numberQue <= ques.Count - 1)
                                {
                                    var needQue = ques[numberQue];
                                    textMsg += "Вопрос " + (numberQue + 1) + " из " + ques.Count + "\n";
                                    textMsg += needQue.Name;
                                    var connAnketQsIts = new SQLiteConnection(Methods.PathToQuestionItems);
                                    var queItems = connAnketQsIts.Table<QuestionItem>().Where(p => p.AnketaId == anketId && p.QuestionId == needQue.ID).ToList();
                                    connAnketQsIts.Close();
                                    if (queItems.Count > 0)
                                    {
                                        queItems.ForEach(p =>
                                        {
                                            inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData(p.Name, "chooseAnswer:" + p.ID)
                                            });
                                        });
                                    }
                                }
                                else
                                {
                                    textMsg += "Вы успешно заполнили анкету, осталось только отправить ее на обработку или пройти еще раз, если есть сомнения.\n" +
                                        "Спасибо";
                                    inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData("🔄Пройти еще раз", "againStartAnket:" + anketId)
                                            });
                                    inlines.Add(new[]
                               {
                                                InlineKeyboardButton.WithCallbackData("📩Отправить результаты", "sendAnket:" + anketId)
                                            });
                                }
                                inlines.Add(new[]
                                       {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "getAnkets")
                                            });
                                var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());

                                await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, textMsg, ParseMode.Html, true, replyMarkup: inlineKeyboard);

                                connAnswers.Close();
                            }
                        }
                        else if (message.Data.StartsWith("sendAnket"))
                        {
                            await Bot.Api.AnswerCallbackQueryAsync(message.Id);

                            string anketId = message.Data.Split(':')[1].Trim();
                            var connAnkets = new SQLiteConnection(Methods.PathToAnkets);
                            var findAnket = connAnkets.Table<Anketa>().FirstOrDefault(p => p.ID == anketId);
                            connAnkets.Close();

                            if (findAnket != null)
                            {
                                var connProgress = new SQLiteConnection(Methods.PathToAnketaUserProgress);
                                var progress = connProgress.Table<AnketaUserProgress>().FirstOrDefault(p => p.AnketaId == anketId && p.ChatId == chatId);
                                if (progress != null)
                                {
                                    progress.IsSend = true;
                                    connProgress.Update(progress);
                                }
                                connProgress.Close();

                                var inlines = new List<InlineKeyboardButton[]>();
                                inlines.Add(new[]
       {
                                                InlineKeyboardButton.WithCallbackData("↩️Назад", "getAnkets")
                                            });
                                var inlineKeyboard = new InlineKeyboardMarkup(inlines.ToArray());

                                await Bot.Api.EditMessageTextAsync(chatId, message.Message.MessageId, "➖➖➖➖➖➖➖➖➖➖\nСпасибо за прохождение анкеты, " +
                                    "нам важно ваше мнение🤝", ParseMode.Html, true, replyMarkup: inlineKeyboard);

                            }
                        }
                        connUser.Close();
                    }
                }
                return Ok();
            }
        }
    }
}
