namespace Main.Services
{
    public interface IServiceLocator<T>
    {
        T2 Register<T2>(T2 newService) where T2 : T;
        void Unregister<T2>(T2 service) where T2 : T;
        T2 Get<T2>() where T2 : T;
    }
}