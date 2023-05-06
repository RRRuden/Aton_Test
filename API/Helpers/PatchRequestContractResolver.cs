using System.Reflection;
using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Helpers;

public class PatchRequestContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);

        prop.SetIsSpecified += (o, o1) =>
        {
            if (o is PatchDtoBase patchRequest) patchRequest.SetHasProperty(prop.PropertyName);
        };

        return prop;
    }
}