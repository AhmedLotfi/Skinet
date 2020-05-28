namespace Core.InterfacesUtilites
{
    public interface IPrimaryKey<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}