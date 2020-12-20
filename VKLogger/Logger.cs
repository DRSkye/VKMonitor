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

        private static void WriteUser(User user)
        {
            WriteUserData(user);
            //WriteUserChanges(user);
        }

        private static void WriteUserData(User user)
        {
            var fullName = $@"id{user.Id} {user.Name} {user.LastName}";

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

            string file = path + $@"\{fullName} {DateTime.Now:dd.MM.yyyy hh.mm.ss}.txt";

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
                    writer.WriteLine(user.BirthDateStr);

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

                    if (user.Relatives != null && user.Relatives.Count > 0)
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

                    if (user.Friends != null && user.Friends.Count > 0)
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

        private static void WriteUserChanges(User user)
        {
            var fullName = $@"{user.Domain} {user.Name} {user.LastName}";
            string file = Path.Combine(Directory.GetCurrentDirectory(), $@"\Changes\{fullName}.txt");

            using (var writer = new StreamWriter(file, true))
            {
                
            }
        }
    }
}
