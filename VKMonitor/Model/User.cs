using System;
using System.Collections.Generic;
using System.ComponentModel;
using VkNet.Enums;
using VkNet.Model;
using System.Linq;

namespace VKMonitor.Model
{
    public enum BirthDateVisibility
    {
        Invisible, Full, DateAndMonth
    }

    public enum FriendStatus
    {
        NotFriend, OutputRequest, InputRequest, Friend 
    }

    public enum RelationType
    {
        Unknown, NotMarried, HasFriend, Engaged, Married, ItsComplex, InActiveSearch, Amorous, CivilMarriage
    }

    public enum Sex
    {
        Unknown, Female, Male
    }

    public class User
    {
        #region Properties

        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Домен пользователя id***** или название страницы в адресной строке
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Идентификатор в виде строки
        /// </summary>
        public string ScreenName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Ник (отчетсво)
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Забанен ли пользователь
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// Информация о бане
        /// </summary>
        public string BanInfo { get; set; }

        /// <summary>
        /// Закрыта ли страница
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Могу ли я видеть страницу, если она закрыта
        /// </summary>
        public bool CanAccessClosed { get; set; }

        /// <summary>
        /// Есть ли ава
        /// </summary>
        public bool HasPhoto { get; set; }

        /// <summary>
        /// Идентификатор авы. Если нет, то нет авы, либо она не менялась очень давно.
        /// </summary>
        public string PhotoId { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// День Рождения
        /// </summary>
        public string BirthDateStr
        {
            get
            {
                switch (BirthDateVisibility)
                {
                    case BirthDateVisibility.Invisible:
                        return $@"Скрыт";
                    case BirthDateVisibility.DateAndMonth:
                        return $@"{BirthDate.Day}.{BirthDate.Month}";
                    case BirthDateVisibility.Full:
                        return $@"{BirthDate.ToShortDateString()}";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Тип видимости даты рождения
        /// </summary>
        public BirthDateVisibility BirthDateVisibility { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public City City { get; set; }

        /// <summary>
        /// Родной город
        /// </summary>
        public string HomeCity { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// О себе
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Деятельность
        /// </summary>
        public string Activities { get; set; }

        /// <summary>
        /// Книги
        /// </summary>
        public string Books { get; set; }

        /// <summary>
        /// Игры
        /// </summary>
        public string Games { get; set; }

        /// <summary>
        /// Интересы
        /// </summary>
        public string Interests { get; set; }

        /// <summary>
        /// Фильмы
        /// </summary>
        public string Movies { get; set; }

        /// <summary>
        /// Музыка
        /// </summary>
        public string Music { get; set; }

        /// <summary>
        /// Цитаты
        /// </summary>
        public string Quotes { get; set; }

        /// <summary>
        /// Телешоу
        /// </summary>
        public string TV { get; set; }

        /// <summary>
        /// Заблокировал ли пользователь меня
        /// </summary>
        public bool IsBlackListed { get; set; }

        /// <summary>
        /// Заблокировал ли я пользователя
        /// </summary>
        public bool IsBlackListedByMe { get; set; }

        /// <summary>
        /// Может ли я оставлять записи на стене пользователя
        /// </summary>
        public bool CanPost { get; set; }

        /// <summary>
        /// Может ли я видеть чужие записи на стене пользователя
        /// </summary>
        public bool CanSeeAllPost { get; set; }

        /// <summary>
        /// Может ли я видеть аудиозаписи пользователя
        /// </summary>
        public bool CanSeeAudio { get; set; }

        /// <summary>
        /// Могу ли я отправить запрос в друзья
        /// </summary>
        public bool CanSendFriendRequest { get; set; }

        /// <summary>
        /// Могу ли я писать пользователю в лс
        /// </summary>
        public bool CanWritePrivateMessage { get; set; }

        /// <summary>
        /// Работы
        /// </summary>
        public List<Career> Careers { get; set; } = new List<Career>();

        /// <summary>
        /// Количество общих друзей
        /// </summary>
        public int CommonFriendsCount { get; set; }

        /// <summary>
        /// Привязанные аккаунты
        /// </summary>
        public Connections Connections { get; set; }

        /// <summary>
        /// Количество различных объектов у пользователя
        /// </summary>
        public Counters Counters { get; set; }

        /// <summary>
        /// Образование
        /// </summary>
        public Education Education { get; set; }

        /// <summary>
        /// Сервисы, куда настроен экспорт
        /// </summary>
        public Exports Exports { get; set; }

        /// <summary>
        /// Количество подписчиков
        /// </summary>
        public long? FollowersCount { get; set; }

        /// <summary>
        /// Статус друга
        /// </summary>
        public FriendStatus FriendStatus { get; set; }

        /// <summary>
        /// Статус друга в виде строки
        /// </summary>
        public string FriendStatusStr
        {
            get
            {
                switch (FriendStatus)
                {
                    case FriendStatus.Friend:
                        return "Друзья";
                    case FriendStatus.InputRequest:
                        return "Входящий запрос в друзья";
                    case FriendStatus.OutputRequest:
                        return "Исходящий запрос в друзья";
                    case FriendStatus.NotFriend:
                    default:
                        return "Не друзья";
                }
            }
        }

        /// <summary>
        /// Указан ли мобильный телефон
        /// </summary>
        public bool HasMobile { get; set; }

        /// <summary>
        /// Контакты
        /// </summary>
        public Contacts Contacts { get; set; }

        /// <summary>
        /// Находится ли пользователь в закладках
        /// </summary>
        public bool IsFavourite { get; set; }

        /// <summary>
        /// Находится ли пользователь в друзьях
        /// </summary>
        public bool IsFriend { get; set; }

        /// <summary>
        /// Скрыт ли пользователь из ленты новостей
        /// </summary>
        public bool IsHiddenFromFeed { get; set; }

        /// <summary>
        /// Последний раз, когда был в сети
        /// </summary>
        public LastSeen LastSeen { get; set; }

        /// <summary>
        /// Девичья фамилия
        /// </summary>
        public string MaidenName { get; set; }

        /// <summary>
        /// Находится ли пользователь онлайн
        /// </summary>
        public bool Online { get; set; }

        /// <summary>
        /// Находится ли пользователь онлайн с телефона
        /// </summary>
        public bool OnlineMobile { get; set; }

        /// <summary>
        /// Находится ли пользователь онлайн с телефона через приложение
        /// </summary>
        public long? OnlineApp { get; set; }

        /// <summary>
        /// Семейное положение
        /// </summary>
        public RelationType Relation { get; set; }

        public string RelationStr
        {
            get
            {
                switch (Relation)
                {
                    case RelationType.NotMarried:
                        return "Не женат/Не замужем";
                    case RelationType.Amorous:
                        return "Влюблён/Влюблена";
                    case RelationType.HasFriend:
                        return "Встречаюсь";
                    case RelationType.CivilMarriage:
                        return "В гражданском браке";
                    case RelationType.ItsComplex:
                        return "Всё сложно";
                    case RelationType.InActiveSearch:
                        return "В активном поиске";
                    case RelationType.Married:
                        return "Женат/Замужем";
                    case RelationType.Engaged:
                        return "Помолвлен/Помолвлена";
                    case RelationType.Unknown:
                    default:
                        return "Неизвестно";
                }
            }
        }

        /// <summary>
        /// Партнёр
        /// </summary>
        public User RelationPartner { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Sex Sex { get; set; }

        public string SexStr
        {
            get
            {
                switch (Sex)
                {
                    case Sex.Male:
                        return "Мужской";
                    case Sex.Female:
                        return "Женский";
                    case Sex.Unknown:
                    default:
                        return "Не указано";
                }
            }
        }

        /// <summary>
        /// Сайт
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Список родственников
        /// </summary>
        public List<Relative> Relatives { get; } = new List<Relative>();

        /// <summary>
        /// Список групп
        /// </summary>
        public List<Group> Groups { get; } = new List<Group>();

        /// <summary>
        /// Школы
        /// </summary>
        public List<School> Schools { get; } = new List<School>();

        /// <summary>
        /// Список друзей
        /// </summary>
        public List<long> Friends { get; } = new List<long>();

        #endregion

        public User(VkNet.Model.User user)
        {
            if (user == null)
                return;

            Id = user.Id;
            ScreenName = user.ScreenName;
            Name = user.FirstName;
            LastName = user.LastName;
            IsBanned = user.IsDeactivated;
            IsBlackListedByMe = user.BlacklistedByMe;

            if (IsBanned)
                BanInfo = user.Deactivated.ToString();
            else
            {
                IsBlackListed = user.Blacklisted;
                if (IsBlackListed)
                    return;

                IsClosed = user.IsClosed.HasValue ? user.IsClosed.Value : true;

                if (IsClosed)
                    CanAccessClosed = user.CanAccessClosed.HasValue ? user.CanAccessClosed.Value : false;

                if (IsClosed && !CanAccessClosed)
                    return;

                PhotoId = user.PhotoId;

                if (user.BirthdayVisibility.HasValue)
                {
                    switch (user.BirthdayVisibility.Value)
                    {
                        case BirthdayVisibility.Invisible:
                            BirthDateVisibility = BirthDateVisibility.Invisible;
                            BirthDate = DateTime.MinValue;
                            break;
                        case BirthdayVisibility.Full:
                            BirthDateVisibility = BirthDateVisibility.Full;
                            BirthDate = Convert.ToDateTime(user.BirthDate);
                            break;
                        case BirthdayVisibility.OnlyDayAndMonth:
                            BirthDateVisibility = BirthDateVisibility.DateAndMonth;
                            var mas = user.BirthDate.Split('.');
                            BirthDate = new DateTime(DateTime.MinValue.Year, Convert.ToInt32(mas[1]), Convert.ToInt32(mas[0]));
                            break;
                    }
                }

                City = new City(user.City);
                HomeCity = user.HomeTown;
                Country = new Country(user.Country);

                About = user.About;
                Activities = user.Activities;
                Books = user.Books;
                Games = user.Games;
                Interests = user.Interests;
                Movies = user.Movies;
                Music = user.Music;
                Quotes = user.Quotes;
                TV = user.Tv;

                Site = user.Site;
                Status = user.Status;

                CanPost = user.CanPost;
                CanSeeAllPost = user.CanSeeAllPosts;
                CanSeeAudio = user.CanSeeAudio;
                CanSendFriendRequest = user.CanSendFriendRequest;
                CanWritePrivateMessage = user.CanWritePrivateMessage;

                VkNet.Model.Career[] careers = new VkNet.Model.Career[user.Career.Count];
                user.Career.CopyTo(careers, 0);
                foreach (var career in careers)
                {
                    Careers.Add(new Career(career));
                }

                CommonFriendsCount = user.CommonCount.HasValue ? user.CommonCount.Value : 0;

                Connections = new Connections(user.Connections);

                Counters = new Counters(user.Counters);

                Education = new Education(user.Education);

                Exports = new Exports(user.Exports);

                FollowersCount = user.FollowersCount;

                switch (user.FriendStatus)
                {
                    case VkNet.Enums.FriendStatus.NotFriend:
                        FriendStatus = FriendStatus.NotFriend;
                        break;
                    case VkNet.Enums.FriendStatus.InputRequest:
                        FriendStatus = FriendStatus.InputRequest;
                        break;
                    case VkNet.Enums.FriendStatus.OutputRequest:
                        FriendStatus = FriendStatus.OutputRequest;
                        break;
                    case VkNet.Enums.FriendStatus.Friend:
                        FriendStatus = FriendStatus.Friend;
                        break;
                }

                HasMobile = user.HasMobile.HasValue ? user.HasMobile.Value : false;
                if (HasMobile)
                    Contacts = new Contacts(user.Contacts);

                IsFriend = user.IsFriend.Value;
                IsFavourite = user.IsFavorite;
                IsHiddenFromFeed = user.IsHiddenFromFeed;

                LastSeen = new LastSeen(user.LastSeen); //Доработать платформы

                switch (user.Sex)
                {
                    case VkNet.Enums.Sex.Deactivated:
                    case VkNet.Enums.Sex.Unknown:
                        Sex = Sex.Unknown;
                        break;
                    case VkNet.Enums.Sex.Female:
                        Sex = Sex.Female;
                        MaidenName = user.MaidenName;
                        break;
                    case VkNet.Enums.Sex.Male:
                        Sex = Sex.Male;
                        break;
                }

                Online = user.Online.HasValue ? user.Online.Value : false;
                if (Online)
                {
                    OnlineMobile = user.OnlineMobile.HasValue ? user.OnlineMobile.Value : false;
                    if (OnlineMobile)
                        OnlineApp = user.OnlineApp;
                }
                else
                {
                    OnlineMobile = false;
                    OnlineApp = null;
                }

                VkNet.Model.Relative[] rel = new VkNet.Model.Relative[user.Relatives.Count];
                user.Relatives.CopyTo(rel, 0);
                foreach (var relative in rel)
                {
                    Relatives.Add(new Relative(relative)); //Доработать родственников, родство не соответсвует названию
                }

                switch (user.Relation) //Доработать получение партнёра
                {
                    case VkNet.Enums.RelationType.Unknown:
                        Relation = RelationType.Unknown;
                        break;
                    case VkNet.Enums.RelationType.NotMarried:
                        Relation = RelationType.NotMarried;
                        break;
                    case VkNet.Enums.RelationType.Amorous:
                        Relation = RelationType.Amorous;
                        //RelationPartner = new User(user.RelationPartner);
                        break;
                    case VkNet.Enums.RelationType.CivilMarriage:
                        Relation = RelationType.CivilMarriage;
                        //RelationPartner = new User(user.RelationPartner);
                        break;
                    case VkNet.Enums.RelationType.Engaged:
                        Relation = RelationType.Engaged;
                        //RelationPartner = new User(user.RelationPartner);
                        break;
                    case VkNet.Enums.RelationType.HasFriend:
                        Relation = RelationType.HasFriend;
                        //RelationPartner = new User(user.RelationPartner);
                        break;
                    case VkNet.Enums.RelationType.InActiveSearch:
                        Relation = RelationType.InActiveSearch;
                        break;
                    case VkNet.Enums.RelationType.ItsComplex:
                        Relation = RelationType.ItsComplex;
                        break;
                    case VkNet.Enums.RelationType.Married:
                        Relation = RelationType.Married;
                        //RelationPartner = new User(user.RelationPartner);
                        break;
                }

                long[] friends = new long[user.FriendLists.Count];
                user.FriendLists.CopyTo(friends,0);
                foreach (var friend in friends)
                {
                    Friends.Add(friend);
                }
            }
        }
    }
}
