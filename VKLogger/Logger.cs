using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VKMonitor.Model;

namespace VKLogger
{
    public static class Logger
    {
        public static void WriteUsers(List<User> users)
        {
            users.AsParallel().ForAll(WriteUser);
        }

        public static void WriteChanges(List<User> lastUsers, List<User> curUsers)
        {
            var help = curUsers.Join(lastUsers, c => c.Id, l => l.Id, (c, l) => new {c, l}).ToList();

            if (!help.Any())
                return;
            
            help.AsParallel().ForAll(obj => WriteUserChanges(obj.c,obj.l));
        }

        private static void WriteUser(User user)
        {
            WriteUserData(user);
        }

        private static void WriteUserData(User user)
        {
            var fullName = $@"id{user.Id}";

            string path = @"C:\VKMonitorServiceLogs\Users";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += $@"\{fullName}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string file = path + $@"\{user.Name} {user.LastName} {DateTime.Now:dd.MM.yyyy hh.mm.ss} {DateTime.Now:dd.MM.yyyy hh.mm.ss}.txt";

            using (var writer = new StreamWriter(file, false))
            {
                writer.WriteLine($@"Id пользователя: {user.Id}");
                writer.WriteLine($@"Имя страницы: {user.ScreenName}");
                writer.WriteLine($@"Домен: {user.Domain}");
                writer.WriteLine($@"Имя: {user.Name}");
                writer.WriteLine($@"Фамилия: {user.LastName}");
                if (!string.IsNullOrEmpty(user.MaidenName))
                    writer.WriteLine($@"Девичья фамилия: {user.MaidenName}");
                writer.WriteLine($@"Пол: {user.SexStr}");
                writer.WriteLine($@"Семейное положение: {user.RelationStr}");
                writer.WriteLine($@"Статус: {user.Status}");
                writer.WriteLine($@"Страница заблокирована: " + (user.IsBanned ? "Да" : "Нет"));
                writer.WriteLine($@"Я добавил в ЧС: " + (user.IsBlackListedByMe ? "Да" : "Нет"));
                writer.WriteLine($@"Статус друга: {user.FriendStatusStr}");

                if (user.IsBanned)
                    writer.WriteLine($@"{user.BanInfo}"); //Доработать
                else
                {
                    if (user.IsBlackListed)
                    {
                        writer.WriteLine($@"Я в ЧС: " + (user.IsBlackListed ? "Да" : "Нет"));
                        return;
                    }

                    writer.WriteLine($@"Страница закрыта: " + (user.IsClosed ? "Да" : "Нет"));
                    if (user.IsClosed)
                        writer.WriteLine($@"Я могу видеть закрытую страницу: " + (user.CanAccessClosed ? "Да" : "Нет"));

                    if (user.IsClosed && !user.CanAccessClosed)
                        return;

                    writer.WriteLine($@"Идентификатор авы: {user.PhotoId}");
                    writer.WriteLine($@"День рождения: {user.BirthDateStr}");

                    writer.WriteLine($@"Страна: {user.Country.Title}");
                    writer.WriteLine($@"Город: {user.City.Title}");
                    writer.WriteLine($@"Родной город: {user.HomeCity}");

                    writer.WriteLine();
                    writer.WriteLine($@"Информация о пользователе:");
                    writer.WriteLine($@"    О себе: {user.About}");
                    writer.WriteLine($@"    Деятельность: {user.Activities}");
                    writer.WriteLine($@"    Книги: {user.Books}");
                    writer.WriteLine($@"    Фильмы: {user.Movies}");
                    writer.WriteLine($@"    Телешоу: {user.TV}");
                    writer.WriteLine($@"    Музыка: {user.Music}");
                    writer.WriteLine($@"    Игры: {user.Games}");
                    writer.WriteLine($@"    Интересы: {user.Interests}");
                    writer.WriteLine($@"    Цитаты: {user.Quotes}");

                    if (user.Counters != null)
                    {
                        writer.WriteLine();
                        writer.WriteLine($@"Счётчики:");
                        writer.WriteLine($@"    Количество друзей: {user.Counters.Friends}");
                        writer.WriteLine($@"    Количество общих друзей: {user.Counters.MutualFriends}");
                        writer.WriteLine($@"    Количество подписчиков: {user.Counters.Followers}");
                        writer.WriteLine($@"    Количество групп: {user.Counters.Groups}");
                        writer.WriteLine($@"    Количество страниц: {user.Counters.Pages}");
                        writer.WriteLine($@"    Количество видео: {user.Counters.Videos}");
                        writer.WriteLine($@"    Количество видео, на которых отмечен пользователь: {user.Counters.UserVideos}");
                        writer.WriteLine($@"    Количество аудиозаписей: {user.Counters.Audios}");
                        writer.WriteLine($@"    Количество открытых альбомов: {user.Counters.Albums}");
                        writer.WriteLine($@"    Количество открытых фото: {user.Counters.Photos}");
                        writer.WriteLine($@"    Количество фото, на которых отмечен пользователь: {user.Counters.UserPhotos}");
                        writer.WriteLine($@"    Количество заметок: {user.Counters.Notes}");
                        writer.WriteLine($@"    Количество подарков: {user.Counters.Gifts}");
                    }

                    if (user.Connections != null)
                    {
                        writer.WriteLine();
                        writer.WriteLine($@"Аккаунты на других ресурсах:");
                        writer.WriteLine($@"    Сайт: {user.Site}");
                        writer.WriteLine($@"    Инстаграмм: {user.Connections.Instagram}");
                        writer.WriteLine($@"    Идентификатор фейсбука: {user.Connections.FacebookId}");
                        writer.WriteLine($@"    Имя на фейсбуке: {user.Connections.FacebookName}");
                        writer.WriteLine($@"    Твиттер: {user.Connections.Twitter}");
                        writer.WriteLine($@"    Скайп: {user.Connections.Skype}");
                    }

                    if (user.Exports != null)
                    {
                        writer.WriteLine();
                        writer.WriteLine($@"Экспорт записей на другие ресурсы:");
                        writer.WriteLine($@"    Инстаграмм: " + (user.Exports.Instagram ? "Да" : "Нет"));
                        writer.WriteLine($@"    Фейсбук: " + (user.Exports.Facebook ? "Да" : "Нет"));
                        writer.WriteLine($@"    LiveJournal: " + (user.Exports.Livejournal ? "Да" : "Нет"));
                        writer.WriteLine($@"    Твиттер: " + (user.Exports.Twitter ? "Да" : "Нет"));
                    }

                    if (user.Contacts != null)
                    {
                        writer.WriteLine();
                        writer.WriteLine($@"Контакты:");
                        writer.WriteLine($@"    Мобильный телефон: {user.Contacts.MobilePhone}");
                        writer.WriteLine($@"    Домашний телефон: {user.Contacts.HomePhone}");
                    }

                    writer.WriteLine();
                    writer.WriteLine($@"Разрешения:");
                    writer.WriteLine($@"    Могу отправить запрос в друзья: " + (user.CanSendFriendRequest ? "Да" : "Нет"));
                    writer.WriteLine($@"    Могу писать в ЛС: " + (user.CanWritePrivateMessage ? "Да" : "Нет"));
                    writer.WriteLine($@"    Могу ли я писать на стене: " + (user.CanPost ? "Да" : "Нет"));
                    writer.WriteLine($@"    Могу видеть чужие посты на стене: " + (user.CanSeeAllPost ? "Да" : "Нет"));
                    writer.WriteLine($@"    Могу видеть аудиозаписи: " + (user.CanSeeAudio ? "Да" : "Нет"));

                    if (user.Careers != null && user.Careers.Count > 0)
                    {
                        writer.WriteLine();
                        writer.WriteLine($@"Карьера:");
                        for (int i = 0; i < user.Careers.Count; i++)
                        {
                            if (i != 0)
                                writer.WriteLine();
                            writer.WriteLine($@"    Компания: {user.Careers[i].Company}");
                            writer.WriteLine($@"    Должность: {user.Careers[i].Position}");
                            writer.WriteLine($@"    Начал(а) работать: {user.Careers[i].From}");
                            writer.WriteLine($@"    Закончил(а) рабоать: {user.Careers[i].Until}");
                        }
                    }

                    if (user.Education != null)
                    {
                        writer.WriteLine();
                        writer.WriteLine($@"Высшее образование:");
                        writer.WriteLine($@"    Название университета: {user.Education.UniversityName}");
                        writer.WriteLine($@"    Название факультета: {user.Education.FacultyName}");
                        writer.WriteLine($@"    Форма обучения: {user.Education.EducationForm}");
                        writer.WriteLine($@"    Статус обучения: {user.Education.EducationStatus}");
                        writer.WriteLine($@"    Год выпуска: {user.Education.Graduation}");
                    }

                    if (user.Relatives != null && user.Relatives.Any())
                    {
                        writer.WriteLine();
                        writer.WriteLine($@"Родственники:");
                        for (int i = 0; i < user.Relatives.Count; i++)
                        {
                            if (i != 0)
                                writer.WriteLine();
                            writer.WriteLine($@"    Идентификатор: {user.Relatives[i].Id}");
                            writer.WriteLine($@"    Имя: {user.Relatives[i].Name}");
                            writer.WriteLine($@"    Родство: {user.Relatives[i].TypeStr}");
                        }
                    }

                    if (user.Friends != null && user.Friends.Any())
                    {
                        writer.WriteLine();
                        writer.WriteLine($@"Друзья:");
                        for (int i = 0; i < user.Friends.Count; i++)
                        {
                            if (i != 0)
                                writer.WriteLine();
                            writer.WriteLine($@"    Идентификатор: {user.Friends[i]}");
                        }
                    }
                }
            }
        }

