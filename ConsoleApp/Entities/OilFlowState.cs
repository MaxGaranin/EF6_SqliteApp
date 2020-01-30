using System.Runtime.Serialization;

namespace ConsoleApp.Entities
{
    [DataContract(Namespace = "IPA")]
    public class OilFlowState : FlowState
    {
        public OilFlowState()
        {
            Period = new Period();
            FluidModel = FluidModel.CreateDefault();
        }

        [DataMember] public double OilQm3PerDay { get; set; }

        [DataMember] public double WaterQm3PerDay { get; set; }

        [DataMember] public double GasQm3PerDay { get; set; }

        [DataMember] public double TemperatureCPerDay { get; set; }

        [DataMember] public double PressureAtmgPerDay { get; set; }

        [DataMember] public double ParkOilPerDay { get; set; }

        [DataMember] public double ParkLiquidPerDay { get; set; }

        [DataMember] public double ParkGasPerDay { get; set; }

        [DataMember] public double LiquidVelocityPerDay { get; set; }

        [DataMember] public double MinLiquidVelocityPerDay { get; set; }

        [DataMember] public double GasVelocityPerDay { get; set; }

        [DataMember] public double MinGasVelocityPerDay { get; set; }

        [DataMember] public double ErosionVelocity { get; set; }

        [DataMember] public double FluidVelocityPerDay { get; set; }

        [DataMember] public double MinFluidVelocityPerDay { get; set; }

        [DataMember] public double MinPressure { get; set; }

        [DataMember] public double MaxPressure { get; set; }

        [DataMember] public FluidModel FluidModel { get; set; }

        public double LiquidM3PerDay
        {
            get { return OilQm3PerDay + WaterQm3PerDay; }
        }

        [DataMember] public double OilMassTPerDay { get; set; }

        [DataMember] public double WaterMassTPerDay { get; set; }

        public double LiquidMassTPerDay
        {
            get { return OilMassTPerDay + WaterMassTPerDay; }
        }

        [DataMember] public double GasMassTPerDay { get; set; }

        public override double MainFlowSubstanceValue
        {
            get { return LiquidM3PerDay; }
        }
    }
}