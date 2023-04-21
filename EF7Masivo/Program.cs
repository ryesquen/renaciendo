
using EF7Masivo.Models;
using Microsoft.EntityFrameworkCore;

var repositorio = new RepositorioMasivo();

using var context = new MasivoContext();

context.Masivo.ExecuteDelete();

repositorio.InsercionMasiva();

Console.ReadLine();