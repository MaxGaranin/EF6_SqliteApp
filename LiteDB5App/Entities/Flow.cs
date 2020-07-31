using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace LiteDB5App.Entities
{
    [DataContract(Namespace = "IPA")]
    public class Flow
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public FlowDirection FlowDirection { get; set; }

        [DataMember]
        public string NetElementId { get; set; }

        [DataMember]
        public virtual List<FlowState> FlowStates { get; set; }
    }

    public enum FlowDirection
    {
        In,
        Out
    }

    [DataContract(Namespace = "IPA")]
    public class FlowState
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Period Period { get; set; }

        [DataMember]
        public bool IsMain { get; set; }

        public virtual double MainFlowSubstanceValue
        {
            get { throw new System.NotImplementedException(); }
        }
    }

    [DataContract(Namespace = "IPA")]
    public class Period : IComparable<Period>
    {
        [DataMember]
        public DateTime LeftBorder { get; set; }

        [DataMember]
        public DateTime RightBorder { get; set; }

//        public DateTime LastDateTime
//        {
//            get { return RightBorder.AddTicks(-1); }
//        }
        
//        public bool IsContains(DateTime date)
//        {
//            return LeftBorder <= date && RightBorder > date;
//        }

        #region ToString

        public override string ToString()
        {
            #region Одно мгновение

            if (LeftBorder == RightBorder)
                return LeftBorder.ToString(CultureInfo.CurrentCulture);

            #endregion

            #region Факт

            if (LeftBorder == new DateTime(1, 1, 2) && RightBorder == LeftBorder.AddTicks(1))
            {
                return "Факт";
            }

            #endregion

            #region Факт2

            if (LeftBorder == new DateTime(1, 1, 2).AddTicks(1) && RightBorder == LeftBorder.AddTicks(2))
            {
                return "Факт2";
            }

            #endregion

            #region День

            if (LeftBorder.AddDays(1) == RightBorder && LeftBorder.AddTicks(-1).Day != LeftBorder.Day)
            {
                return string.Format("{0}", LeftBorder.ToString("d.MM.yy"));
            }

            #endregion

            #region Месяц

            if (LeftBorder.AddMonths(1) == RightBorder && LeftBorder.AddTicks(-1).Month != LeftBorder.Month)
            {
                return string.Format("{0} '{1}", LeftBorder.ToString("MMM"), LeftBorder.ToString("yy"));
            }

            #endregion

            #region Полугодие

            if (LeftBorder.AddMonths(6) == RightBorder)
            {
                if (LeftBorder.Month == 1 && LeftBorder.AddTicks(-1).Month != LeftBorder.Month)
                {
                    return string.Format("1 п. '{0}", LeftBorder.ToString("yy"));
                }
                if (LeftBorder.Month == 7 && LeftBorder.AddTicks(-1).Month != LeftBorder.Month)
                {
                    return string.Format("2 п. '{0}", LeftBorder.ToString("yy"));
                }
            }

            #endregion

            #region Год

            if (LeftBorder.AddYears(1) == RightBorder && LeftBorder.AddTicks(-1).Year != LeftBorder.Year)
                return string.Format("'{0}", LeftBorder.ToString("yy"));

            #endregion

            #region Все периоды

            if (LeftBorder == new DateTime(1, 1, 2).AddTicks(2) && RightBorder == new DateTime(1, 1, 2).AddTicks(3))
            {
                return "Все периоды";
            }

            #endregion

            #region Произвольный период

            return string.Format("{0} - {1}", LeftBorder.ToShortDateString(), RightBorder.ToShortDateString());

            #endregion
        }

        #endregion

        #region Equals

        protected bool Equals(Period other)
        {
            return LeftBorder.Equals(other.LeftBorder) && RightBorder.Equals(other.RightBorder);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Period) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (LeftBorder.GetHashCode()*397) ^ RightBorder.GetHashCode();
            }
        }

        #endregion

        #region CompareTo

        public int CompareTo(Period other)
        {
            return other == null ? 1 : LeftBorder.CompareTo(other.LeftBorder);
        }

        #endregion

        #region Operators overrides

        public static bool operator <(Period period1, Period period2)
        {
            return period1.LeftBorder < period2.LeftBorder;
        }

        public static bool operator >(Period period1, Period period2)
        {
            return period1.LeftBorder > period2.LeftBorder;
        }

        public static bool operator <=(Period period1, Period period2)
        {
            return period1.LeftBorder <= period2.LeftBorder;
        }

        public static bool operator >=(Period period1, Period period2)
        {
            return period1.LeftBorder >= period2.LeftBorder;
        }

        public static bool operator ==(Period period1, Period period2)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(period1, period2)) return true;

            // If one is null, but not both, return false.
            if (((object)period1 == null) || ((object)period2 == null)) return false;

            return (period1.LeftBorder == period2.LeftBorder && period1.RightBorder == period2.RightBorder);
        }

        public static bool operator !=(Period period1, Period period2)
        {
            return !(period1 == period2);
        }

        #endregion
    }
}