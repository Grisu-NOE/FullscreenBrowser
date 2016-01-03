// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaticMapsEngine.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Configuration.Google.StaticMaps
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using At.FF.Krems.Configuration.Google.StaticMaps.Entities;
    using At.FF.Krems.Configuration.Google.StaticMaps.Enums;

    /// <summary>
    /// Creates a URL to google's static map according to propery filled up StaticMapsRequest
    /// http://code.google.com/apis/maps/documentation/staticmaps/
    /// </summary>
    public class StaticMapsEngine
    {
        protected static readonly string BaseUrl;

        static StaticMapsEngine()
        {
            BaseUrl = @"maps.google.com/maps/api/staticmap";
        }

        public string GenerateStaticMapUrl(StaticMapRequest request)
        {
            var scheme = request.IsSSL ? "https://" : "http://";

            var parametersList = new QueryStringParametersList();

            if (request.Center != null)
            {
                var center = request.Center;

                var centerLocation = center.LocationString;

                parametersList.Add("center", centerLocation);
            }

            if (request.Zoom != default(byte))
            {
                parametersList.Add("zoom", request.Zoom.ToString());
            }

            if (request.Size.Width != default(int) || request.Size.Height != default(int))
            {
                var imageSize = request.Size;

                parametersList.Add("size", $"{imageSize.Width}x{imageSize.Height}");
            }
            else
            {
                throw new ArgumentException("Size is invalid");
            }

            if (request.ImageFormat != default(ImageFormat))
            {
                string format;
                switch (request.ImageFormat)
                {
                    case ImageFormat.Png8:
                        format = "png8";
                        break;
                    case ImageFormat.Png32:
                        format = "png32";
                        break;
                    case ImageFormat.Gif:
                        format = "gif";
                        break;
                    case ImageFormat.Jpg:
                        format = "jpg";
                        break;
                    case ImageFormat.JpgBaseline:
                        format = "jpg-baseline";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(request.ImageFormat));
                }

                parametersList.Add("format", format);
            }

            if (request.MapType != null)
            {
                string type;
                switch (request.MapType)
                {
                    case MapType.Roadmap:
                        type = "roadmap";
                        break;
                    case MapType.Satellite:
                        type = "satellite";
                        break;
                    case MapType.Terrain:
                        type = "terrain";
                        break;
                    case MapType.Hybrid:
                        type = "hybrid";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(request.MapType));
                }

                parametersList.Add("maptype", type);
            }

            if (request.Style != null)
            {
                foreach (var style in request.Style)
                {
                    var styleComponents = new List<string>();

                    if (style.MapFeature != default(MapFeature))
                    {
                        string mapFeature;

                        switch (style.MapFeature)
                        {
                            case MapFeature.All:
                                mapFeature = "all";
                                break;
                            case MapFeature.Administrative:
                                mapFeature = "administrative";
                                break;
                            case MapFeature.AdministrativeCountry:
                                mapFeature = "administrative.country";
                                break;
                            case MapFeature.AdministrativeLandParcel:
                                mapFeature = "administrative.land_parcel";
                                break;
                            case MapFeature.AdministrativeLocality:
                                mapFeature = "administrative.locality";
                                break;
                            case MapFeature.AdministrativeNeighborhood:
                                mapFeature = "administrative.neighborhood";
                                break;
                            case MapFeature.AdministrativeProvince:
                                mapFeature = "administrative.province";
                                break;
                            case MapFeature.Landscape:
                                mapFeature = "landscape";
                                break;
                            case MapFeature.LandscapeManMade:
                                mapFeature = "landscape.man_made";
                                break;
                            case MapFeature.LandscapeNatural:
                                mapFeature = "landscape.natural";
                                break;
                            case MapFeature.LandscapeNaturalLandcover:
                                mapFeature = "landscape.natural.landcover";
                                break;
                            case MapFeature.LandscapeNaturalTerrain:
                                mapFeature = "landscape.natural.terrain";
                                break;
                            case MapFeature.Poi:
                                mapFeature = "poi";
                                break;
                            case MapFeature.PoiAttraction:
                                mapFeature = "poi.attraction";
                                break;
                            case MapFeature.PoiBusiness:
                                mapFeature = "poi.business";
                                break;
                            case MapFeature.PoiGovernment:
                                mapFeature = "poi.government";
                                break;
                            case MapFeature.PoiMedical:
                                mapFeature = "poi.medical";
                                break;
                            case MapFeature.PoiPark:
                                mapFeature = "poi.park";
                                break;
                            case MapFeature.PoiPlaceOfWorship:
                                mapFeature = "poi.place_of_worship";
                                break;
                            case MapFeature.PoiSchool:
                                mapFeature = "poi.school";
                                break;
                            case MapFeature.PoiSportsComplex:
                                mapFeature = "poi.sports_complex";
                                break;
                            case MapFeature.Road:
                                mapFeature = "road";
                                break;
                            case MapFeature.RoadArterial:
                                mapFeature = "road.arterial";
                                break;
                            case MapFeature.RoadHighway:
                                mapFeature = "road.highway";
                                break;
                            case MapFeature.RoadHighwayControlledAccess:
                                mapFeature = "road.highway.controlled_access";
                                break;
                            case MapFeature.RoadLocal:
                                mapFeature = "road.local";
                                break;
                            case MapFeature.Transit:
                                mapFeature = "transit";
                                break;
                            case MapFeature.TransitLine:
                                mapFeature = "transit.line";
                                break;
                            case MapFeature.TransitStation:
                                mapFeature = "transit.station";
                                break;
                            case MapFeature.TransitStationAirport:
                                mapFeature = "transit.station.airport";
                                break;
                            case MapFeature.TransitStationBus:
                                mapFeature = "transit.station.bus";
                                break;
                            case MapFeature.TransitStationRail:
                                mapFeature = "transit.station.rail";
                                break;
                            case MapFeature.Water:
                                mapFeature = "water";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(style.MapFeature));
                        }

                        styleComponents.Add("feature:" + mapFeature);
                    }

                    if (style.MapElement != default(MapElement))
                    {
                        string element;

                        switch (style.MapElement)
                        {
                            case MapElement.All:
                                element = "all";
                                break;
                            case MapElement.Geometry:
                                element = "geometry";
                                break;
                            case MapElement.GeometryFill:
                                element = "geometry.fill";
                                break;
                            case MapElement.GeometryStroke:
                                element = "geometry.stroke";
                                break;
                            case MapElement.Labels:
                                element = "lables";
                                break;
                            case MapElement.LabelsIcon:
                                element = "labels.icon";
                                break;
                            case MapElement.LabelsText:
                                element = "labels.text";
                                break;
                            case MapElement.LabelsTextFill:
                                element = "labels.text.fill";
                                break;
                            case MapElement.LabelsTextStroke:
                                element = "labels.text.stroke";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(style.MapElement));
                        }

                        styleComponents.Add("element:" + element);
                    }
                    
                    if (style.HUE != default(Color))
                    {
                        styleComponents.Add("hue:0x" + ColorToHexConverter(style.HUE));
                    }

                    var lightness = style.Lightness;
                    if (lightness != null)
                    {
                        if (lightness < -100 || lightness > 100)
                        {
                            throw new ArgumentOutOfRangeException(nameof(style.Lightness));
                        }

                        styleComponents.Add("lightness:" + lightness);
                    }


                    var saturation = style.Saturation;
                    if (saturation != null)
                    {
                        if (saturation < -100 || saturation > 100)
                        {
                            throw new ArgumentOutOfRangeException(nameof(style.Saturation));
                        }

                        styleComponents.Add("saturation:" + saturation);
                    }

                    var gamma = style.Gamma;
                    if (gamma != null && gamma != 1.0)
                    {
                        if (gamma < 0.01 || gamma > 10.0)
                        {
                            throw new ArgumentOutOfRangeException(nameof(style.Gamma));
                        }

                        styleComponents.Add("gamma:" + gamma);
                    }

                    var inverseLightness = style.InvertLightness;
                    if (inverseLightness)
                    {
                        styleComponents.Add("inverse_lightnes:true");
                    }

                    if (style.Weight != default(int))
                    {
                        if (style.Weight < 0)
                        {
                            throw new ArgumentOutOfRangeException(nameof(style.Weight));
                        }

                        styleComponents.Add("weight:" + style.Weight);
                    }

                    if (style.Color != default(Color))
                    {
                        styleComponents.Add("color:0x" + ColorToHexConverter(style.Color));
                    }

                    var mapVisibility = style.Visibility;

                    if (mapVisibility != default(MapVisibility))
                    {
                        string visibility;

                        switch (mapVisibility)
                        {
                            case MapVisibility.On:
                                visibility = "on";
                                break;
                            case MapVisibility.Off:
                                visibility = "off";
                                break;
                            case MapVisibility.Simplified:
                                visibility = "simplified";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(style.Visibility));
                        }

                        styleComponents.Add("visibility:" + visibility);
                    }

                    parametersList.Add("style", string.Join("|", styleComponents));
                }
            }

            var markers = request.Markers;

            if (markers != null)
            {
                foreach (var marker in markers)
                {
                    var markerStyleParams = new List<string>();

                    var markerStyle = marker.Style;
                    if (markerStyle != null)
                    {
                        if (string.IsNullOrWhiteSpace(markerStyle.Color))
                        {
                            throw new ArgumentException("Marker style color can't be empty");
                        }

                        markerStyleParams.Add("color:" + markerStyle.Color);

                        if (!string.IsNullOrWhiteSpace(markerStyle.Label))
                        {
                            markerStyleParams.Add("label:" + markerStyle.Label);
                        }

                        if (markerStyle.Size != default(MarkerSize))
                        {
                            switch (markerStyle.Size)
                            {
                                case MarkerSize.Mid:
                                    markerStyleParams.Add("size:mid");
                                    break;
                                case MarkerSize.Tiny:
                                    markerStyleParams.Add("size:tiny");
                                    break;
                                case MarkerSize.Small:
                                    markerStyleParams.Add("size:small");
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }

                    var styleString = string.Join("|", markerStyleParams);

                    var locations = string.Join("|", marker.Locations.Select(location => location.LocationString));

                    parametersList.Add("markers", $"{styleString}|{locations}");
                }
            }

            var pathes = request.Pathes;

            if (pathes != null)
            {
                foreach (var path in pathes)
                {
                    var pathStyleParams = new List<string>();

                    var pathStyle = path.Style;

                    if (pathStyle != null)
                    {
                        if (string.IsNullOrWhiteSpace(pathStyle.Color))
                        {
                            throw new ArgumentException("Path style color can't be empty");
                        }

                        pathStyleParams.Add("color:" + pathStyle.Color);

                        if (!string.IsNullOrWhiteSpace(pathStyle.FillColor))
                        {
                            pathStyleParams.Add("fillcolor:" + pathStyle.FillColor);
                        }

                        if (pathStyle.Weight != default(int))
                        {
                            pathStyleParams.Add("weight:" + pathStyle.Weight);
                        }
                    }

                    var styleString = string.Join("|", pathStyleParams);

                    var locations = string.Join("|", path.Locations.Select(location => location.LocationString));

                    parametersList.Add("path", $"{styleString}|{locations}");
                }
            }

            parametersList.Add("sensor", request.Sensor ? "true" : "false");

            return scheme + BaseUrl + "?" + parametersList.GetQueryStringPostfix();
        }

        /// <summary>Convert <see cref="Color"/> to hex <see cref="string"/>.</summary>
        /// <param name="color">The color for conversion.</param>
        /// <returns>The <see cref="Color"/> as hex <see cref="string"/> in the format RRGGBB.</returns>
        private static string ColorToHexConverter(Color color)
        {
            return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}
