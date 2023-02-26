using FirebaseAdmin;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace Yuppie.WebApi.Infra.Context
{
    public class FirestoreDbContext
    {
        private readonly FirebaseApp _dbFirebase;
        private FirestoreDb _db;
        private string _projectId;

        public FirestoreDbContext(FirebaseApp app)
        {
            _dbFirebase = app;
            _db = FirestoreDb.Create(_dbFirebase.Options.ProjectId);
        }

        public FirestoreDb GetDb()
        {
            return _db;
        }
    }
}
