using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace POCSampleModel.Common
{
    public static class CommonHelper
    {
        public static T GetPatchObject<T>(string existsDetail, JObject body)
        {
            var data = (JObject)JsonConvert.DeserializeObject(existsDetail);
            data.Merge(body, new JsonMergeSettings { MergeNullValueHandling = MergeNullValueHandling.Ignore });
            return data.ToObject<T>();
        }
        public static string DictionaryToXml(Dictionary<string, object> dic, string rootElement = "Root")
        {
            string strXMLResult = string.Empty;

            if (dic != null && dic.Count > 0)
            {
                foreach (KeyValuePair<string, object> pair in dic)
                {
                    strXMLResult += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }

                strXMLResult = "<" + rootElement + ">" + strXMLResult + "</" + rootElement + ">";
            }

            return strXMLResult;
        }
        public static string GenerateUniqueSID(string prefix)
        {
            prefix.ToUpper();
            return (prefix + Guid.NewGuid().ToString()).ToUpper();
        }
        public static T1 ToDocumentData<T, T1>(this T model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, T1>();
            });
            IMapper iMapper = config.CreateMapper();
            T1 doc = iMapper.Map<T, T1>(model);
            return doc;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray()).ToLower();
        }

        public static string ParseAPIResponse(CommonApiResponse apiResponse)
        {
            var apiResult = string.Empty;
            var ResponseObj = string.Empty;
            if (apiResponse != null)
            {
                if (apiResponse.Content != null && !string.IsNullOrWhiteSpace(apiResponse.Content.ToString()))
                {
                    if (apiResponse.StatusCode == StatusCodes.Status200OK || apiResponse.StatusCode == StatusCodes.Status201Created)
                    {
                        apiResult = apiResponse.Content.ToString();
                        ResponseObj = apiResult.ToString();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(apiResponse.Errormessage))
                            throw new HttpStatusCodeException(apiResponse.ErrorCode, apiResponse.Errormessage, apiResponse.ErrorCode.ToString());
                        else
                            throw new HttpStatusCodeException(apiResponse.ErrorCode, "Internal Server Error", apiResponse.ErrorCode.ToString());
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(apiResponse.Errormessage))
                    {
                        var errorResponse = JsonConvert.DeserializeObject<NotFoundAPIResponse>(apiResponse.Errormessage);
                        if (errorResponse != null && !string.IsNullOrWhiteSpace(errorResponse.message))
                        {
                            throw new HttpStatusCodeException(errorResponse.status, errorResponse.message, errorResponse.code.ToString());
                        }
                        else
                        {
                            throw new HttpStatusCodeException(apiResponse.ErrorCode, apiResponse.Errormessage, apiResponse.ErrorCode.ToString());
                        }

                    }

                    else
                        throw new HttpStatusCodeException(apiResponse.StatusCode, "Internal Server Error", apiResponse.ErrorCode.ToString());
                }
            }

            return ResponseObj;
        }

        public static bool IsValidJson(this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }
            var value = stringValue.Trim();
            if ((value.StartsWith("{") && value.EndsWith("}")) || //For object
                (value.StartsWith("[") && value.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }




    }
}
