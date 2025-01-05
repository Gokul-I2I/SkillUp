using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SkillUpBackend.CustomException;
using SkillUpBackend.Model;
using SkillUpBackend.ViewModel;
using System.Collections.ObjectModel;
using System.Data;

namespace SkillUpBackend.Repository
{
    public class BatchRepository : IBatchRepository
    {
        private readonly string _connectionString;
        public BatchRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<Batch>> GetBatches()
        {
            try
            {
                List<Batch> batches = new();
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using var command = new SqlCommand("GetAllActiveBatches", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        batches.Add(new Batch
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                            InsertedBy = reader.GetString(reader.GetOrdinal("InsertedBy")),
                            InsertedOn = reader.GetDateTime(reader.GetOrdinal("InsertedOn")),
                            UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy")),
                            UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? null : reader.GetDateTime(reader.GetOrdinal("UpdatedOn"))
                        });
                    }
                }
                return batches;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> CreateBatch(Batch batch)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                using var command = new SqlCommand("[dbo].[CreateBatch]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@BatchName", batch.Name);
                command.Parameters.AddWithValue("@IsActive", batch.IsActive);
                command.Parameters.AddWithValue("@InsertedBy", batch.InsertedBy);
                command.Parameters.AddWithValue("@UpdatedBy", batch.UpdatedBy ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UpdatedOn", batch.UpdatedOn ?? (object)DBNull.Value);

                SqlParameter outputParam = new SqlParameter("@BatchId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);
                await command.ExecuteNonQueryAsync();
                return (int)outputParam.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateBatch(Batch batch)
        {
            try
            {
                SqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                var command = new SqlCommand("UpdateBatch", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@BatchId", batch.Id);
                command.Parameters.AddWithValue("@Name", batch.Name);
                command.Parameters.AddWithValue("@IsActive", batch.IsActive);
                command.Parameters.AddWithValue("@UpdatedBy", batch.UpdatedBy);
                command.Parameters.AddWithValue("@UpdatedOn", batch.UpdatedOn ?? DateTime.Now);

                int rowsAffected = await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BatchViewModel> GetBatchByIdWithUsers(int id)
        {
            BatchViewModel batchViewModel = new()
            {
                UserCreateOrEdits = new List<UserCreateOrEdit>()
            };
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using var command = new SqlCommand("[dbo].[GetAllUserByBatchId]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@BatchId", id);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (batchViewModel.BatchId == 0)
                    {
                        batchViewModel.BatchId = reader.GetInt32(reader.GetOrdinal("BatchId"));
                        batchViewModel.BatchName = reader.GetString(reader.GetOrdinal("BatchName"));

                    }
                    batchViewModel.UserCreateOrEdits.Add(new UserCreateOrEdit
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Role = reader.GetString(reader.GetOrdinal("RoleName")),
                        JoinDate = reader.GetDateTime(reader.GetOrdinal("JoinDate")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    });
                }

            }
            return batchViewModel;
        }

        public async Task<Batch> GetBatchById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetBatchById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BatchId", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Safely handle potential NULL values from database
                            var batch = new Batch()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                InsertedBy = reader.GetString(reader.GetOrdinal("InsertedBy")),
                                UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy")),
                                InsertedOn = reader.GetDateTime(reader.GetOrdinal("InsertedOn")),
                                UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedOn"))
                            };

                            return batch;
                        }
                        else
                        {
                            throw new BatchNotFoundException();  // If no rows found, throw a custom exception
                        }
                    }
                }
            }
        }
        public async Task AddUserToBatch(BatchCreateOrEdit batchCreateOrEdit)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                foreach (var userId in batchCreateOrEdit.UserId)
                {
                    using var command = new SqlCommand("[dbo].[AddUserToBatch]", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@BatchId", batchCreateOrEdit.Id);
                    command.Parameters.AddWithValue("@InsertedBy", batchCreateOrEdit.InsertedBy);
                    command.Parameters.AddWithValue("@InsertedOn", batchCreateOrEdit.InsertedOn);
                    command.Parameters.AddWithValue("@JoinDate", batchCreateOrEdit.JoinDate);
                    command.Parameters.AddWithValue("@IsActive", batchCreateOrEdit.IsActive);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task RemoveUserFromBatch(BatchCreateOrEdit batchCreateOrEdit)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                foreach (var userId in batchCreateOrEdit.UserId)
                {
                    using var command = new SqlCommand("RemoveUserFromBatch", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@BatchId", batchCreateOrEdit.Id);
                    command.Parameters.AddWithValue("@UpdatedBy", batchCreateOrEdit.UpdatedBy);
                    command.Parameters.AddWithValue("@UpdatedOn", batchCreateOrEdit.UpdatedOn);
                    command.Parameters.AddWithValue("IsActive", batchCreateOrEdit.IsActive);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task ActiveUser(Collection<int> excitingUser, int batchId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                foreach (var userId in excitingUser)
                {
                    using var command = new SqlCommand("ActiveUser", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@BatchId", batchId);
                    command.Parameters.AddWithValue("@UpdatedBy", "admin");
                    command.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }

}
