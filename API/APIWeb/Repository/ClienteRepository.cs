using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Data;
using APIWeb.Models;
using APIWeb.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Context _context;

        public ClienteRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> ObterTodosClientes()
        {
            return await _context.Clientes.Where(cliente => cliente.EhAtivo)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Cliente> ObterCliente(int idCliente)
        {
            return await _context.Clientes.Where(cliente => cliente.IdCliente == idCliente)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> InserirCliente(Cliente cliente)
        {
            if (cliente is null)
            {
                return false;
            }

            try
            {
                cliente.EhAtivo = true;
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<bool> AtualizarCliente(Cliente cliente)
        {
            try
            {
                cliente.EhAtivo = true;
                _context.Entry(cliente).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeletarCliente(int idCliente)
        {
            try
            {
                Cliente cliente = _context.Clientes.Find(idCliente);
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;

        }
    }
}
