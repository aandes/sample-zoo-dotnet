using System.Web;
using System.Web.Optimization;

namespace sample_zoo_dotnet
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/styles/css").Include(
                      "~/Assets/css/global.css",
                      "~/Assets/css/components.css",
                      "~/Assets/css/view/index.css"));
        }
    }
}
