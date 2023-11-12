using Dapper;
using DL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.BaseDL
{
    public abstract class BaseDL<T> : IBaseDL<T>
    {
        #region Field


        protected readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Construct

        public BaseDL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Method

        public abstract string GetTableName();

        //public virtual async Task<bool> CheckDuplicateCodeAsyns(Guid? id, string code)
        //{
        //    // tên table cần lấy 
        //    var tableName = this.GetTableName();

        //    // câu lệnh sql
        //    var query = $"SELECT {tableName}Id FROM {tableName} WHERE {tableName}Code = @code AND ({tableName}Id != @id OR LENGTH(@id) = 0)";

        //    // khởi tạo kết nối 
        //    var connection = _unitOfWork.GetDbConnection;

        //    // chuẩn bị dữ liệu đầu vào
        //    var parameters = new DynamicParameters();
        //    parameters.Add("id", id == null ? "" : id);
        //    parameters.Add("code", code);

        //    // kết nối vào db để truy vấn
        //    var result = await connection.QueryFirstOrDefaultAsync(query, parameters);

        //    // trả về kết quả
        //    return result != null;

        //}

        public virtual async Task DeleteMultiAsync(List<Guid> listRecordId)
        {
            // lấy tên table tương ứng
            var tableName = this.GetTableName();
            string listId = string.Join(",", listRecordId.Select(s => "'" + s + "'"));

            // khởi tạo kết nối
            var connection = _unitOfWork.GetDbConnection;

            // câu lệnh sql
            var query = $"DELETE FROM {tableName} WHERE @listId LIKE CONCAT ('%', {Char.ToUpper(tableName[0]) + tableName.Substring(1)}Id, '%')";

            /*int numberOfRecordAffect = 0;*/

            // chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("listId", listId);

            // thực thi câu lệnh sql
            await connection.ExecuteAsync(query, parameters, transaction: _unitOfWork.GetDbTransaction);
        }

        /// <summary>
        /// Trả về danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: DTQUOC (5/6/2023)
        public virtual async Task<IEnumerable<T>> GetAllRecordsAsync()
        {
            // lấy tên table
            var tableName = this.GetTableName();

            // khởi tạo kết nối vào database
            var connection = _unitOfWork.GetDbConnection;

            // lấy dữ liệu
            var entity = (T)Activator.CreateInstance(typeof(T));

            var props = entity.GetType().GetProperties();

            // tạo câu lệnh sql
            var query = $"SELECT " + string.Join(", ", props.Select(p => p.Name)) + $" FROM {this.GetTableName()}";

            // thực hiện lấy dữ liệu
            var result = await connection.QueryAsync<T>(query);

            // trả về kết quả
            return result;
        }

        /// <summary>
        /// Lấy thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="recordId">ID tài sản muốn lấy thông tin</param>
        /// <returns>Thông tin tài sản</returns>
        /// Created by: DTQUOC (5/6/2023)
        public async Task<T> GetRecordByIdAsync(Guid recordId)
        {
            // lấy tên table
            var tableName = GetTableName();

            // khởi tạo kết nối đến data base
            var connection = _unitOfWork.GetDbConnection;

            // tạo câu lệnh sql
            var query = $"SELECT * FROM {tableName} WHERE {Char.ToUpper(tableName[0]) + tableName.Substring(1)}Id = @recordId";

            // khởi tạo tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("recordId", recordId);

            // thực hiện lấy dữ liệu
            var result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters);

            // trả về kết quả
            return result;
        }

        /// <summary>
        /// hàm thêm một bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần thêm</param>
        /// Created by: DTQUOC (27/7/2023)
        public virtual async Task InsertRecordAsync(T record)
        {
            // lấy tên table
            var tableName = GetTableName();

            // khởi tạo kết nối
            var connection = _unitOfWork.GetDbConnection;

            // khởi tạo câu lệnh sql
            var query = $"INSERT INTO {tableName} (";
            var props = record.GetType().GetProperties().Where(p => p.GetValue(record) != null);

            // tạo câu lệnh sql
            query += string.Join(", ", props.Select(p => p.Name));
            query += ") VALUES (";
            query += string.Join(", ", props.Select(p => $"@{p.Name}"));
            query += ")";

            // khởi tạo thâm số đầu vào
            var parameters = new DynamicParameters();
            foreach (var p in props)
            {
                parameters.Add(p.Name, p.GetValue(record));
            }
            var propId = record.GetType().GetProperty($"{Char.ToUpper(tableName[0]) + tableName.Substring(1)}Id");
            var id = (Guid)propId.GetValue(record);
            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
                parameters.Add($"{tableName}Id", id);
            }
            parameters.Add($"CreatedBy", "DTQUOC");
            parameters.Add($"CreatedDate", DateTime.Now);

            // thực hiện câu lệnh sql
            await connection.ExecuteAsync(query, parameters);
        }

        public virtual async Task UpdateRecordAsync(Guid recordId, T record)
        {
            // lấy tên table
            var tableName = GetTableName();

            // khởi tạo kết nối đến database
            var connection = _unitOfWork.GetDbConnection;

            // khởi tạo câu lệnh sql
            var query = $"UPDATE {tableName} SET ";

            // lấy các trường của record không phải là Id
            var props = record.GetType().GetProperties().Where(p => p.GetValue(record) != null && p.Name != $"{Char.ToUpper(tableName[0]) + tableName.Substring(1)}Id");

            // viết câu lệnh sql
            query += string.Join(", ", props.Select(p => $"{p.Name} = @{p.Name}"));

            // phần WHERE của câu lệnh sql
            query += $" WHERE {Char.ToUpper(tableName[0]) + tableName.Substring(1)}Id = @{Char.ToUpper(tableName[0]) + tableName.Substring(1)}Id";
            var parameters = new DynamicParameters();

            foreach (var p in props)
            {
                parameters.Add(p.Name, p.GetValue(record));

            }
            parameters.Add($"{Char.ToUpper(tableName[0]) + tableName.Substring(1)}Id", recordId);
            parameters.Add($"ModifielDate", DateTime.Now);

            // thực hiện câu lệnh sql
            await connection.ExecuteAsync(query, parameters);
        }

        #endregion
    }
}
