﻿using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MvcTrabajoMaster.Services
{
    public class ServiceApiTorneos
    {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiTorneos(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        #region CallApi
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    T data =
                        await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region Metodos Torneos
        public async Task<int> GetNumeroTorneosAsync()
        {
            string request = "/api/Torneos/GetNTorneos/";
            int numtorneos =
                await this.CallApiAsync<int>(request);

            return numtorneos;
        }
        public async Task<List<Torneo>> GetTorneosAsync()
        {
            string request = "/api/Torneos/GetTorneos/";
            List<Torneo> torneos =
                await this.CallApiAsync<List<Torneo>>(request);

            return torneos;
        }
        public async Task<Torneo> GetTorneoByIdAsync(int idtorneo)
        {
            string request = "/api/Torneos/FindTorneo/" + idtorneo;
            Torneo torneo =
                await this.CallApiAsync<Torneo>(request);

            return torneo;
        }

        public async Task<List<VistaTorneo>> GetTorneosPaginadoAsync(int posicion)
        {
            string request = "/api/Torneos/GetTorneosPag/" + posicion;
            List<VistaTorneo> torneos =
                await this.CallApiAsync<List<VistaTorneo>>(request);

            return torneos;
        }

        public async Task InsertTorneoAsync(int idtorneo, string nombre, string region, DateTime fecha, int napuntados,
            string descripcion, string normas, string tipo, string link, string foto)
        {

        }

        public async Task UpdateTorneoAsync(int idtorneo, string nombre, string region, DateTime fecha, int napuntados,
            string descripcion, string normas, string tipo, string link, string foto)
        {

        }

        public async Task<int> GetTorneoMaxIdAsync()
        {
            string request = "/api/Torneos/GetTorneoMaxId/";
            int idmaxtorneo =
                await this.CallApiAsync<int>(request);

            return idmaxtorneo;
        }

        public async Task SumarApuntadoAsync(int idtorneo)
        {

        }

        public async Task DeleteTorneo(int idtorneo)
        {

        }
        public async Task<List<Torneo>> GetTorneosByIdJugadorAsync(int idjugador)
        {
            string request = "/api/Torneos/GetTorneosByJugador/" + idjugador;
            List<Torneo> torneos =
                await this.CallApiAsync<List<Torneo>>(request);

            return torneos;
        }

        public async Task<int> GetNApuntadosTorneoAsync(int idtorneo)
        {
            string request = "/api/Torneos/GetNApuntadosTorneos/" + idtorneo;
            int napuntadostorneos =
                await this.CallApiAsync<int>(request);

            return napuntadostorneos;
        }

        #endregion

        #region Metodos Sets
        public async Task<List<Set>> GetSetsAsync()
        {
            string request = "/api/Sets/GetSets/";
            List<Set> sets =
                await this.CallApiAsync<List<Set>>(request);

            return sets;
        }
        public async Task<Set> GetSetByIdAsync(int idset)
        {
            string request = "/api/Sets/FindSet/" + idset;
            Set set =
                await this.CallApiAsync<Set>(request);

            return set;
        }

        public async Task InsertSetAsync(int idset, int ap1, int ap2, int apganador, string resultado, string ronda, int idtorneo)
        {

        }

        public async Task UpdateSetAsync(int idset, int ap1, int ap2, int apganador, string resultado, string ronda, int idtorneo)
        {

        }
        public async Task<int> GetSetMaxIdAsync()
        {
            string request = "/api/Sets/GetSetMaxId/";
            int MaxIdSet =
                await this.CallApiAsync<int>(request);

            return MaxIdSet;
        }

        public async Task<List<VistaSetFormateado>> GetSetsFormatByIdApuntadoAsync(int idapuntado)
        {
            string request = "/api/Sets/GetSetsFormateadoApuntado/" + idapuntado;
            List<VistaSetFormateado> sets =
                await this.CallApiAsync<List<VistaSetFormateado>>(request);

            return sets;
        }

        public async Task<List<VistaSetFormateado>> GetSetsFormatByIdJugadorAsync(int idjugador)
        {
            string request = "/api/Sets/GetSetsFormateadoJugador/" + idjugador;
            List<VistaSetFormateado> sets =
                await this.CallApiAsync<List<VistaSetFormateado>>(request);

            return sets;
        }

        public async Task DeleteSetAsync(int idset)
        {

        }
        #endregion

        #region Metodos Apuntados
        public async Task<List<Apuntado>> GetApuntadosAsync()
        {
            string request = "/api/Apuntados/GetApuntados/";
            List<Apuntado> apuntados =
                await this.CallApiAsync<List<Apuntado>>(request);

            return apuntados;
        }

        public async Task<List<Apuntado>> GetApuntadosByTorneoAsync(int idtorneo)
        {
            string request = "/api/Apuntados/GetApuntadosByTorneo/"+ idtorneo;
            List<Apuntado> apuntados =
                await this.CallApiAsync<List<Apuntado>>(request);

            return apuntados;
        }

        public async Task<List<VistaApuntadosJugadores>> GetVApuntadosByTorneoAsync(int idtorneo, int posicion)
        {
            string request = "/api/Apuntados/GetVApuntadosByTorneo/" + idtorneo + "/" + posicion;
            List<VistaApuntadosJugadores> apuntados =
                await this.CallApiAsync<List<VistaApuntadosJugadores>>(request);

            return apuntados;
        }

        public async Task<List<VistaApuntadosJugadores>> GetVApuntadosByTorneoNoPagAsync(int idtorneo)
        {
            string request = "/api/Apuntados/GetVApuntadosByTorneo/" + idtorneo;
            List<VistaApuntadosJugadores> apuntados =
                await this.CallApiAsync<List<VistaApuntadosJugadores>>(request);

            return apuntados;
        }

        public async Task<List<VistaApuntadosJugadores>> GetVApuntadosAsync()
        {
            string request = "/api/Apuntados/GetVApuntados";
            List<VistaApuntadosJugadores> apuntados =
                await this.CallApiAsync<List<VistaApuntadosJugadores>>(request);

            return apuntados;
        }
        public async Task<Apuntado> GetApuntadoByIdAsync(int idapuntado)
        {
            string request = "/api/Apuntados/FindApuntado/"+ idapuntado;
            Apuntado apuntado =
                await this.CallApiAsync<Apuntado>(request);

            return apuntado;
        }

        public async Task InsertApuntadoAsync(int idinscripcion, int idtorneo, int idjugador, int puesto, string record, int seed)
        {

        }

        public async Task DeleteApuntadoAsync(int idinscripcion)
        {

        }

        public async Task UpdateApuntadoAsync(int idinscripcion, int idtorneo, int idjugador, int puesto, string record, int seed)
        {

        }

        public async Task<int> GetApuntadoMaxIdAsync()
        {
            string request = "/api/Apuntados/GetMaxId/";
            int MaxIdApuntado =
                await this.CallApiAsync<int>(request);

            return MaxIdApuntado;
        }
        #endregion

        #region Metodos Jugadores
        public async Task<List<Jugador>> GetJugadoresAsync()
        {
            string request = "/api/Jugadores/GetJugadores/";
            List<Jugador> jugadores =
                await this.CallApiAsync<List<Jugador>>(request);

            return jugadores;
        }

        public async Task<int> GetNJugadoresAsync()
        {
            string request = "/api/Jugadores/GetNJugadores/";
            int NJugadores =
                await this.CallApiAsync<int>(request);

            return NJugadores;
        }

        public async Task<List<VistaJugadores>> GetVistaJugadoresAsync(int posicion)
        {
            string request = "/api/Jugadores/GetVistaJugadores/" + posicion;
            List<VistaJugadores> jugadores =
                await this.CallApiAsync<List<VistaJugadores>>(request);

            return jugadores;
        }

        public async Task DeleteJugador(int idjugador)
        {

        }

        public async Task<Jugador> GetJugadorByIdAsync(int idjugador)
        {
            string request = "/api/Jugadores/FindJugador/"+ idjugador;
            Jugador jugador =
                await this.CallApiAsync<Jugador>(request);

            return jugador;
        }

        public async Task InsertJugador(int idjugador, string nick, string region,
                string nombre, string email, string password,
                string rol, string equipo)
        {

        }

        public async Task UpdateJugador(int idjugador, string nick, string region,
             string nombre, string email, string password,
             string rol, string equipo)
        {

        }

        public async Task<Jugador> ExisteJugadorAsync(string email, string password)
        {
            string request = "/api/Jugadores/ExisteJugador/" + email + "/" + password;
            Jugador jugador =
                await this.CallApiAsync<Jugador>(request);

            return jugador;
        }
        #endregion


    }
}