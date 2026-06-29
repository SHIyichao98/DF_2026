using Grasshopper.Kernel;
using System.Drawing;

namespace DF2026.PluginTemplate
{
    public sealed class PluginAssemblyInfo : GH_AssemblyInfo
    {
        public override string Name => PluginConfig.PluginName;

        public override Bitmap Icon => IconLoader.ComponentIcon;

        public override string Description => PluginConfig.PluginDescription;

        public override System.Guid Id => PluginConfig.AssemblyId;

        public override string AuthorName => PluginConfig.AuthorName;

        public override string AuthorContact => PluginConfig.AuthorContact;

        public override string AssemblyVersion =>
            GetType().Assembly.GetName().Version?.ToString() ?? "1.0.0";
    }
}
