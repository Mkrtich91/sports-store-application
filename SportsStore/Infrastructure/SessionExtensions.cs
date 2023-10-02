using Newtonsoft.Json;

namespace SportsStore.Infrastructure
{
  public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
#pragma warning disable IDE0034
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
#pragma warning restore IDE0034
        }
    }
}
