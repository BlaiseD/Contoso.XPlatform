namespace Contoso.XPlatform.ViewModels.Validatables
{
    public interface IHasItemsSourceValidatable : IValidatable
    {
        void Reload(object entity);
        void Clear();
    }
}
