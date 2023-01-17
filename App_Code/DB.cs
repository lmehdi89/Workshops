using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class DB
{
    private SqlConnection con;
    private SqlCommand cmd;

    public DB()
    {
        string DBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        con = new SqlConnection(DBConnectionString);
        cmd = new SqlCommand();
    }

    public bool ValidateUser(string UserName, string Password)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ValidateUser";

        cmd.Parameters.AddWithValue("@UserName", UserName);
        cmd.Parameters.AddWithValue("@Password", Password);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        string Sum = "";
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Sum = reader.GetValue(0).ToString();
            }
        }
        con.Close();

        if (Sum == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public DataTable GetRolesForUserName(string UserName)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetRolesForUserName";

        cmd.Parameters.AddWithValue("@UserName", UserName);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public DataTable GetAllUsers()
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetAllUsers";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public DataTable GetUsersNotInRole(string RoleId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetUsersNotInRole";

        cmd.Parameters.AddWithValue("@RoleId", RoleId);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public DataTable GetUsersInRole(string RoleId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetUsersInRole";

        cmd.Parameters.AddWithValue("@RoleId", RoleId);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public List<string> PermissionInRolesSelect(string RoleId, List<string> Permissions)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PermissionInRolesSelect";

        cmd.Parameters.AddWithValue("@RoleId", RoleId);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Permissions.Add(reader.GetInt32(0).ToString());
            }
        }

        con.Close();

        return Permissions;
    }

    public DataTable PermissionsSelect()
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PermissionsSelect";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public void PermissionInRolesDelete(string RoleId, string PermissionId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PermissionInRolesDelete";

        cmd.Parameters.AddWithValue("@RoleId", RoleId);
        cmd.Parameters.AddWithValue("@PermissionId", PermissionId);

        cmd.ExecuteNonQuery();
        con.Close();
    }

    public void PermissionInRolesInsert(string RoleId, string PermissionId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PermissionInRolesInsert";

        cmd.Parameters.AddWithValue("@RoleId", RoleId);
        cmd.Parameters.AddWithValue("@PermissionId", PermissionId);

        cmd.ExecuteNonQuery();
        con.Close();
    }

    public string GetUserId(string UserName)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetUserId";

        cmd.Parameters.AddWithValue("@UserName", UserName);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();
        string UserId = "";
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                UserId = reader.GetValue(0).ToString();
            }
        }

        con.Close();

        return UserId;
    }

    public DataTable UsersSelect()
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UsersSelect";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public int UsersInsert(string UserName, string Password, string FirstName, string LastName, string Email, string Tel, string Mobile, string Address, string PostalCode, bool IsActive, string CreatedUserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UsersInsert";

        cmd.Parameters.AddWithValue("@UserName", UserName);
        cmd.Parameters.AddWithValue("@Password", Password);
        cmd.Parameters.AddWithValue("@FirstName", FirstName);
        cmd.Parameters.AddWithValue("@LastName", LastName);
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Tel", Tel);
        cmd.Parameters.AddWithValue("@Mobile", Mobile);
        cmd.Parameters.AddWithValue("@Address", Address);
        cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);
        cmd.Parameters.AddWithValue("@CreatedUserId", CreatedUserId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public DataTable GetUserInfo(string UserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetUserInfo";

        cmd.Parameters.AddWithValue("@UserId", UserId);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public int UsersUpdate(string UserId, string UserName, string Password, string FirstName, string LastName, string Email, string Tel, string Mobile, string Address, string PostalCode, bool IsActive)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UsersUpdate";

        cmd.Parameters.AddWithValue("@UserId", UserId);
        cmd.Parameters.AddWithValue("@UserName", UserName);
        cmd.Parameters.AddWithValue("@Password", Password);
        cmd.Parameters.AddWithValue("@FirstName", FirstName);
        cmd.Parameters.AddWithValue("@LastName", LastName);
        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Tel", Tel);
        cmd.Parameters.AddWithValue("@Mobile", Mobile);
        cmd.Parameters.AddWithValue("@Address", Address);
        cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int UsersDelete(string UserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UsersDelete";

        cmd.Parameters.AddWithValue("@UserId", UserId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public DataTable RolesSelect()
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RolesSelect";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public int RolesUpdate(string RoleId, string RoleName, string IsActive)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RolesUpdate";

        cmd.Parameters.AddWithValue("@RoleId", RoleId);
        cmd.Parameters.AddWithValue("@RoleName", RoleName);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int RolesDelete(string RoleId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RolesDelete";

        cmd.Parameters.AddWithValue("@RoleId", RoleId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int RolesInsert(string RoleName, bool IsActive)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RolesInsert";

        cmd.Parameters.AddWithValue("@RoleName", RoleName);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int UsersInRolesUpdate(string RoleId, DataTable UserIds)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UsersInRolesUpdate";

        cmd.Parameters.AddWithValue("@RoleId", RoleId);
        cmd.Parameters.AddWithValue("@UserIds", UserIds);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public DataTable ArticlesSelect()
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ArticlesSelect";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public int ArticlesDelete(string ArticleId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ArticlesDelete";

        cmd.Parameters.AddWithValue("@ArticleId", ArticleId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int ArticlesInsert(string Title, string CodeCategory, string ImagePath, string Contents, bool IsActive, string CreatedUserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ArticlesInsert";

        cmd.Parameters.AddWithValue("@Title", Title);
        cmd.Parameters.AddWithValue("@CodeCategory", CodeCategory);
        cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
        cmd.Parameters.AddWithValue("@Contents", Contents);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);
        cmd.Parameters.AddWithValue("@CreatedUserId", CreatedUserId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public DataTable GetArticleInfo(string ArticleId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetArticleInfo";

        cmd.Parameters.AddWithValue("@ArticleId", ArticleId);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }
    public DataTable GetWorkShopInfo(int WorkShopID)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetWorkShopInfo";

        cmd.Parameters.AddWithValue("@WorkShopID", WorkShopID);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }


    public int ArticlesUpdate(string ArticleId, string Title, string CodeCategory, string ImagePath, string Contents, bool IsActive, string UserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ArticlesUpdate";

        cmd.Parameters.AddWithValue("@ArticleId", ArticleId);
        cmd.Parameters.AddWithValue("@Title", Title);
        cmd.Parameters.AddWithValue("@CodeCategory", CodeCategory);
        cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
        cmd.Parameters.AddWithValue("@Contents", Contents);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);
        cmd.Parameters.AddWithValue("@UserId", UserId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public DataTable ArticlesView(string ArticleId, string CodeCategory)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ArticlesView";

        cmd.Parameters.AddWithValue("@ArticleId", ArticleId);
        cmd.Parameters.AddWithValue("@CodeCategory", CodeCategory);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public DataTable GallerySelect(string ArticleId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GallerySelect";

        cmd.Parameters.AddWithValue("@ArticleId", ArticleId);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public int GalleryInsert(string ArticleId, string ImagePath, string ImageDesc, string CreatedUserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GalleryInsert";

        cmd.Parameters.AddWithValue("@ArticleId", ArticleId);
        cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
        cmd.Parameters.AddWithValue("@ImageDesc", ImageDesc);
        cmd.Parameters.AddWithValue("@CreatedUserId", CreatedUserId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int GalleryUpdate(string id, string ImageDesc, string UserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GalleryUpdate";

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@ImageDesc", ImageDesc);
        cmd.Parameters.AddWithValue("@UserId", UserId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int GalleryDelete(string id)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GalleryDelete";

        cmd.Parameters.AddWithValue("@id", id);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public DataTable MenuSelectAll()
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MenuSelectAll";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public DataTable MenuSelect(string ParentId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MenuSelect";

        cmd.Parameters.AddWithValue("@ParentId", ParentId);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public int MenuInsert(string ParentId, string MenuTitle, string MenuContent, bool IsActive, string CreatedUserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MenuInsert";

        cmd.Parameters.AddWithValue("@ParentId", ParentId);
        cmd.Parameters.AddWithValue("@MenuTitle", MenuTitle);
        cmd.Parameters.AddWithValue("@MenuContent", MenuContent);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);
        cmd.Parameters.AddWithValue("@CreatedUserId", CreatedUserId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int MenuUpdate(string id, string ParentId, string MenuTitle, string MenuContent, bool IsActive, string UserId)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MenuUpdate";

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@ParentId", ParentId);
        cmd.Parameters.AddWithValue("@MenuTitle", MenuTitle);
        cmd.Parameters.AddWithValue("@MenuContent", MenuContent);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);
        cmd.Parameters.AddWithValue("@UserId", UserId);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int MenuDelete(string id)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MenuDelete";

        cmd.Parameters.AddWithValue("@id", id);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public DataTable GetMenuInfo(string id)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetMenuInfo";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public DataTable MenuSelectForParentId(string id)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MenuSelectForParentId";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public DataTable CategorySelect()
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CategorySelect";

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public int CategoryInsert(string CodeCategory, string NameCategory)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CategoryInsert";

        cmd.Parameters.AddWithValue("@CodeCategory", CodeCategory);
        cmd.Parameters.AddWithValue("@NameCategory", NameCategory);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int CategoryUpdate(string id, string CodeCategory, string NameCategory)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CategoryUpdate";

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@CodeCategory", CodeCategory);
        cmd.Parameters.AddWithValue("@NameCategory", NameCategory);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public int CategoryDelete(string id)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CategoryDelete";

        cmd.Parameters.AddWithValue("@id", id);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }

    public DataTable WorkshopsSelect(int status)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "WorkshopsSelect";
        cmd.Parameters.AddWithValue("@status", status);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }

    public int WorkshopsInsert(string Name, string Code, int Times, string Place, DateTime StartTime, DateTime EndTime, string ProfName, int Capacity, string Descr, bool IsActive, int Reserved)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "WorkshopInsert";

        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Code", Code);
        cmd.Parameters.AddWithValue("@Times", Times);
        cmd.Parameters.AddWithValue("@Place", Place);
        cmd.Parameters.AddWithValue("@StartTime", StartTime);
        cmd.Parameters.AddWithValue("@EndTime", EndTime);
        cmd.Parameters.AddWithValue("@ProfName", ProfName);
        cmd.Parameters.AddWithValue("@Capacity", Capacity);
        cmd.Parameters.AddWithValue("@Descr", Descr);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);
        cmd.Parameters.AddWithValue("@Reserved", Reserved);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }
    public int WorkshopsUpdate(string Name, string Code, int Times, string Place, DateTime StartTime, DateTime EndTime, string ProfName, int Capacity, string Descr, bool IsActive, int Reserved, int WorkShopID)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "WorkshopUpdate";

        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Code", Code);
        cmd.Parameters.AddWithValue("@Times", Times);
        cmd.Parameters.AddWithValue("@Place", Place);
        cmd.Parameters.AddWithValue("@StartTime", StartTime);
        cmd.Parameters.AddWithValue("@EndTime", EndTime);
        cmd.Parameters.AddWithValue("@ProfName", ProfName);
        cmd.Parameters.AddWithValue("@Capacity", Capacity);
        cmd.Parameters.AddWithValue("@Descr", Descr);
        cmd.Parameters.AddWithValue("@IsActive", IsActive);
        cmd.Parameters.AddWithValue("@Reserved", Reserved);
        cmd.Parameters.AddWithValue("@WorkShopID", WorkShopID);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;
    }
    public DataTable WorkShopDelete(string WorkShopID, int status)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "WorkShopDelete";

        cmd.Parameters.AddWithValue("@WorkShopID", WorkShopID);
        cmd.Parameters.AddWithValue("@status", status);

        //var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        //returnParameter.Direction = ParameterDirection.ReturnValue;
        //cmd.ExecuteNonQuery();
        //int Result = (int)returnParameter.Value;

        //con.Close();

        //return Result;
        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;

    }
    public DataTable UserInWorkshopsSelect(string WorkshopID)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UserInWorkshopsSelect";
        cmd.Parameters.AddWithValue("@WorkshopID", WorkshopID);

        SqlDataReader reader;
        reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);
        con.Close();

        return dt;
    }
    public int UserInWorkShopDelete(string WorkShopID, string UserID)
    {
        con.Open();
        cmd.Parameters.Clear();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UserInWorkShopDelete";

        cmd.Parameters.AddWithValue("@WorkShopID", WorkShopID);
        cmd.Parameters.AddWithValue("@UserID", UserID);

        var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        int Result = (int)returnParameter.Value;

        con.Close();

        return Result;

    }
}
