using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BDMSyncHttpClient
{
	public class JsonClient
	{
		public static TResult Get<TResult>(Uri url, IDictionary<String, String> headers, ICredentials credentials)
			where TResult : class, new()
		{
			String jsonResult = null;
			HttpWebRequest httpWebRequest = WebRequest.CreateHttp(url);
			httpWebRequest.Method = "GET";
			httpWebRequest.Credentials = credentials;
			foreach (KeyValuePair<String, String> keyValuePair in headers)
				httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				using (Stream responseStream = httpWebResponse.GetResponseStream())
					using (StreamReader streamReader = new(responseStream, Encoding.UTF8))
						jsonResult = streamReader.ReadToEnd();
			if (String.IsNullOrWhiteSpace(jsonResult))
				return null;
			return JsonConvert.DeserializeObject<TResult>(jsonResult);
		}

		public static TResult Delete<TResult>(Uri url, IDictionary<String, String> headers, ICredentials credentials)
			where TResult : class, new()
		{
			String jsonResult = null;
			HttpWebRequest httpWebRequest = WebRequest.CreateHttp(url);
			httpWebRequest.Method = "DELETE";
			httpWebRequest.Credentials = credentials;
			foreach (KeyValuePair<String, String> keyValuePair in headers)
				httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			using (Stream responseStream = httpWebResponse.GetResponseStream())
			using (StreamReader streamReader = new(responseStream, Encoding.UTF8))
				jsonResult = streamReader.ReadToEnd();
			if (String.IsNullOrWhiteSpace(jsonResult))
				return null;
			return JsonConvert.DeserializeObject<TResult>(jsonResult);
		}

		public static TResult Post<TResult, TBody>(Uri url, IDictionary<String, String> headers, TBody body, ICredentials credentials)
			where TResult : class, new()
			where TBody : class, new()
		{
			String jsonResult = null;
			HttpWebRequest httpWebRequest = WebRequest.CreateHttp(url);
			httpWebRequest.Method = "PUT";
			httpWebRequest.Credentials = credentials;
			foreach (KeyValuePair<String, String> keyValuePair in headers)
				httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			if (body is not null)
			{
				Byte[] bytesBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
                using Stream requestStream = httpWebRequest.GetRequestStream();
					requestStream.Write(bytesBody, 0, bytesBody.Length);
					requestStream.Close();
            }
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			using (Stream responseStream = httpWebResponse.GetResponseStream())
			using (StreamReader streamReader = new(responseStream, Encoding.UTF8))
				jsonResult = streamReader.ReadToEnd();
			if (String.IsNullOrWhiteSpace(jsonResult))
				return null;
			return JsonConvert.DeserializeObject<TResult>(jsonResult);
		}

		public static TResult Put<TResult, TBody>(Uri url, IDictionary<String, String> headers, TBody body, ICredentials credentials)
			where TResult : class, new()
			where TBody : class, new()
		{
			String jsonResult = null;
			HttpWebRequest httpWebRequest = WebRequest.CreateHttp(url);
			httpWebRequest.Method = "PUT";
			httpWebRequest.Credentials = credentials;
			foreach (KeyValuePair<String, String> keyValuePair in headers)
				httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			if (body is not null)
			{
				Byte[] bytesBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
                using Stream requestStream = httpWebRequest.GetRequestStream();
					requestStream.Write(bytesBody, 0, bytesBody.Length);
					requestStream.Close();
            }
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			using (Stream responseStream = httpWebResponse.GetResponseStream())
			using (StreamReader streamReader = new(responseStream, Encoding.UTF8))
				jsonResult = streamReader.ReadToEnd();
			if (String.IsNullOrWhiteSpace(jsonResult))
				return null;
			return JsonConvert.DeserializeObject<TResult>(jsonResult);
		}

		public static TResult Patch<TResult, TBody>(Uri url, IDictionary<String, String> headers, TBody body, ICredentials credentials)
			where TResult : class, new()
			where TBody : class, new()
		{
			String jsonResult = null;
			HttpWebRequest httpWebRequest = WebRequest.CreateHttp(url);
			httpWebRequest.Method = "PATCH";
			httpWebRequest.Credentials = credentials;
			foreach (KeyValuePair<String, String> keyValuePair in headers)
				httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			if (body is not null)
			{
				Byte[] bytesBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
                using Stream requestStream = httpWebRequest.GetRequestStream();
					requestStream.Write(bytesBody, 0, bytesBody.Length);
					requestStream.Close();
            }
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			using (Stream responseStream = httpWebResponse.GetResponseStream())
			using (StreamReader streamReader = new(responseStream, Encoding.UTF8))
				jsonResult = streamReader.ReadToEnd();
			if (String.IsNullOrWhiteSpace(jsonResult))
				return null;
			return JsonConvert.DeserializeObject<TResult>(jsonResult);
		}
	}
}