﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Settings
{
    public class ReportSettings
    {
        public int NormTimeInMonth { get; }
        public decimal BonusDirector { get; }
        public decimal BonusCoefficient { get; }

        public ReportSettings()
        {

        }

        public ReportSettings(int normTimeInMonth, decimal bonusDirector, decimal bonusCoefficient)
        {
            NormTimeInMonth = normTimeInMonth;
            BonusDirector = bonusDirector;
            BonusCoefficient = bonusCoefficient;
        }
        

        public string ToFile()
        {
            return $"{NormTimeInMonth};{BonusDirector};{BonusCoefficient}";
        }
    }
}