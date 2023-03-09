using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Google.Cloud.Firestore;
using FirebaseAdmin;
using Google.Cloud.Firestore.V1;
using System.Collections.Generic;
using AutoMapper;
using Yuppie.WebApi.Infra.Repository;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Chat;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;
using System.Linq;
using Newtonsoft.Json;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{

    public class ChatFirebaseService : IChatFirebaseService
    {
        private readonly FirebaseApp _dbFirebase;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        public ChatFirebaseService(FirebaseApp app, IMapper mapper, IUsuarioService usuarioService)
        {
            _dbFirebase = app;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public void Login()
        {

        }
        public async Task<ObjectResult> IniciaValidacaoLogin(int idChat)
        {
            try
            {
                var db = new FirestoreDbContext(_dbFirebase).GetDb();
                var docRef = db.Collection("user");
                Query docRefPorId = docRef.WhereEqualTo("id", idChat);
                QuerySnapshot querySnapshot = await docRefPorId.GetSnapshotAsync();

                if (querySnapshot.Count == 0)
                {
                    var consulta = await _usuarioService.BuscarUsuarioPorId(idChat);
                    UsuarioModel user = (UsuarioModel)consulta.Value;
                    var cfbModel = new ChatFirebaseUserModel
                    {
                        id = user.Id,
                        name = user.Nome,
                        status = "Offline"
                    };

                    var docID = user.Id.ToString();
                    await docRef.Document(docID).CreateAsync(cfbModel);                 
                    return new ObjectResult(cfbModel)
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                else
                {
                    var consulta = await _usuarioService.BuscarUsuarioPorId(idChat);
                    UsuarioModel user = (UsuarioModel)consulta.Value;
                    var docID = user.Id.ToString();
                    var objUser = new Dictionary<string, object>
                    {
                            { "id", user.Id },
                            { "name", user.Nome },
                            { "status", "Online" }
                    };

                    foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                    {
                        Dictionary<string, object> usuario = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(usuario);
                        ChatFirebaseUserModel usuarioModel = JsonConvert.DeserializeObject<ChatFirebaseUserModel>(json);
                        if (usuarioModel.photo != null)
                            objUser.Add("photo", usuarioModel.photo);

                        var collection = db.Collection("user");
                        await collection.AddAsync(objUser);
                        return new ObjectResult(objUser)
                        {
                            StatusCode = StatusCodes.Status201Created
                        };
                    }   
                }
                return new ObjectResult(idChat)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(idChat)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> BuscarUsuarioPorId(int id)
        {
            try
            {
                var db = new FirestoreDbContext(_dbFirebase).GetDb();
                var docRef = db.Collection("user");
                Query docRefPorId = docRef.WhereEqualTo("id", id);
                QuerySnapshot querySnapshot = await docRefPorId.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    Dictionary<string, object> usuario = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(usuario);
                    ChatFirebaseUserModel usuarioModel = JsonConvert.DeserializeObject<ChatFirebaseUserModel>(json);
                    return new ObjectResult(usuarioModel)
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new ObjectResult(id)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(id)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarContratosPorId(int id)
        {
            try
            {
                List<ChatFirebaseUserModel> contratosLista = new List<ChatFirebaseUserModel>();
                var db = new FirestoreDbContext(_dbFirebase).GetDb();                
                var docRef = db.Collection("user");
                Query docRefPorId = docRef.WhereEqualTo("id", id);
                QuerySnapshot querySnapshot = await docRef.GetSnapshotAsync();                
                var contratos = await docRef.Document(id.ToString()).Collection("contracts").GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapshot in contratos.Documents)
                {
                    Dictionary<string, object> contrato = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(contrato);
                    ChatFirebaseUserModel model = JsonConvert.DeserializeObject<ChatFirebaseUserModel>(json);
                    contratosLista.Add(model);

                }
                return new ObjectResult(contratosLista)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(id)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> AtualizarDadosChatUsuario(ChatFirebaseUserModel model) {
            try
            {
                return new ObjectResult(model)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception)
            {
                return new ObjectResult(model)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> AdicionarContratos(int IdComprador, int IdVendedor)
        {
            try
            {
                return new ObjectResult(true)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception)
            {
                return new ObjectResult(false)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

    }
}
