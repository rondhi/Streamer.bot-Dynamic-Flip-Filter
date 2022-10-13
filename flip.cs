using System.Net;
using Newtonsoft.Json;
using System;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class FilterSettings1
{
    [JsonProperty("Camera.Mode")]
    public int CameraMode { get; set; }
}

public class Root1
{
    public string sourceName { get; set; }

    public string filterName { get; set; }

    public string filterKind { get; set; }

    public FilterSettings1 filterSettings { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class FilterSettings2
{
    public string filter { get; set; }

    public string setting_name { get; set; }

    public float setting_float { get; set; }

    public bool custom_duration { get; set; }

    public int duration { get; set; }

    public int easing_match { get; set; }
}

public class Root2
{
    public string sourceName { get; set; }

    public string filterName { get; set; }

    public string filterKind { get; set; }

    public FilterSettings2 filterSettings { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Root3
{
    public string sourceName { get; set; }

    public string filterName { get; set; }
}

public class CPHInline
{
    public bool Execute()
    {
        int Connection = Convert.ToInt32(args["obsConnection"].ToString());
        string SourceName = args["sourceName"].ToString();
        int Duration = Convert.ToInt32(args["duration"].ToString());
        float FlipsAngle = float.Parse(args["flips"].ToString()) * 360;
        string transformFilterName = "Do A Flip " + Guid.NewGuid();
        string moveFilterName = "Do A Flip " + Guid.NewGuid();
        var json1 = new Root1{sourceName = SourceName, filterName = transformFilterName, filterKind = "streamfx-filter-transform", filterSettings = new FilterSettings1{CameraMode = 1, }}; //Raw data 1
        var json2 = new Root2{sourceName = SourceName, filterName = moveFilterName, filterKind = "move_value_filter", filterSettings = new FilterSettings2{filter = transformFilterName, setting_name = "Rotation.Z", setting_float = FlipsAngle, custom_duration = true, duration = Duration, easing_match = 3}}; //Raw data 2
        var json3 = new Root3{sourceName = SourceName, filterName = transformFilterName}; //Raw data 3
        var json4 = new Root3{sourceName = SourceName, filterName = moveFilterName}; //Raw data 4
        CPH.ObsSendRaw("CreateSourceFilter", JsonConvert.SerializeObject(json1), Connection); //Raw command 1
        CPH.ObsSendRaw("CreateSourceFilter", JsonConvert.SerializeObject(json2), Connection); //Raw command 2
        CPH.Wait(Duration); //Wait for the filter to finish
        CPH.ObsSendRaw("RemoveSourceFilter", JsonConvert.SerializeObject(json3), Connection); //Raw command 3
        CPH.ObsSendRaw("RemoveSourceFilter", JsonConvert.SerializeObject(json4), Connection); //Raw command 4
        return true;
    }
}
