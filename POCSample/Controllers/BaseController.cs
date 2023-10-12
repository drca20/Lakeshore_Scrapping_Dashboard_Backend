using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POCSample.Helper;
using POCSampleModel.CustomModels;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using POCSampleModel.RequestModel;
using FluentValidation.Results;
using POCSampleModel.Common;
using POCSampleModel.ResponseModel;

namespace POCSample.Controllers
{
    [EnableCors("CorsApi")]
    public class BaseController : ControllerBase
    {

        [HttpPost("uploadimage")]
        public async Task<string> UploadAsync(IHostingEnvironment _hostingEnvironment, string fileName)
        {
            var fileupload = "";
            if (HttpContext.Request.Form.Files.Count != 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    fileupload = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/uploads/", fileName);

                    using (var fileStream = new FileStream(fileupload, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);

                    }
                }
            }
            return fileupload;
        }
        /// <summary>
        /// fill params from model
        /// </summary>
        /// <param name="model">request params</param>
        /// <returns>dictinonary object </returns>
        //protected List<dynamic> FillParamesFromModel(NewSearchRequestModel model)
        //{
        //    List<dynamic> parameters = new List<dynamic>();
        //    //object[] parameters;
        //    //string[] parameters;
        //    string filters = "";
        //    double pageStart = 1, pageSize = 10;
        //    string sortcolumn = "";
        //    string sortorder = "";
        //    string searchtext = "";

        //    if (model != null)
        //    {
        //        if (!double.TryParse(model.Page.ToString(), out pageStart) || pageStart < -1)
        //        {
        //            throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Invalid Page Start");
        //        }
        //        if (!double.TryParse(model.PageSize.ToString(), out pageSize) || pageSize < -1)
        //        {
        //            throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Invalid Page Size");
        //        }
        //        if (!double.TryParse(model.PageSize.ToString(), out pageSize) || pageSize > 1000)
        //        {
        //            throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Page size must be less than 1000");
        //        }
        //        if (!string.IsNullOrWhiteSpace(model.SortColumn))
        //        {
        //            //TODO: can add condition here  for check allowed column or not
        //            sortcolumn = model.SortColumn.Trim();
        //        }
        //        else
        //        {
        //            sortcolumn = "LastModifiedByDateTime";
        //        }
        //        if (!string.IsNullOrWhiteSpace(model.SortOrder))
        //        {
        //            sortorder = model.SortOrder.Trim();
        //        }
        //        else
        //        {
        //            sortorder =  "desc";
        //        }
        //        if (!string.IsNullOrWhiteSpace(model.SearchText))
        //        {
        //            searchtext =  model.SearchText.Trim();
        //        }
        //        else
        //        {
        //            searchtext = model.SearchText.Trim();
        //            //parameters.Add(Constants.SearchParameters.SearchText, "%");
        //        }
        //        if (!string.IsNullOrWhiteSpace(model.Filters))
        //        {
        //            // check here for fields allowrd or not
        //            string filter = "";
        //            if (model.Filters.IsValidJson())
        //            {
        //                filter = GetFilterConditionFromModel(model.Filters.ToString().Trim());
        //                //var modelfilters = JsonConvert.DeserializeObject<List<FilterRequestModel>>(model.Filters);
        //                //if (modelfilters.Where(s => s.Key.ToLower() == "attributes").Any())
        //                //{
        //                //    var codes = MessageHelper.ReadCommonCodesMessage(CommonResponseCodeConstant.FilterNotAllowedOnAttributeField);
        //                //    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, string.Format(codes.Message), codes.Value);
        //                //}
        //            }
        //            filters =  filter.Trim();
        //        }
        //        else
        //        {
        //           filters = " 1=1 and";
        //        }
        //    }
        //    parameters.Add(model.Page);
        //    parameters.Add(model.PageSize);
        //    parameters.Add(searchtext);
        //    parameters.Add(filters);
        //    parameters.Add(sortcolumn);
        //    parameters.Add(sortorder);
        //    //parameters[0]= model.Page;
        //    //parameters[1]=model.PageSize;
        //    // parameters[2]=filters;
        //    // parameters[3]=sortcolumn;
        //    // parameters[4] = sortorder;
        //    //parameters.add(parameters[0]);
        //    //, parameters[1]};
        //    //parameters = filters;
        //    return parameters;
        //}

