namespace TestProject
{
    public interface IInterface<T, U>
    {
        public T Generate();
        
        public U GenerateInstance();
    }
}