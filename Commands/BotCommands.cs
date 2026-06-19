using System.Linq;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;
using SatelliteBot.Data;

namespace SatelliteBot.Commands
{
    /// <summary>
    /// Обработчик команд бота для работы со спутниками планет.
    /// </summary>
    internal class BotCommands
    {
        /// <summary>
        /// Команда /start - приветствие.
        /// </summary>
        [SlashHandler("/start")]
        public static async Task StartCommand(IBotContext context)
        {
            string msg = "Добро пожаловать в бот спутников планет!\n\n" +
                         "Доступные команды:\n" +
                         "/planets - список планет\n" +
                         "/satellites_название_планеты - спутники планеты\n" +
                         "/info_название_спутника - информация о спутнике\n" +
                         "/all - все спутники";
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Команда /planets - список всех планет.
        /// </summary>
        [SlashHandler("/planets")]
        public static async Task PlanetsCommand(IBotContext context)
        {
            var planets = SatelliteData.GetPlanets();
            string msg = "Планеты Солнечной системы:\n\n";
            for (int i = 0; i < planets.Count; i++)
            {
                msg += $"{i + 1}. {planets[i]}\n";
            }
            msg += "\nИспользуйте /satellites_название для просмотра спутников.";
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Команда /satellites - спутники планеты.
        /// </summary>
        [SlashHandler('_', "/satellites")]
        public static async Task SatellitesCommand(IBotContext context)
        {
            var args = context.GetSlashArgs();
            if (args.Count == 0)
            {
                string helpMsg = "Укажите название планеты.\n" +
                                 "Пример: /satellites_Земля\n\n" +
                                 "Доступные планеты: Меркурий, Венера, Земля, Марс, Юпитер, Сатурн, Уран, Нептун";
                await MessageSender.Send(context, helpMsg);
                return;
            }

            string planetName = args[0];
            var satellites = SatelliteData.GetSatellitesByPlanet(planetName);

            if (satellites.Count == 0)
            {
                string errorMsg = $"Планета \"{planetName}\" не найдена.\n" +
                                  "Доступные планеты: Меркурий, Венера, Земля, Марс, Юпитер, Сатурн, Уран, Нептун";
                await MessageSender.Send(context, errorMsg);
                return;
            }

            string msg = $"*Спутники планеты {planetName}:*\n\n";
            for (int i = 0; i < satellites.Count; i++)
            {
                msg += $"{i + 1}. {satellites[i].Name}\n";
            }
            msg += "\nИспользуйте /info_название для подробной информации.";
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Команда /info - информация о спутнике.
        /// </summary>
        [SlashHandler('_', "/info")]
        public static async Task InfoCommand(IBotContext context)
        {
            var args = context.GetSlashArgs();
            if (args.Count == 0)
            {
                string helpMsg = "Укажите название спутника.\n" +
                                 "Пример: /info_Луна";
                await MessageSender.Send(context, helpMsg);
                return;
            }

            string satelliteName = args[0];
            var satellite = SatelliteData.GetSatelliteByName(satelliteName);

            if (satellite == null)
            {
                string errorMsg = $"Спутник \"{satelliteName}\" не найден.\n" +
                                  "Используйте /all для просмотра всех спутников.";
                await MessageSender.Send(context, errorMsg);
                return;
            }

            string msg = satellite.ToString();
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Команда /all - все спутники.
        /// </summary>
        [SlashHandler("/all")]
        public static async Task AllCommand(IBotContext context)
        {
            string msg = SatelliteData.GetAllSatellitesInfo();
            await MessageSender.Send(context, msg);
        }
    }
}
