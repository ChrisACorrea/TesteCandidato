using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TesteCandidatoWeb.Data;
using TesteCandidatoWeb.Models;

namespace TesteCandidatoWeb.Services;

public class EnderecoService
{
    private ApplicationDbContext DbContext { get; }
    private DbSet<Endereco> DbSet { get; }
    private ViacepService ViaCepService { get; }

    public EnderecoService(ApplicationDbContext dbContext, ViacepService viaCepService)
    {
        DbContext = dbContext;
        DbSet = dbContext.Ceps;
        ViaCepService = viaCepService;
    }

    public async Task<Endereco?> GetEndereco(string cep, bool searchInDatabase = true)
    {
        var cleanedCep = CleanCep(cep);

        Endereco? endereco = null;

        if (searchInDatabase)
            endereco = await GetEnderecoByCep(cleanedCep);

        endereco ??= await SearchEnderecoInViacepByCep(cleanedCep);
        
        if (endereco is not null)
            return await Save(endereco);
        
        return endereco;
    }

    public async Task<Endereco> Save(Endereco endereco)
    {
        var adressFound = await GetEnderecoByCep(endereco.Cep);

        if(adressFound is not null)
            endereco.Id = adressFound.Id;

        if(endereco is not null && endereco.Id > 0)
        {
            DbContext.Update(endereco);
        } else
        {
            DbContext.Add(endereco);
        }

        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        return endereco;
    }

    private async Task<Endereco?> GetEnderecoByCep(string cep)
    {
        if (!IsValidCep(cep))
            throw new Exception("Cep Inválido");

        return await DbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Cep == cep);
    }

    public async Task<List<Endereco>> GetEnderecosByUf(string uf)
    {
        var cleanedUf = CleanUf(uf);

        if (!IsValidUf(cleanedUf))
            throw new Exception("UF Inválido");

        return await DbSet.Where(e => e.Uf.ToUpper() == cleanedUf)
            .AsNoTracking()
            .ToListAsync();
    }

    private async Task<Endereco?> SearchEnderecoInViacepByCep(string cep)
    {
        var adressFound = await ViaCepService.GetByCep(cep);

        if (adressFound is not null)
            adressFound.Cep = CleanCep(adressFound.Cep ?? string.Empty);

        return adressFound;
    }

    private static string CleanCep(string cep)
    {
        return cep?.Trim()?.Replace("-", string.Empty) ?? string.Empty;
    }

    private static bool IsValidCep(string cep)
    {
        return Regex.IsMatch(cep, "^\\d{8}$");
    }

    private static string CleanUf(string uf)
    {
        return uf?.Trim()?.ToUpper() ?? string.Empty;
    }

    private static bool IsValidUf(string uf)
    {
        return Regex.IsMatch(uf, "^[A-Z]{2}$");
    }
}
