// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapFeature.cs" company="Freiwillige Feuerwehr Krems/Donau">
//      Freiwillige Feuerwehr Krems/Donau
//      Austraße 33
//      A-3500 Krems/Donau
//      Austria
//      Tel.:   +43 (0)2732 85522
//      Fax.:   +43 (0)2732 85522 40
//      E-mail: office@feuerwehr-krems.at
// 
//      This software is furnished under a license and may be
//      used  and copied only in accordance with the terms of
//      such  license  and  with  the  inclusion of the above
//      copyright  notice.  This software or any other copies
//      thereof   may  not  be  provided  or  otherwise  made
//      available  to  any  other  person.  No  title  to and
//      ownership of the software is hereby transferred.
// 
//      The information in this software is subject to change
//      without  notice  and  should  not  be  construed as a
//      commitment by Freiwillige Feuerwehr Krems/Donau.
// 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace At.FF.Krems.Configuration.Google.StaticMaps.Enums
{
    /// <summary>
    /// A map consists of a set of features, such as roads or parks. The feature types form a category tree, with feature:all as the root.
    /// https://developers.google.com/maps/documentation/javascript/reference#MapTypeStyleFeatureType
    /// </summary>
    public enum MapFeature
    {
        /// <summary>Apply the rule to administrative areas.</summary>
        Administrative,

        /// <summary>Apply the rule to countries.</summary>
        AdministrativeCountry,

        /// <summary>Apply the rule to land parcels.</summary>
        AdministrativeLandParcel,

        /// <summary>Apply the rule to localities.</summary>
        AdministrativeLocality,

        /// <summary>Apply the rule to neighborhoods.</summary>
        AdministrativeNeighborhood,

        /// <summary>Apply the rule to provinces.</summary>
        AdministrativeProvince,

        /// <summary>Apply the rule to all selector types.</summary>
        All,

        /// <summary>Apply the rule to landscapes.</summary>
        Landscape,

        /// <summary>Apply the rule to man made structures.</summary>
        LandscapeManMade,

        /// <summary>Apply the rule to natural features.</summary>
        LandscapeNatural,

        /// <summary>Apply the rule to landcover.</summary>
        LandscapeNaturalLandcover,

        /// <summary>Apply the rule to terrain.</summary>
        LandscapeNaturalTerrain,

        /// <summary>Apply the rule to points of interest.</summary>
        Poi,

        /// <summary>Apply the rule to attractions for tourists.</summary>
        PoiAttraction,

        /// <summary>Apply the rule to businesses.</summary>
        PoiBusiness,

        /// <summary>Apply the rule to government buildings.</summary>
        PoiGovernment,

        /// <summary>Apply the rule to emergency services (hospitals, pharmacies, police, doctors, etc).</summary>
        PoiMedical,

        /// <summary>Apply the rule to parks.</summary>
        PoiPark,

        /// <summary>Apply the rule to places of worship, such as churches, temples, or mosques.</summary>
        PoiPlaceOfWorship,

        /// <summary>Apply the rule to schools.</summary>
        PoiSchool,

        /// <summary>Apply the rule to sports complexes.</summary>
        PoiSportsComplex,

        /// <summary>Apply the rule to all roads.</summary>
        Road,

        /// <summary>Apply the rule to arterial roads.</summary>
        RoadArterial,

        /// <summary>Apply the rule to highways.</summary>
        RoadHighway,

        /// <summary>Apply the rule to controlled-access highways.</summary>
        RoadHighwayControlledAccess,

        /// <summary>Apply the rule to local roads.</summary>
        RoadLocal,

        /// <summary>Apply the rule to all transit stations and lines.</summary>
        Transit,

        /// <summary>Apply the rule to transit lines.</summary>
        TransitLine,

        /// <summary>Apply the rule to all transit stations.</summary>
        TransitStation,

        /// <summary>Apply the rule to airports.</summary>
        TransitStationAirport,

        /// <summary>Apply the rule to bus stops.</summary>
        TransitStationBus,

        /// <summary>Apply the rule to rail stations.</summary>
        TransitStationRail,

        /// <summary>Apply the rule to bodies of water.</summary>
        Water
    }
}
