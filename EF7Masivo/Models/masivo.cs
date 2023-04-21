namespace EF7Masivo.Models;

public partial class Masivo
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public string? AnotherDescription { get; set; }
}

public class RepositorioMasivo
{
    public void InsercionMasiva()
    {
        Masivo masivo;
        List<Masivo> masivoList = new List<Masivo>();
        for (int i = 0; i < 1000000; i++)
        {
            masivo = new()
            {
                Description = $"Descripcion {i}",
                Date = DateTime.Now,
                AnotherDescription = $"Another Description {i}",
            };
            masivoList.Add(masivo);
        }
        using var context = new MasivoContext();
        context.Masivo.AddRange(masivoList);
        context.SaveChanges();
    }
}