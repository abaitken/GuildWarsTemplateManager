namespace TemplateManager
{
    public interface IApplicationInformationService
    {
        string AssemblyTitle { get; }
        string AssemblyVersion { get; }
        string FileVersion { get; }
        string AssemblyConfiguration { get; }
        string AssemblyDescription { get; }
        string AssemblyProduct { get; }
        string AssemblyCopyright { get; }
        string AssemblyCompany { get; }
    }
}