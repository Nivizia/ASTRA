using System.Collections.Generic;
using System.Linq;

namespace DiamondAPI.Services
{
    public class VNPayLibrary
    {
        private readonly SortedList<string, string> _requestData = new SortedList<string, string>();
        private readonly SortedList<string, string> _responseData = new SortedList<string, string>();

        public void AddRequestData(string key, string value)
        {
            if (!_requestData.ContainsKey(key))
            {
                _requestData.Add(key, value);
            }
        }

        public string CreateRequestUrl(string baseUrl, string hashSecret)
        {
            string queryString = string.Join("&", _requestData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            string hash = Utils.HmacSHA512(hashSecret, queryString);
            return $"{baseUrl}?{queryString}&vnp_SecureHash={hash}";
        }

        public void AddResponseData(string key, string value)
        {
            if (!_responseData.ContainsKey(key))
            {
                _responseData.Add(key, value);
            }
        }

        public string GetResponseData(string key)
        {
            return _responseData.ContainsKey(key) ? _responseData[key] : string.Empty;
        }

        public bool ValidateSignature(string receivedHash, string hashSecret)
        {
            string responseRaw = string.Join("&", _responseData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            string computedHash = Utils.HmacSHA512(hashSecret, responseRaw);
            return receivedHash.Equals(computedHash, System.StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
