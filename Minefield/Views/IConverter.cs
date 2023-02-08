namespace MineField.Views;

public interface IConverter<Tin, Tout>
{
    (bool parsed, Tout value) TryConvert(Tin source);
}