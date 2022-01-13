using EveStats.Data.Resources;

namespace EveStats.Service.Helpers.Web
{
    public class ESIScopeListGenerator
    {

        public static string[] ListOfScopes()
        {
            string scopes = APIConstants.ESIScopes;

            return scopes.Split(' ');
        }
    }
}
