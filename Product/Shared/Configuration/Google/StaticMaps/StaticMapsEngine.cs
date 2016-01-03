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

            if (request.Zoom != default(int))
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
                            case MapFeature.Road:
                                mapFeature = "road";
                                break;
                            case MapFeature.Landscape:
                                mapFeature = "landscape";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
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
                            case MapElement.Labels:
                                element = "lables";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        styleComponents.Add("element:" + element);
                    }

                    var hue = style.HUE;
                    if (hue != null)
                    {
                        styleComponents.Add("hue:" + hue);
                    }

                    var lightness = style.Lightness;
                    if (lightness != null)
                    {
                        styleComponents.Add("lightness:" + lightness);
                    }


                    var saturation = style.Saturation;
                    if (saturation != null)
                    {
                        styleComponents.Add("saturation:" + saturation);
                    }

                    var gamma = style.Gamma;
                    if (gamma != null)
                    {
                        styleComponents.Add("hue:" + gamma);
                    }

                    bool inverseLightness = style.InverseLightness;
                    if (inverseLightness)
                    {
                        styleComponents.Add("inverse_lightnes:true");
                    }

                    MapVisibility mapVisibility = style.MapVisibility;

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
                                throw new ArgumentOutOfRangeException();
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
    }
}