        protected NewSearchRequestModel FillParamesFromModel(NewSearchRequestModel model)
        {
            NewSearchRequestModel parameters = new NewSearchRequestModel();

            double pageStart = 1, pageSize = 10;

            if (model != null)
            {
                if (!double.TryParse(model.Page.ToString(), out pageStart) || pageStart < -1)
                {
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Invalid Page Start");
                }
                if (!double.TryParse(model.PageSize.ToString(), out pageSize) || pageSize < -1)
                {
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Invalid Page Size");
                }
                if (!double.TryParse(model.PageSize.ToString(), out pageSize) || pageSize > 1000)
                {
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Page size must be less than 1000");
                }
                if (!string.IsNullOrWhiteSpace(model.SortColumn))
                {
                    //TODO: can add condition here  for check allowed column or not
                    parameters.SortColumn = model.SortColumn.Trim();
                }
                else
                {
                    parameters.SortColumn = "CreatedDateTime";
                }
                if (!string.IsNullOrWhiteSpace(model.SortOrder))
                {
                    parameters.SortOrder = model.SortOrder.Trim();
                }
                else
                {
                    parameters.SortOrder ="desc";
                }
                if (!string.IsNullOrWhiteSpace(model.SearchText))
                {
                    //model.SearchText = ToEscapeXml(model.SearchText);
                    parameters.SearchText = model.SearchText.Trim();
                }
                else
                {
                    parameters.SearchText = "%";
                }
                if (!string.IsNullOrWhiteSpace(model.Filters))
                {
                    // check here for fields allowrd or not
                    string filter = "";
                    if (model.Filters.IsValidJson())
                    {
                        filter = GetFilterConditionFromModel(model.Filters.ToString().Trim());
                        //var modelfilters = JsonConvert.DeserializeObject<List<FilterRequestModel>>(model.Filters);
                        //if (modelfilters.Where(s => s.Key.ToLower() == "attributes").Any())
                        //{
                        //    var codes = MessageHelper.ReadCommonCodesMessage(CommonResponseCodeConstant.FilterNotAllowedOnAttributeField);
                        //    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, string.Format(codes.Message), codes.Value);
                        //}
                    }
                    parameters.Filters=filter.Trim();
                }
                else
                {
                    parameters.Filters=" 1=1 and";
                }
            }
            //if (user_id > 0)
            //{
            //    parameters.Add("logged_in_userid", user_id);
            //}
            parameters.Page=model.Page;
            parameters.PageSize=model.PageSize;
            //    parameters.Add(Constants.SearchParameters.Filters, model.Filters);
            return parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected string GetFilterConditionFromModel(string filter)
        {
            string condition = " ";
            if (filter == null)
            {
                return condition;
            }


            var filterConditions = JsonConvert.DeserializeObject<List<FilterRequestModel>>(filter);

            int i = 0;
            foreach (var item in filterConditions)
            {
                //item.Condition = string.IsNullOrWhiteSpace(item.Condition)?"=" : item.Condition;
                i++;
                if (item.Value != null)
                {
                    item.Value = ToEscapeXml(item.Value.ToString());
                }
                if (!string.IsNullOrWhiteSpace(item.Condition))
                {
                    if (item.Condition.ToLower() == "in")
                    {
                        var data = item.Value.ToString().Split(",");
                        string query = string.Empty;
                        int j = 0;
                        foreach (var val in data)
                        {
                            j++;
                            var intValue = 0;
                            bool isInt = int.TryParse(val, out intValue);
                            if (isInt)
                            {
                                query += intValue;
                            }
                            else
                            {
                                query +=   "'" + val  + "'";
                            }
                            if (j < data.Count())
                            {
                                query += " , ";
                            }
                        }

                        condition += item.Key + " in (" + query + ")";
                    }
                    else if (item.Condition.ToLower() == "nin")
                    {
                        var data = item.Value.ToString().Split(",");
                        string query = string.Empty;
                        int j = 0;
                        foreach (var val in data)
                        {
                            j++;
                            var intValue = 0;
                            bool isInt = int.TryParse(val, out intValue);
                            if (isInt)
                            {
                                query += intValue;
                            }
                            else
                            {
                                query += "'" + val + "'";
                            }
                            if (j < data.Count())
                            {
                                query += " , ";
                            }
                        }

                        condition += item.Key + " not in (" + query + ")";
                    }
                    else if (item.Condition.ToLower() == "between")
                    {
                        var intValue = 0;
                        bool isInt = int.TryParse(item.From.ToString(), out intValue);

                        if (isInt)
                        {
                            condition += item.Key + " between " + item.From + " AND " + item.To;
                        }
                        else
                        {
                            try
                            {
                                if (Convert.ToDateTime(item.From) == Convert.ToDateTime(item.To))
                                {
                                    item.To = Convert.ToDateTime(item.To).AddHours(23.59).ToString("yyyy-MM-dd HH:mm:ss");
                                }

                            }
                            catch (Exception)
                            {

                            }
                            condition += item.Key + " between '" + item.From + "' AND '" + item.To + "'";
                        }
                    }
                    else if (item.Condition.ToLower() == "=")
                    {
                        var data = item.Key.ToString().Split(",");
                        string query = "( ";
                        int j = 0;

                        var intValue = 0;
                        bool isInt = int.TryParse(item.Value.ToString(), out intValue);

                        foreach (var keyVal in data)
                        {
                            j++;

                            if (isInt)
                            {
                                query += keyVal + " = " + intValue;
                            }
                            else
                            {
                                query += keyVal + "  = '" + item.Value + "'";
                            }
                            if (j < data.Count())
                            {
                                query += " OR ";
                            }
                        }
                        condition += query + " ) ";
                    }
                    else if (item.Condition.ToLower() == ">=")
                    {
                        var data = item.Key.ToString().Split(",");
                        string query = "( ";
                        int j = 0;

                        var intValue = 0;
                        bool isInt = int.TryParse(item.Value.ToString(), out intValue);

                        foreach (var keyVal in data)
                        {
                            j++;

                            if (isInt)
                            {
                                query += keyVal + " >= " + intValue;
                            }
                            else
                            {
                                query += keyVal + "  >= '" + item.Value + "'";
                            }
                            if (j < data.Count())
                            {
                                query += " OR ";
                            }
                        }
                        condition += query + " ) ";
                    }
                    else if (item.Condition.ToLower() == ">")
                    {
                        var data = item.Key.ToString().Split(",");
                        string query = "( ";
                        int j = 0;

                        var intValue = 0;
                        bool isInt = int.TryParse(item.Value.ToString(), out intValue);

                        foreach (var keyVal in data)
                        {
                            j++;

                            if (isInt)
                            {
                                query += keyVal + " > " + intValue;
                            }
                            else
                            {
                                query += keyVal + "  > '" + item.Value + "'";
                            }
                            if (j < data.Count())
                            {
                                query += " OR ";
                            }
                        }
                        condition += query + " ) ";
                    }
                    else if (item.Condition.ToLower() == "<=")
                    {
                        var data = item.Key.ToString().Split(",");
                        string query = "( ";
                        int j = 0;

                        var intValue = 0;
                        bool isInt = int.TryParse(item.Value.ToString(), out intValue);

                        foreach (var keyVal in data)
                        {
                            j++;

                            if (isInt)
                            {
                                query += keyVal + " <= " + intValue;
                            }
                            else
                            {
                                query += keyVal + "  <= '" + item.Value + "'";
                            }
                            if (j < data.Count())
                            {
                                query += " OR ";
                            }
                        }
                        condition += query + " ) ";
                    }
                    else if (item.Condition.ToLower() == "<")
                    {
                        var data = item.Key.ToString().Split(",");
                        string query = "( ";
                        int j = 0;

                        var intValue = 0;
                        bool isInt = int.TryParse(item.Value.ToString(), out intValue);

                        foreach (var keyVal in data)
                        {
                            j++;

                            if (isInt)
                            {
                                query += keyVal + " < " + intValue;
                            }
                            else
                            {
                                query += keyVal + "  < '" + item.Value + "'";
                            }
                            if (j < data.Count())
                            {
                                query += " OR ";
                            }
                        }
                        condition += query + " ) ";
                    }
                    else
                    {
                        throw new HttpStatusCodeException(StatusCodes.Status400BadRequest);
                    }
                }
                else
                {
                    // (key = 1 or key like '%test%')

                    var data = item.Key.ToString().Split(",");
                    string query = "( ";
                    int j = 0;

                    var intValue = 0;
                    bool isInt = int.TryParse(item.Value.ToString(), out intValue);

                    if (!string.IsNullOrWhiteSpace(item.Type) && item.Type != typeof(int).ToString())
                    {
                        isInt = false;

                    }

                    foreach (var keyVal in data)
                    {
                        j++;

                        if (isInt)
                        {
                            query += keyVal + " = " + intValue;
                        }
                        else
                        {
                            query += keyVal + "  LIKE (\'%" + item.Value + "%\')";
                        }
                        if (j < data.Count())
                        {
                            query += " OR ";
                        }
                    }
                    condition += query + " ) ";

                }


                condition += " AND ";

            }
            return condition;
        }
        [NonAction]
        public JWTHelper GetUserDetails()
        {

            var t = JsonConvert.DeserializeObject<JWTHelper>(HttpContext.User.FindFirst("AccountData").Value);
            return t;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [NonAction]
        public string ToEscapeXml(string s)
        {
            string escapeString = s;
            if (!string.IsNullOrWhiteSpace(escapeString))
            {
                escapeString = escapeString.Replace("&", "&amp;");
                //escapeString = escapeString.Replace("'", "&apos;");
                escapeString = escapeString.Replace("'", "''");
                escapeString = escapeString.Replace("\"", "&quot;");
                escapeString = escapeString.Replace(">", "&gt;");
                escapeString = escapeString.Replace("<", "&lt;");
                escapeString = escapeString.Replace("[", "[[");
                escapeString = escapeString.Replace("]", "]]");
            }
            return escapeString;
        }

        [NonAction]
        private Dictionary<string, string> GetFormData()
        {
            return Request.Form.Keys.Cast<string>().ToDictionary(key => key, key => Convert.ToString(Request.Form[key]));
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public Dictionary<string, string> GetHeaderData()
        {
            return Request.Headers.Keys.Cast<string>().ToDictionary(key => key, key => Convert.ToString(Request.Headers[key]));
        }

        internal ActionResult UpdateResult<T>(T result)
        {
            if (result != null)
            {
                if (result.ToString().Contains("url"))
                    return Ok(SetUrl(result));
                else
                    return Ok(result);
            }
            return NotFound();
        }

        internal ActionResult UpdateResult(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                if (result.ToString().Contains("url"))
                    return Ok(SetUrl(result));
                else
                    return Ok(JsonConvert.DeserializeObject(result));
            }
            return NotFound();
        }

        internal ActionResult GetResult<T>(T result)
        {
            if (result != null)
            {
                if (result.ToString().Contains("url"))
                    return Ok(SetUrl(result));
                else
                    return Ok(result);
            }
            return NotFound();
        }

        internal ActionResult GetResult(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                if (result.Contains("url"))
                    return Ok((SetUrl(result)));
                else
                    return Ok(JsonConvert.DeserializeObject(result));
            }
            return NotFound();
        }

        internal ActionResult CreatedResult<T>(T result)
        {
            if (result.ToString().Contains("url"))
                return StatusCode(StatusCodes.Status201Created, SetUrl(result, true));
            else
                return StatusCode(StatusCodes.Status201Created, result);
        }

        internal ActionResult CreatedResult(string result)
        {
            if (result.Contains("url"))
                return StatusCode(StatusCodes.Status201Created, SetUrl(result, true));
            else
                return StatusCode(StatusCodes.Status201Created, JsonConvert.DeserializeObject(result));
        }

        internal ActionResult DeletedResult(string result)
        {
            return StatusCode(StatusCodes.Status204NoContent, result);
        }

        private dynamic SetUrl(string result, bool insert = false)
        {
            dynamic responsevar = JsonConvert.DeserializeObject(result.ToString());
            BindDynamicUrl(responsevar, insert);
            return responsevar;
        }

        private dynamic SetUrl<T>(T result, bool insert = false)
        {
            dynamic responsevar = result;
            BindDynamicUrl(responsevar, insert);
            return responsevar;
        }


        internal ActionResult FluentValidationResult(ValidationResult validationResult)
        {
            FluentErrorResponse errorResponse = new FluentErrorResponse();
            errorResponse.Code = StatusCodes.Status400BadRequest;
            errorResponse.Status = StatusCodes.Status400BadRequest;
            JObject jObject = new JObject();
            foreach (var item in validationResult.Errors)
            {
                if (!jObject.ContainsKey(item.PropertyName))
                    jObject.Add(item.PropertyName, new JArray() { item.ErrorMessage });
            }
            errorResponse.Errors = jObject;
            errorResponse.isFluentError = true;
            throw new Exception(JsonConvert.SerializeObject(errorResponse));
        }
        private void BindDynamicUrl(dynamic responsevar, bool insert = false)
        {
            string httpstr = "http://";
            if (HttpContext.Request.IsHttps)
                httpstr = "https://";
            if (insert)
                responsevar.url = httpstr + HttpContext.Request.Host.ToString() + HttpContext.Request.Path.ToString().TrimEnd('/') + "/" + responsevar.url;
            else
                responsevar.url = httpstr + HttpContext.Request.Host.ToString() + HttpContext.Request.Path.ToString();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [NonAction]
        public SearchPage<T> BindSearchResult<T>(SearchPage<T> list, SearchRequestModel model, string key)
        {
            if (list.Meta.NextPageExists)
                list.Meta.NextPageUrl = HttpUtility.UrlDecode(HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page + 1 + ""));
            if (model.Page > 1)
                list.Meta.PreviousPageUrl = HttpUtility.UrlDecode(HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page - 1 + ""));
            list.Meta.Url = HttpUtility.UrlDecode(HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page + ""));
            list.Meta.FirstPageUrl = HttpUtility.UrlDecode(HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", 1 + ""));
            list.Meta.Key = key;
            if (list.Meta.TotalResults > 0)
            {
                if (list.Meta.PageSize != 0)
                    list.Meta.TotalPages = (int)Math.Ceiling((double)list.Meta.TotalResults / list.Meta.PageSize);
                else
                    list.Meta.TotalPages = (int)list.Meta.TotalResults;
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [NonAction]
        public Page BindSearchResult(Page list, SearchRequestModel model, string key)
        {
            list.Meta.FirstPageUrl = HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", 1 + "");
            list.Meta.Url = HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page + "");
            list.Meta.Page = model.Page;
            list.Meta.PageSize = model.PageSize;
            if (list.Meta.TotalResults > 0)
            {
                if (list.Meta.TotalResults > (model.Page * model.PageSize))
                {
                    list.Meta.NextPageUrl = HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page + 1 + "");
                }
                if (model.Page > 1)
                    list.Meta.PreviousPageUrl = HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page - 1 + "");

                list.Meta.TotalPages = (int)Math.Ceiling((double)list.Meta.TotalResults / model.PageSize);

            }
            list.Meta.Key = key;
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [NonAction]
        public ConnectionPage BindSearchResult(ConnectionPage list, SearchRequestModel model, string key)
        {
            list.Meta.FirstPageUrl = HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", 1 + "");
            list.Meta.Url = HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page + "");
            list.Meta.Page = model.Page;
            list.Meta.PageSize = model.PageSize;
            if (list.Meta.TotalResults > 0)
            {
                if (list.Meta.TotalResults > (model.Page * model.PageSize))
                {
                    list.Meta.NextPageUrl = HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page + 1 + "");
                }
                if (model.Page > 1)
                    list.Meta.PreviousPageUrl = HttpContext.Request.HttpContext.AddOrReplaceQueryParameter("page", model.Page - 1 + "");

                list.Meta.TotalPages = (int)Math.Ceiling((double)list.Meta.TotalResults / model.PageSize);

            }
            list.Meta.Key = key;
            return list;
        }


        internal T GetPatchObject<T>(string existsDetail, JObject body)
        {
            var data = (JObject)JsonConvert.DeserializeObject(existsDetail);
            data.Merge(body, new JsonMergeSettings { MergeNullValueHandling = MergeNullValueHandling.Merge });
            return data.ToObject<T>();
        }




    }
}