        private static void WriteUserChanges(User lastUser, User curUser)
        {
            var fullName = $@"id{curUser.Id}";

            string path = @"C:\VKMonitorServiceLogs\Users";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += $@"\{fullName}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string file = path + $@"\{curUser.Name} {curUser.LastName} {DateTime.Now:dd.MM.yyyy hh.mm.ss}.txt";

            using (var writer = new StreamWriter(file, true))
            {
                if (lastUser.Name != curUser.Name)
                    writer.WriteLine($@"Имя: {lastUser.Name} -> {curUser.Name}");

                if (lastUser.LastName != curUser.LastName)
                    writer.WriteLine($@"Фамилия: {lastUser.LastName} -> {curUser.LastName}");

                if (lastUser.MaidenName != null && curUser.MaidenName != null)
                    if (lastUser.MaidenName != curUser.MaidenName)
                        writer.WriteLine($@"Девичья фамилия: {lastUser.MaidenName} -> {curUser.MaidenName}");

                if (lastUser.SexStr != curUser.SexStr)
                    writer.WriteLine($@"Пол: {lastUser.SexStr} -> {curUser.SexStr}");

                if (lastUser.RelationStr != curUser.RelationStr)
                    writer.WriteLine($@"Семейное положение: {lastUser.RelationStr} -> {curUser.RelationStr}");

                if (lastUser.Status != curUser.Status)
                    writer.WriteLine($@"Статус: {lastUser.Status} -> {curUser.Status}");

                if (lastUser.IsBanned != curUser.IsBanned)
                    writer.WriteLine($@"Страница заблокирована: {(lastUser.IsBanned ? "Да" : "Нет")} -> {(curUser.IsBanned ? "Да" : "Нет")}");

                if (lastUser.IsBlackListedByMe != curUser.IsBlackListedByMe)
                    writer.WriteLine($@"Я добавил в ЧС: {(lastUser.IsBlackListedByMe ? "Да" : "Нет")} -> {(curUser.IsBlackListedByMe ? "Да" : "Нет")}");

                if (lastUser.FriendStatusStr != curUser.FriendStatusStr)
                    writer.WriteLine($@"Статус друга: {lastUser.FriendStatusStr} -> {curUser.FriendStatusStr}");

                if (curUser.IsBanned)
                    return;

                if (lastUser.IsBlackListed != curUser.IsBlackListed)
                    writer.WriteLine($@"Я в ЧС: {(lastUser.IsBlackListed ? "Да" : "Нет")} -> {(curUser.IsBlackListed ? "Да" : "Нет")}");

                if (curUser.IsBlackListed)
                    return;

                if (lastUser.IsClosed != curUser.IsClosed)
                    writer.WriteLine($@"Страница закрыта: {(lastUser.IsClosed ? "Да" : "Нет")} -> {(curUser.IsClosed ? "Да" : "Нет")}");
                if (curUser.IsClosed)
                    if(lastUser.CanAccessClosed != curUser.CanAccessClosed)
                        writer.WriteLine($@"Страница закрыта, Я могу видеть закрытую страницу: {(lastUser.CanAccessClosed ? "Да" : "Нет")} -> {(curUser.CanAccessClosed ? "Да" : "Нет")}");

                if (curUser.IsClosed && !curUser.CanAccessClosed)
                    return;

                if (lastUser.PhotoId != curUser.PhotoId)
                    writer.WriteLine($@"Идентификатор авы: {lastUser.PhotoId} -> {curUser.PhotoId}");

                if (lastUser.BirthDateStr != curUser.BirthDateStr)
                    writer.WriteLine($@"День рождения: {lastUser.BirthDateStr} -> {curUser.BirthDateStr}");

                if (lastUser.Country.Title != curUser.Country.Title)
                    writer.WriteLine($@"Страна: {lastUser.Country.Title} -> {curUser.Country.Title}");
                if (lastUser.City.Title != curUser.City.Title)
                    writer.WriteLine($@"Город: {lastUser.City.Title} -> {curUser.City.Title}");
                if (lastUser.HomeCity != curUser.HomeCity)
                    writer.WriteLine($@"Родной город: {lastUser.HomeCity} -> {curUser.HomeCity}");

                writer.WriteLine();
                writer.WriteLine($@"Информация о пользователе:");
                if (lastUser.About != curUser.About)
                    writer.WriteLine($@"    О себе: {lastUser.About} -> {curUser.About}");
                if (lastUser.Activities != curUser.Activities)
                    writer.WriteLine($@"    Деятельность: {lastUser.Activities} -> {curUser.Activities}");
                if (lastUser.Books != curUser.Books)
                    writer.WriteLine($@"    Книги: {lastUser.Books} -> {curUser.Books}");
                if (lastUser.Movies != curUser.Movies)
                    writer.WriteLine($@"    Фильмы: {lastUser.Movies} -> {curUser.Movies}");
                if (lastUser.TV != curUser.TV)
                    writer.WriteLine($@"    Телешоу: {lastUser.TV} -> {curUser.TV}");
                if (lastUser.Music != curUser.Music)
                    writer.WriteLine($@"    Музыка: {lastUser.Music} -> {curUser.Music}");
                if (lastUser.Games != curUser.Games)
                    writer.WriteLine($@"    Игры: {lastUser.Games} -> {curUser.Games}");
                if (lastUser.Interests != curUser.Interests)
                    writer.WriteLine($@"    Интересы: {lastUser.Interests} -> {curUser.Interests}");
                if (lastUser.Quotes != curUser.Quotes)
                    writer.WriteLine($@"    Цитаты: {lastUser.Quotes} -> {curUser.Quotes}");


                if (curUser.Counters != null)
                {
                    writer.WriteLine();
                    writer.WriteLine($@"Счётчики:");
                    if (lastUser.Counters.Friends != curUser.Counters.Friends)
                        writer.WriteLine($@"    Количество друзей: {lastUser.Counters.Friends} -> {curUser.Counters.Friends}");
                    if (lastUser.Counters.MutualFriends != curUser.Counters.MutualFriends)
                        writer.WriteLine($@"    Количество общих друзей: {lastUser.Counters.MutualFriends} -> {curUser.Counters.MutualFriends}");
                    if (lastUser.Counters.Followers != curUser.Counters.Followers)
                        writer.WriteLine($@"    Количество подписчиков: {lastUser.Counters.Followers} -> {curUser.Counters.Followers}");
                    if (lastUser.Counters.Groups != curUser.Counters.Groups)
                        writer.WriteLine($@"    Количество групп: {lastUser.Counters.Groups} -> {curUser.Counters.Groups}");
                    if (lastUser.Counters.Pages != curUser.Counters.Pages)
                        writer.WriteLine($@"    Количество страниц: {lastUser.Counters.Pages} -> {curUser.Counters.Pages}");
                    if (lastUser.Counters.Videos != curUser.Counters.Videos)
                        writer.WriteLine($@"    Количество видео: {lastUser.Counters.Videos} -> {curUser.Counters.Videos}");
                    if (lastUser.Counters.UserVideos != curUser.Counters.UserVideos)
                        writer.WriteLine($@"    Количество видео, на которых отмечен пользователь: {lastUser.Counters.UserVideos} -> {curUser.Counters.UserVideos}");
                    if (lastUser.Counters.Audios != curUser.Counters.Audios)
                        writer.WriteLine($@"    Количество аудиозаписей: {lastUser.Counters.Audios} -> {curUser.Counters.Audios}");
                    if (lastUser.Counters.Albums != curUser.Counters.Albums)
                        writer.WriteLine($@"    Количество открытых альбомов: {lastUser.Counters.Albums} -> {curUser.Counters.Albums}");
                    if (lastUser.Counters.Photos != curUser.Counters.Photos)
                        writer.WriteLine($@"    Количество открытых фото: {lastUser.Counters.Photos} -> {curUser.Counters.Photos}");
                    if (lastUser.Counters.UserPhotos != curUser.Counters.UserPhotos)
                        writer.WriteLine($@"    Количество фото, на которых отмечен пользователь: {lastUser.Counters.UserPhotos} -> {curUser.Counters.UserPhotos}");
                    if (lastUser.Counters.Notes != curUser.Counters.Notes)
                        writer.WriteLine($@"    Количество заметок: {lastUser.Counters.Notes} -> {curUser.Counters.Notes}");
                    if (lastUser.Counters.Gifts != curUser.Counters.Gifts)
                        writer.WriteLine($@"    Количество подарков: {lastUser.Counters.Gifts} -> {curUser.Counters.Gifts}");
                }

                if (curUser.Connections != null)
                {
                    writer.WriteLine();
                    writer.WriteLine($@"Аккаунты на других ресурсах:");
                    if (lastUser.Site != curUser.Site)
                        writer.WriteLine($@"    Сайт: {lastUser.Site} -> {curUser.Site}");
                    if (lastUser.Connections.Instagram != curUser.Connections.Instagram)
                        writer.WriteLine($@"    Инстаграмм: {lastUser.Connections.Instagram} -> {curUser.Connections.Instagram}");
                    if (lastUser.Connections.FacebookId != curUser.Connections.FacebookId)
                        writer.WriteLine($@"    Идентификатор фейсбука: {lastUser.Connections.FacebookId} -> {curUser.Connections.FacebookId}");
                    if (lastUser.Connections.FacebookName != curUser.Connections.FacebookName)
                        writer.WriteLine($@"    Имя на фейсбуке: {lastUser.Connections.FacebookName} -> {curUser.Connections.FacebookName}");
                    if (lastUser.Connections.Twitter != curUser.Connections.Twitter)
                        writer.WriteLine($@"    Твиттер: {lastUser.Connections.Twitter} -> {curUser.Connections.Twitter}");
                    if (lastUser.Connections.Skype != curUser.Connections.Skype)
                        writer.WriteLine($@"    Скайп: {lastUser.Connections.Skype} -> {curUser.Connections.Skype}");
                }

                if (curUser.Exports != null)
                {
                    writer.WriteLine();
                    writer.WriteLine($@"Экспорт записей на другие ресурсы:");
                    if (lastUser.Exports.Instagram != curUser.Exports.Instagram)
                        writer.WriteLine($@"    Инстаграмм: {(lastUser.Exports.Instagram ? "Да" : "Нет")} -> {(curUser.Exports.Instagram ? "Да" : "Нет")}");
                    if (lastUser.Exports.Facebook != curUser.Exports.Facebook)
                        writer.WriteLine($@"    Фейсбук: {(lastUser.Exports.Facebook ? "Да" : "Нет")} -> {(curUser.Exports.Facebook ? "Да" : "Нет")}");
                    if (lastUser.Exports.Livejournal != curUser.Exports.Livejournal)
                        writer.WriteLine($@"    LiveJournal: {(lastUser.Exports.Livejournal ? "Да" : "Нет")} -> {(curUser.Exports.Livejournal ? "Да" : "Нет")}");
                    if (lastUser.Exports.Twitter != curUser.Exports.Twitter)
                        writer.WriteLine($@"    Твиттер: {(lastUser.Exports.Twitter ? "Да" : "Нет")} -> {(curUser.Exports.Twitter ? "Да" : "Нет")}");
                }

                if (curUser.Contacts != null)
                {
                    writer.WriteLine();
                    writer.WriteLine($@"Контакты:");
                    if (lastUser.Contacts.MobilePhone != curUser.Contacts.MobilePhone)
                        writer.WriteLine($@"    Мобильный телефон: {lastUser.Contacts.MobilePhone} -> {curUser.Contacts.MobilePhone}");
                    if (lastUser.Contacts.HomePhone != curUser.Contacts.HomePhone)
                        writer.WriteLine($@"    Домашний телефон: {lastUser.Contacts.HomePhone} -> {curUser.Contacts.HomePhone}");
                }

                writer.WriteLine();
                writer.WriteLine($@"Разрешения:");
                if (lastUser.CanSendFriendRequest != curUser.CanSendFriendRequest)
                    writer.WriteLine($@"    Могу отправить запрос в друзья: {(lastUser.CanSendFriendRequest ? "Да" : "Нет")} -> {(curUser.CanSendFriendRequest ? "Да" : "Нет")}");
                if (lastUser.CanWritePrivateMessage != curUser.CanWritePrivateMessage)
                    writer.WriteLine($@"    Могу писать в ЛС: {(lastUser.CanWritePrivateMessage ? "Да" : "Нет")} -> {(curUser.CanWritePrivateMessage ? "Да" : "Нет")}");
                if (lastUser.CanPost != curUser.CanPost)
                    writer.WriteLine($@"    Могу ли я писать на стене: {(lastUser.CanPost ? "Да" : "Нет")} -> {(curUser.CanPost ? "Да" : "Нет")}");
                if (lastUser.CanSeeAllPost != curUser.CanSeeAllPost)
                    writer.WriteLine($@"    Могу видеть чужие посты на стене: {(lastUser.CanSeeAllPost ? "Да" : "Нет")} -> {(curUser.CanSeeAllPost ? "Да" : "Нет")}");
                if (lastUser.CanSeeAudio != curUser.CanSeeAudio)
                    writer.WriteLine($@"    Могу видеть аудиозаписи: {(lastUser.CanSeeAudio ? "Да" : "Нет")} -> {(curUser.CanSeeAudio ? "Да" : "Нет")}");

                if (lastUser.Careers.Count != curUser.Careers.Count)
                {
                    writer.WriteLine();
                    writer.WriteLine($@"Карьера:");
                    writer.WriteLine($@"    Кол-во работ: {lastUser.Careers.Count} -> {curUser.Careers.Count}");

                    //for (int i = 0; i < curUser.Careers.Count; i++)
                    //{
                    //    if (i != 0)
                    //        writer.WriteLine();
                    //    if (lastUser.Careers[i].Company != curUser.Careers[i].Company)
                    //        writer.WriteLine($@"    Компания: {lastUser.Careers[i].Company} -> {curUser.Careers[i].Company}");
                    //    if (lastUser.Careers[i].Position != curUser.Careers[i].Position)
                    //        writer.WriteLine($@"    Должность: {lastUser.Careers[i].Position} -> {curUser.Careers[i].Position}");
                    //    if (lastUser.Careers[i].From != curUser.Careers[i].From)
                    //        writer.WriteLine($@"    Начал(а) работать: {lastUser.Careers[i].From} -> {curUser.Careers[i].From}");
                    //    if (lastUser.Careers[i].Until != curUser.Careers[i].Until)
                    //        writer.WriteLine($@"    Закончил(а) рабоать: {lastUser.Careers[i].Until} -> {curUser.Careers[i].Until}");
                    //}
                }

                if (curUser.Education != null)
                {
                    writer.WriteLine();
                    writer.WriteLine($@"Высшее образование:");
                    if (lastUser.Education.UniversityName != curUser.Education.UniversityName)
                        writer.WriteLine($@"    Название университета: {lastUser.Education.UniversityName} -> {curUser.Education.UniversityName}");
                    if (lastUser.Education.FacultyName != curUser.Education.FacultyName)
                        writer.WriteLine($@"    Название факультета: {lastUser.Education.FacultyName} -> {curUser.Education.FacultyName}");
                    if (lastUser.Education.EducationForm != curUser.Education.EducationForm)
                        writer.WriteLine($@"    Форма обучения: {lastUser.Education.EducationForm} -> {curUser.Education.EducationForm}");
                    if (lastUser.Education.EducationStatus != curUser.Education.EducationStatus)
                        writer.WriteLine($@"    Статус обучения: {lastUser.Education.EducationStatus} -> {curUser.Education.EducationStatus}");
                    if (lastUser.Education.Graduation != curUser.Education.Graduation)
                        writer.WriteLine($@"    Год выпуска: {lastUser.Education.Graduation} -> {curUser.Education.Graduation}");
                }

                if (lastUser.Relatives.Count != curUser.Relatives.Count)
                {
                    writer.WriteLine();
                    writer.WriteLine($@"Родственники:");
                    writer.WriteLine($@"    Кол-во родственников: {lastUser.Relatives.Count} -> {curUser.Relatives.Count}");
                    //for (int i = 0; i < curUser.Relatives.Count; i++)
                    //{
                    //    if (i != 0)
                    //        writer.WriteLine();
                    //    writer.WriteLine($@"    Идентификатор: {user.Relatives[i].Id}");
                    //    writer.WriteLine($@"    Имя: {user.Relatives[i].Name}");
                    //    writer.WriteLine($@"    Родство: {user.Relatives[i].TypeStr}");
                    //}
                }

                //if (user.Friends != null && user.Friends.Count > 0)
                //{
                //    writer.WriteLine();
                //    writer.WriteLine($@"Друзья:");
                //    for (int i = 0; i < user.Friends.Count; i++)
                //    {
                //        if (i != 0)
                //            writer.WriteLine();
                //        writer.WriteLine($@"    Идентификатор: {user.Friends[i]}");
                //    }
                //}
            }
        }
    }
}
