using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.Common
{
    public class Codes
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Message { get; set; }
        public string arMessage { get; set; }
        public string afMessage { get; set; }
    }
    public class ResponseCodeHelper
    {
        public List<Codes> ModuleCodes { get; set; }
        public List<Codes> CommonCodes { get; set; }
        public List<Codes> GloAdminCodes { get; set; }

    }

    public static class ResponseCodeHelpers
    {
        public static string SelectedModule { get; set; }
        public static List<Codes> ModuleCodes { get; set; }
        public static List<Codes> CommonCodes { get; set; }
        public static List<Codes> GloCodes { get; set; }

    }
    public static class MessageHelper
    {
        public static Codes ReadModuleCodesMessage(string key, string StatusCode = "400", string RequestStatus = "BadRequest")
        {
            Codes codes = new Codes() { Value = StatusCode, Message = RequestStatus };
            if (ResponseCodeHelpers.ModuleCodes != null && ResponseCodeHelpers.ModuleCodes.Any())
            {
                var moduleCodes = ResponseCodeHelpers.ModuleCodes.Where(x => x.Key.ToLower() == key.ToLower()).FirstOrDefault();
                if (moduleCodes != null && !string.IsNullOrWhiteSpace(moduleCodes.Message))
                {
                    codes.Message = moduleCodes.Message;
                    codes.Value = moduleCodes.Value;
                }
            }
            return codes;
        }

        public static Codes ReadCommonCodesMessage(string key, string StatusCode = "400", string RequestStatus = "BadRequest")
        {
            Codes codes = new Codes() { Value = StatusCode, Message = RequestStatus };
            if (ResponseCodeHelpers.CommonCodes != null && ResponseCodeHelpers.CommonCodes.Any())
            {
                var commonCodes = ResponseCodeHelpers.CommonCodes.Where(x => x.Key.ToLower() == key.ToLower()).FirstOrDefault();
                BindCommonCodes(codes, commonCodes);
            }
            return codes;
        }
        private static void BindCommonCodes(Codes codes, Codes commonCodes)
        {
            if (commonCodes != null && !string.IsNullOrWhiteSpace(commonCodes.Message))
            {
                codes.Key = commonCodes.Key;
                codes.Message = commonCodes.Message;
                codes.Value = string.Format("{0}{1}", ResponseCodeHelpers.SelectedModule, commonCodes.Value);
                //codes.Message = Resources.Test;

                var currentCulture = Thread.CurrentThread.CurrentCulture;

                string lang = currentCulture.Name;
                switch (lang)
                {
                    case "ar":
                        codes.Message = commonCodes.arMessage;
                        break;
                    case "af":
                        codes.Message = commonCodes.afMessage;
                        break;
                    default:
                        codes.Message = commonCodes.Message;
                        break;
                }
                if (string.IsNullOrWhiteSpace(codes.Message))
                {
                    codes.Message = commonCodes.Message;
                }
            }
        }

        public static Codes ReadGlodMessage(string key, string StatusCode = "400", string RequestStatus = "BadRequest")
        {
            Codes codes = new Codes() { Value = StatusCode, Message = RequestStatus };
            if (ResponseCodeHelpers.GloCodes != null && ResponseCodeHelpers.GloCodes.Any())
            {
                Codes commonCodes = new Codes();
                commonCodes = ResponseCodeHelpers.GloCodes.Where(x => x.Key.ToLower() == key.ToLower()).FirstOrDefault();
                BindCommonCodes(codes, commonCodes);
            }
            return codes;
        }

        public static object ReadGlodMessage(object personalInformationIsNotFound)
        {
            throw new NotImplementedException();
        }
    }
}
