using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazNNBot
{
    public class UpdateMessage
    {
        [PrimaryKey]
        public int UpdateId { get; set; }
    }
    public class User
    {
        [PrimaryKey]
        public long ChatId { get; set; }
        public DateTime DateRegister { get; set; }
        public string Subdivision { get; set; }
        public string PersonNumber { get; set; }
        public string State { get; set; }
        public int LastMessageId { get; set; }
        public bool IsRegistered { get; set; }
    }
    public class Anketa
    {
        [PrimaryKey]
        public string ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public bool IsActive { get; set; }
    }
    public class AnketaUserProgress
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string AnketaId { get; set; }
        public long ChatId { get; set; }
        public DateTime Date { get; set; }
        //public bool IsFinished { get; set; }
        public bool IsSend { get; set; }
    }
    public class Question
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string AnketaId { get; set; }
        public string Name { get; set; }
        public bool EnterText { get; set; }
        public bool MultipleChoice { get; set; }
        public string Accessory { get; set; }
    }
    public class QuestionItem
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string AnketaId { get; set; }
        public string QuestionId { get; set; }
        public string Name { get; set; }
    }
    public class AnswerChoice
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string AnketaId { get; set; }
        public string QuestionId { get; set; }
        public string QuestionItemId { get; set; }
        public int Value { get; set; }
        public string Accessory { get; set; }
        public long ChatId { get; set; }
    }
    public class AnswerText
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string AnketaId { get; set; }
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public long ChatId { get; set; }
    }
    public class Appeal
    {
        [PrimaryKey]
        public string ID { get; set; }
        public long ChatId { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; }
        public bool IsNew { get; set; }
    }
    public class AppealSigns
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string AppealID { get; set; }
        public long ChatId { get; set; }
    }
    public class SendAnketsToUser
    {
        [PrimaryKey]
        public string ID { get; set; }
        public long ChatId { get; set; }
        public string AnketaId { get; set; }
        public bool IsNew { get; set; }
    }
    public class SendAppealsToUser
    {
        [PrimaryKey]
        public string ID { get; set; }
        public long ChatId { get; set; }
        public string AppealID { get; set; }
        public bool IsNew { get; set; }
    }
}
