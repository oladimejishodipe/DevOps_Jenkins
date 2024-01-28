using System;
using System.Collections.Generic;

namespace MeMawKNowsBestCrime.MeMawKnowsBestCrimeData;

public partial class MeMawKnowBestThreeWeightedCrimeDatum
{
    public string? States { get; set; }

    public string? AgencyName { get; set; }

    public string StateCityIdentity { get; set; } = null!;

    public double? WRelTotalOffenseWeighted { get; set; }

    public double? WRelTotalMajorCrimeWeighted { get; set; }

    public double? WRelCrimesAgainstPersonsWeighted { get; set; }

    public double? WRelCrimesAgainstPropertyWeighted { get; set; }

    public double? WRelCrimesAgainstSocietyWeighted { get; set; }

    public double? WRelAssaultOffensesWeighted { get; set; }

    public double? WRelAggravatedAssaultWeighted { get; set; }
    public double? CitySize { get; set; }
    
}
