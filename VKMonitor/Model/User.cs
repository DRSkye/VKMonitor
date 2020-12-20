using System;
using System.Collections.Generic;
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

    public class User
    {
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
        public DateTime? BirthDate { get; set; }

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
        /// Указан ли мобильный телефон
        /// </summary>
        public bool HasMobile { get; set; }

        /// <summary>
        /// Контакты
        /// </summary>
        public Contacts Contacts { get; set; }

        /// <summary>
        /// Список групп
        /// </summary>
        public List<Group> Groups { get; } = new List<Group>();

        public User(VkNet.Model.User user)
        {
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
                IsClosed = user.IsClosed.Value;

                IsBlackListed = user.Blacklisted;

                if (IsClosed)
                    CanAccessClosed = user.CanAccessClosed.Value;

                HasPhoto = user.HasPhoto.Value;
                if (HasPhoto)
                    PhotoId = user.PhotoId;

                switch (user.BirthdayVisibility.Value)
                {
                    case BirthdayVisibility.Invisible:
                        BirthDateVisibility = BirthDateVisibility.Invisible;
                        BirthDate = null;
                        break;
                    case BirthdayVisibility.Full:
                        BirthDateVisibility = BirthDateVisibility.Full;
                        BirthDate = Convert.ToDateTime(user.BirthDate);
                        break;
                    case BirthdayVisibility.OnlyDayAndMonth:
                        BirthDateVisibility = BirthDateVisibility.DateAndMonth;
                        var mas = user.BirthDate.Split('.');
                        BirthDate = new DateTime(0, Convert.ToInt32(mas[1]), Convert.ToInt32(mas[0]));
                        break;
                }

                
                
                City = new City(user.City);
                HomeCity = user.HomeTown;
                Country = new Country(user.Country);

                About = user.About;
                Activities = user.Activities;
                Books = user.Books;
                Games = user.Games;
                Interests = user.Interests;

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

                CommonFriendsCount = user.CommonCount.Value;

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

                HasMobile = user.HasMobile.Value;
                if (HasMobile)
                    Contacts = new Contacts(user.Contacts);


            }
        }
    }
}
