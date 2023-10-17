using Slugify;

namespace Yenetta_code.Configurations
{
    public class Slug
    {
        public static string CreateSlug(string originalString)
        {
            var slugHelper = new SlugHelper();
            return slugHelper.GenerateSlug(originalString);
        }
    }
}
