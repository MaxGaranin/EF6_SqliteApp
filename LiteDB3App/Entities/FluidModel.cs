using System;
using System.Runtime.Serialization;

namespace LiteDB3App.Entities
{
    [DataContract(Namespace = "IPA")]
    public class FluidModel
    {
        public FluidModel()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
        }

        #region Create Default model

        /// <summary>
        /// Создание модели по умолчанию с DefaultModelId
        /// </summary>
        public static FluidModel CreateDefault()
        {
            var model = CreateBase();
            model.Id = Guid.NewGuid();
            return model;
        }

        /// <summary>
        /// Создание базовой модели с заполненными свойствами
        /// </summary>
        public static FluidModel CreateBase()
        {
            var baseModel = new FluidModel
            {
                OilSg = 0.876, // относительные единицы
                WaterSg = 1.02,
                GasSg = 0.64,
                OilHeatCapacity = 1884.0542, // Дж/кг/К
                WaterHeatCapacity = 4186.787,
                GasHeatCapacity = 2302.7329,
                OilHeatConductivity = 0.1384, // Вт/м/К
                WaterHeatConductivity = 0.6055,
                GasHeatConductivity = 0.0346,
                OilFreezingTemperature = 0,
            };

            return baseModel;
        }

        #endregion

        #region Common

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Относительная плотность нефти 
        /// </summary>
        [DataMember]
        public double OilSg { get; set; }

        /// <summary>
        /// Относительная плотность воды
        /// </summary>
        [DataMember]
        public double WaterSg { get; set; }

        /// <summary>
        /// Относительная плотность газа
        /// </summary>
        [DataMember]
        public double GasSg { get; set; }

        /// <summary>
        /// Удельная теплоемкость нефти
        /// </summary>
        [DataMember]
        public double OilHeatCapacity { get; set; }

        /// <summary>
        /// Удельная теплоемкость воды
        /// </summary>
        [DataMember]
        public double WaterHeatCapacity { get; set; }

        /// <summary>
        /// Удельная теплоемкость газа
        /// </summary>
        [DataMember]
        public double GasHeatCapacity { get; set; }

        /// <summary>
        /// Теплопроводность нефти
        /// </summary>
        [DataMember]
        public double OilHeatConductivity { get; set; }

        /// <summary>
        /// Теплопроводность воды
        /// </summary>
        [DataMember]
        public double WaterHeatConductivity { get; set; }

        /// <summary>
        /// Теплопроводность газа
        /// </summary>
        [DataMember]
        public double GasHeatConductivity { get; set; }

        /// <summary>
        /// Температура начала застывания нефти
        /// </summary>
        [DataMember]
        public double OilFreezingTemperature { get; set; }

        #endregion
    }
}