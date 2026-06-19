namespace SatelliteBot.Models
{
    /// <summary>
    /// Модель спутника планеты.
    /// </summary>
    public class Satellite
    {
        /// <summary>
        /// Название спутника.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Название планеты-хозяина.
        /// </summary>
        public string PlanetHost { get; set; }

        /// <summary>
        /// Дата открытия.
        /// </summary>
        public string DiscoveryDate { get; set; }

        /// <summary>
        /// Диаметр в км.
        /// </summary>
        public string Diameter { get; set; }

        /// <summary>
        /// Период обращения.
        /// </summary>
        public string OrbitalPeriod { get; set; }

        /// <summary>
        /// Примечание.
        /// </summary>
        public string Note { get; set; }

        public Satellite(
            string name,
            string planetHost,
            string discoveryDate,
            string diameter,
            string orbitalPeriod,
            string note)
        {
            Name = name;
            PlanetHost = planetHost;
            DiscoveryDate = discoveryDate;
            Diameter = diameter;
            OrbitalPeriod = orbitalPeriod;
            Note = note;
        }

        public override string ToString()
        {
            return $"*{Name}*\n" +
                   $"Планета: {PlanetHost}\n" +
                   $"Дата открытия: {DiscoveryDate}\n" +
                   $"Диаметр: {Diameter}\n" +
                   $"Период обращения: {OrbitalPeriod}\n" +
                   $"Примечание: {Note}";
        }
    }
}
